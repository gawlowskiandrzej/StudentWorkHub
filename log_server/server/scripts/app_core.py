from flask import Flask
from helpers import setup_logging
from log_store import LogStore

# TCP server configuration
TCP_HOST: str = "0.0.0.0"
TCP_PORT: int = 54000

# Flask configuration
FLASK_HOST: str = "0.0.0.0"
FLASK_PORT: int = 54001

# Folder and file for logs
LOGS_DIRECTORY: str = "logs"
LOG_FILE_NAME: str = "app_server_logs.log"

# TLS encryption
TLS_CERTFILE: str = "public_keys/log_server.crt"
TLS_KEYFILE: str = "server.key"

logger = setup_logging(LOGS_DIRECTORY, LOG_FILE_NAME)
apps_logger = LogStore(LOGS_DIRECTORY, "apps_log", buffer_limit=100, max_file_size=3*1024*1024)

app = Flask(__name__)
