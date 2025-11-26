import os
import threading
from pathlib import Path

class LogStore:
    _DEFAULT_LOG_BASE_NAME: str = "app_log"
    _DEFAULT_BUFFER_LIMIT: int = 100
    _DEFAULT_MAX_FILE_SIZE: int = 3 * 1024 * 1024
    
    def __init__(self, log_dir: str | Path = "logs", log_base_name: str = _DEFAULT_LOG_BASE_NAME, buffer_limit: int = _DEFAULT_BUFFER_LIMIT, max_file_size: int = _DEFAULT_MAX_FILE_SIZE):
        self.directory = Path(log_dir).expanduser().resolve()
        if not self.directory.is_dir():
            raise FileNotFoundError(f"Directory does not exist: {self.directory}")

        self.base_name = str(log_base_name) if log_base_name else self._DEFAULT_LOG_BASE_NAME
        self.buffer_limit = int(buffer_limit) if buffer_limit > 0 else self._DEFAULT_BUFFER_LIMIT
        self.max_file_size = int(max_file_size) if max_file_size > 1024 else self._DEFAULT_MAX_FILE_SIZE

        self.lock = threading.Lock()
        self.buffer: list[str] = []
        self.file_line_counts: dict[int, int] = {}
        self.current_file_id: int | None = None
        self.current_file_size: int = 0

        self._initialize_files()

    def _file_path(self, file_id: int) -> Path:
        return self.directory.joinpath(f"{self.base_name}_{file_id}.log")

    def _protect_file(self, path: Path) -> None:
        try:
            path = Path(path)
            if not path.is_file():
                return
            
            os.chown(path, 0, 0)
        except (PermissionError, OSError, AttributeError):
            return
        try:
            path.chmod(0o004)
        except (PermissionError, OSError, NotImplementedError):
            return

    def _initialize_files(self) -> None:
        existing_files = {}
        prefix = f"{self.base_name}_"
        pattern = f"{self.base_name}_*.log"

        for path in self.directory.glob(pattern):
            stem = path.stem
            if not stem.startswith(prefix):
                continue
            id_part = stem[len(prefix):]
            try:
                file_id = int(id_part)
            except ValueError:
                continue

            line_count = 0
            with path.open("r", encoding="utf-8", newline="") as f:
                for _ in f:
                    line_count += 1

            existing_files[file_id] = (path, line_count)

        if existing_files:
            for file_id, (path, line_count) in existing_files.items():
                self.file_line_counts[file_id] = line_count
            self.current_file_id = max(existing_files.keys())
            last_path = existing_files[self.current_file_id][0]
            self.current_file_size = last_path.stat().st_size

            for file_id, (path, _) in existing_files.items():
                if file_id != self.current_file_id:
                    self._protect_file(path)
        else:
            self.current_file_id = 0
            path = self._file_path(self.current_file_id)
            path.touch(exist_ok=True)
            self.file_line_counts[self.current_file_id] = 0
            self.current_file_size = 0

    def _flush_locked(self) -> None:
        if not self.buffer:
            return

        if self.current_file_id is None:
            raise RuntimeError("Logger is closed")

        path = self._file_path(self.current_file_id)
        if not path.exists():
            path.touch(exist_ok=True)

        f = path.open("ab")
        try:
            for raw_line in self.buffer:
                safe_line = raw_line.replace("\n", "\x1E")
                data = (safe_line + "\n").encode("utf-8")

                if self.current_file_size + len(data) > self.max_file_size and self.current_file_size > 0:
                    f.close()
                    self._protect_file(path)
                    self.current_file_id = max(self.file_line_counts.keys()) + 1
                    path = self._file_path(self.current_file_id)
                    path.touch(exist_ok=True)
                    self.current_file_size = 0
                    self.file_line_counts[self.current_file_id] = 0
                    f = path.open("ab")

                f.write(data)
                self.current_file_size += len(data)
                self.file_line_counts[self.current_file_id] = self.file_line_counts.get(self.current_file_id, 0) + 1
        finally:
            f.close()
            self.buffer.clear()

    def flush(self) -> None:
        with self.lock:
            self._flush_locked()

    def write(self, messages: list[str]) -> None:
        if not isinstance(messages, (list, tuple)):
            raise TypeError("messages must be a list or tuple of strings")

        prepared: list[str] = []
        for m in messages:
            if not isinstance(m, str):
                raise TypeError("all messages must be strings")
            prepared.append(m)

        if not prepared:
            return

        with self.lock:
            if self.current_file_id is None:
                raise RuntimeError("Logger is closed")

            if len(self.buffer) + len(prepared) < self.buffer_limit:
                self.buffer.extend(prepared)
            else:
                self.buffer.extend(prepared)
                self._flush_locked()

    def read(self, offset: int, limit: int) -> list[str]:
        if limit <= 0:
            return []
        if offset < 0:
            offset = 0

        with self.lock:
            if self.current_file_id is None:
                return []

            file_ids = sorted(self.file_line_counts.keys())
            segments = []
            for file_id in file_ids:
                count = self.file_line_counts.get(file_id, 0)
                if count > 0:
                    segments.append(("file", file_id, count))

            buffer_copy = list(self.buffer)
            if buffer_copy:
                segments.append(("buffer", None, len(buffer_copy)))

            total_lines = sum(seg[2] for seg in segments)
            if total_lines == 0 or offset >= total_lines:
                return []

            target_total = min(limit + offset, total_lines)
            selected_segments = []
            remaining = target_total

            for seg in reversed(segments):
                if remaining <= 0:
                    break
                selected_segments.append(seg)
                remaining -= seg[2]

            selected_segments.reverse()

            all_lines: list[str] = []
            for seg in selected_segments:
                kind, file_id, _ = seg
                if kind == "file":
                    path = self._file_path(file_id)
                    if not path.exists():
                        continue
                    with path.open("r", encoding="utf-8", newline="") as f:
                        for line in f:
                            if line.endswith("\n"):
                                line = line[:-1]
                            line = line.replace("\x1E", "\n")
                            all_lines.append(line)
                else:
                    for line in buffer_copy:
                        line = line.replace("\x1E", "\n")
                        all_lines.append(line)

            if not all_lines:
                return []

            total = len(all_lines)
            end_index = total - offset
            if end_index < 0:
                return []

            start_index = end_index - limit
            if start_index < 0:
                start_index = 0

            return all_lines[start_index:end_index]

    def close(self) -> None:
        with self.lock:
            self._flush_locked()
            self.current_file_id = None

    def __del__(self):
        try:
            self.close()
        except Exception:
            pass
