import sys
import signal
import asyncio
import threading
from app_core import (
    app,
    TCP_HOST,
    TCP_PORT,
    FLASK_HOST,
    FLASK_PORT,
    TLS_CERTFILE,
    TLS_KEYFILE,
    logger,
    apps_logger
)
from routes import *
from connections import start_log_server

def shutdown_gracefully(signum, _):
    logger.info("Received signal %s, shutting down", signum)
    try:
        apps_logger.close()
    finally:
        sys.exit(0)

def run_tcp_server_in_thread():
    asyncio.run(start_log_server(TCP_HOST, TCP_PORT, TLS_CERTFILE, TLS_KEYFILE))

if __name__ == "__main__":
    signal.signal(signal.SIGTERM, shutdown_gracefully)
    signal.signal(signal.SIGINT, shutdown_gracefully)
    
    try:
        logger.info("Starting log server thread")
        
        tcp_thread = threading.Thread(
            target=run_tcp_server_in_thread,
            daemon=True)
        tcp_thread.start()

        logger.info("Starting Flask HTTP server on %s:%d",
                    FLASK_HOST,
                    FLASK_PORT)
        
        app.run(host=FLASK_HOST,
                port=FLASK_PORT,
                debug=False,
                use_reloader=False,
                threaded=True,
                ssl_context=(TLS_CERTFILE, TLS_KEYFILE))
    finally:
        logger.info("Main finally block, closing apps_logger")
        apps_logger.close()
