import asyncio
import threading
from app_core import (
    app,
    TCP_HOST,
    TCP_PORT,
    FLASK_HOST,
    FLASK_PORT,
    logger,
    apps_logger
)
from routes import *
from connections import start_log_server

def run_tcp_server_in_thread():
    asyncio.run(start_log_server(TCP_HOST, TCP_PORT))

if __name__ == "__main__":
    try:
        logger.info("Starting log server thread")
        tcp_thread = threading.Thread(
            target=run_tcp_server_in_thread,
            daemon=True)
        tcp_thread.start()

        logger.info(
            "Starting Flask HTTP server on %s:%d", FLASK_HOST, FLASK_PORT
        )
        app.run(host=FLASK_HOST, port=FLASK_PORT, debug=False, use_reloader=False, threaded=True)
    finally:
        apps_logger.close()
