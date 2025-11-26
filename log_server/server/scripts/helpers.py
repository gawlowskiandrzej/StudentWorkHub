import os
import sys
import logging
from pathlib import Path
from cryptography.hazmat.primitives import serialization
from cryptography.hazmat.primitives.asymmetric import rsa

def setup_logging(logs_dir: str | Path, logs_file: str, logger_name: str = "log_server") -> logging.Logger:
    """
    Initialize global logging configuration with console and file handlers.

    The function configures the root logger to log messages at INFO level and
    attaches two handlers:
    1. A console handler that writes log records to standard output.
    2. A file handler that writes log records to the given log file.

    This function is safe to call multiple times: if a logger with the given
    name already has handlers attached, the existing logger instance is returned
    without reconfiguring the logging system.

    :param logs_dir: Base directory where the log file will be stored.
                     The directory must already exist.
    :type logs_dir: pathlib.Path
    :param logs_file: Name of the log file to create or append to inside logs_dir.
    :type logs_file: str
    :param logger_name: Name of the application logger that will be returned and used
                        as the main logger in the codebase.
                        Defaults to "log_server".
    :type logger_name: str
    :returns: Configured application logger instance that can be reused across the codebase.
    :rtype: logging.Logger
    :raises ValueError: If logs_dir does not exist or is not a directory.
    """

    # If a logger with this name already has handlers, assume logging is configured and reuse it
    existing_logger = logging.getLogger(logger_name)
    if existing_logger.handlers:
        return existing_logger
    
    logs_dir = Path(logs_dir).expanduser().resolve()
    if not logs_dir.is_dir():
        raise ValueError(f"Logs directory: {logs_dir} doesn't exist")
    log_file_path: Path = logs_dir.joinpath(logs_file)

    # Get root logger
    root_logger: logging.Logger = logging.getLogger()
    root_logger.setLevel(logging.INFO)

    # Remove old handlers if any
    if root_logger.hasHandlers():
        root_logger.handlers.clear()

    # Common formatter
    formatter: logging.Formatter = logging.Formatter(
        fmt="%(asctime)s [%(levelname)s] %(name)s - %(message)s",
        datefmt="%Y-%m-%d %H:%M:%S",
    )

    # Console handler
    console_handler: logging.StreamHandler = logging.StreamHandler(sys.stdout)
    console_handler.setLevel(logging.INFO)
    console_handler.setFormatter(formatter)

    # File handler
    file_handler: logging.FileHandler = logging.FileHandler(str(log_file_path), encoding="utf-8")
    file_handler.setLevel(logging.INFO)
    file_handler.setFormatter(formatter)

    # Attach handlers
    root_logger.addHandler(console_handler)
    root_logger.addHandler(file_handler)

    app_logger: logging.Logger = logging.getLogger(logger_name)
    app_logger.info("Log initialized. Log file: %s", log_file_path)

    return app_logger

def get_public_keys(public_keys_folder: str | Path) -> list[rsa.RSAPublicKey]:
    """
    Load all RSA public keys from the given directory.

    The function scans the provided directory for files, attempts to load each file
    as a PEM-encoded public key and returns a list of successfully loaded
    RSAPublicKey instances. Subdirectories and empty files are ignored.

    :param public_keys_folder: Path to the directory containing PEM-encoded public key files.
    :type public_keys_folder: str | pathlib.Path
    :returns: List of loaded RSA public keys found in the directory.
    :rtype: list[rsa.RSAPublicKey]
    :raises ValueError: If the given path does not exist or is not a directory.
    """
    # Normalize directory path, expand user (~) and resolve to absolute path
    public_keys_folder = Path(public_keys_folder).expanduser().resolve()
    if not public_keys_folder.is_dir():
        raise ValueError(f"Public keys directory: {public_keys_folder} doesn't exist")
    
    public_keys: list[rsa.RSAPublicKey] = []
    for file in public_keys_folder.iterdir():
        file_path = public_keys_folder.joinpath(file)
        
        # Skip subdirectories
        if file_path.is_dir():
            continue
        
        # Read file content as bytes
        with open(file_path, "rb") as key_file:
            pem_data = key_file.read()
        
        # Skip empty files
        if not pem_data:
            continue
        
        # Try to load PEM-encoded public key from file content
        public_key = serialization.load_pem_public_key(pem_data)
        
        # Only keep RSA public keys, ignore other key types
        if isinstance(public_key, rsa.RSAPublicKey):
            public_keys.append(public_key)
            
    return public_keys

def generate_challenge() -> bytes:
    """
    Generate a cryptographically secure random challenge value.

    :returns: Challenge value as a 64-character lowercase hex string
              encoded as ASCII bytes.
    :rtype: bytes
    """
    return os.urandom(32).hex().encode("ascii") 

def parse_log_entry(entry_str):
    original = entry_str
    s = entry_str.strip()

    if s.startswith('[') and s.endswith(']'):
        s = s[1:-1]

    parts = s.split('][')

    while len(parts) < 6:
        parts.append('')

    date_str = parts[0]
    type_str = parts[1]
    server_id = parts[2]
    server_id_ext = parts[3]
    tags_str = parts[4]
    message = parts[5]

    tags = [t for t in tags_str.split(',') if t] if tags_str else []

    return {
        "date": date_str,
        "type": type_str,
        "serverId": server_id,
        "serverIdExt": server_id_ext,
        "tags": tags,
        "message": message,
        "raw": original
    }