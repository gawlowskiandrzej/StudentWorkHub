import os
import threading
from pathlib import Path
from datetime import datetime

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

    def _validate_message(self, message: str) -> str | None:
        if not isinstance(message, str):
            return None
        fields: list[str] = []
        i = 0
        n = len(message)
        while i < n:
            if message[i] != "[":
                return None
            j = message.find("]", i + 1)
            if j == -1:
                return None
            fields.append(message[i + 1:j])
            i = j + 1
        if i != n:
            return None
        if len(fields) != 6:
            return None
        date_str, type_str, server_id, server_id_ext, tags_str, msg_str = fields
        try:
            datetime.strptime(date_str, "%Y-%m-%dT%H:%M:%S.%fZ")
        except ValueError:
            return None
        allowed_types = {
            "INFO",
            "WARNING",
            "ERROR",
            "CRITICAL ERROR",
            "NOTIFICATION",
            "ATTENTION",
            "DEBUG",
            "DIAGNOSTICS",
        }
        if type_str not in allowed_types:
            return None
        if not server_id:
            return None
        if tags_str:
            tags = tags_str.split(",")
            normalized_tags: list[str] = []
            for tag in tags[:5]:
                if len(tag) > 128:
                    tag = tag[:128]
                normalized_tags.append(tag)
            tags_str = ",".join(normalized_tags)
        msg_str = msg_str or ""
        if not msg_str:
            return None
        if len(msg_str) > 512:
            msg_str = msg_str[:512 - 3] + "..."
        return f"[{date_str}][{type_str}][{server_id}][{server_id_ext}][{tags_str}][{msg_str}]"

    def write(self, messages: list[str]) -> None:
        if not isinstance(messages, (list, tuple)):
            raise TypeError("messages must be a list or tuple of strings")
        prepared: list[str] = []
        for m in messages:
            if not isinstance(m, str):
                raise TypeError("all messages must be strings")
            validated = self._validate_message(m)
            if validated is None:
                continue
            prepared.append(validated)
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
            segments: list[tuple[str, int | None, int]] = []

            for file_id in file_ids:
                path = self._file_path(file_id)
                if not path.exists():
                    continue
                count = self.file_line_counts.get(file_id, 0)
                if count > 0:
                    segments.append(("file", file_id, count))

            buffer_copy = list(self.buffer)
            if buffer_copy:
                segments.append(("buffer", None, len(buffer_copy)))

            total_lines = sum(seg[2] for seg in segments)
            if total_lines == 0 or offset >= total_lines:
                return []

            remaining = limit
            current_offset = offset
            result: list[str] = []

            for kind, file_id, seg_count in segments:
                if remaining <= 0:
                    break

                if current_offset >= seg_count:
                    current_offset -= seg_count
                    continue

                if kind == "file":
                    path = self._file_path(file_id)
                    if not path.exists():
                        current_offset = 0
                        continue

                    skipped = 0
                    with path.open("r", encoding="utf-8", newline="") as f:
                        for line in f:
                            if skipped < current_offset:
                                skipped += 1
                                continue
                            if remaining <= 0:
                                break
                            if line.endswith("\n"):
                                line = line[:-1]
                            line = line.replace("\x1E", "\n")
                            result.append(line)
                            remaining -= 1

                    current_offset = 0
                else:
                    start_index = current_offset
                    end_index = min(seg_count, current_offset + remaining)
                    for i in range(start_index, end_index):
                        line = buffer_copy[i].replace("\x1E", "\n")
                        result.append(line)
                        remaining -= 1
                    current_offset = 0

            return result

    def get_current_offset(self) -> int:
        with self.lock:
            return sum(self.file_line_counts.values()) + len(self.buffer)

    def close(self) -> None:
        with self.lock:
            self._flush_locked()
            self.current_file_id = None

    def __del__(self):
        try:
            self.close()
        except Exception:
            pass