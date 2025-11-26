#!/usr/bin/env bash
set -euo pipefail

# Use APP_DIR from environment, fallback to /log_server if not set
APP_DIR="${APP_DIR:-/log_server}"

KEY_DIR="${APP_DIR}/public_keys"
PRIVATE_KEY="${APP_DIR}/server.key"
CERT_PATH="${KEY_DIR}/log_server.crt"

# Ensure key directory exists (it is a shared Docker volume)
mkdir -p "${KEY_DIR}"
chmod 1777 "${KEY_DIR}"

# Generate certificate only if it does not exist yet
if [ ! -f "${CERT_PATH}" ]; then
    echo "[log_server] Generating TLS key and certificate in ${APP_DIR}..."
    openssl req -x509 -newkey rsa:4096 \
        -keyout "${PRIVATE_KEY}" \
        -out "${CERT_PATH}" \
        -days 3650 -nodes \
        -subj "/CN=log-server"

    # file ownership będzie appuser:appuser, bo skrypt działa jako appuser
    chmod 604 "${CERT_PATH}"
    chmod 0400 "${PRIVATE_KEY}"
    echo "[log_server] Certificate generated at ${CERT_PATH}"
else
    echo "[log_server] Existing certificate found at ${CERT_PATH}, skipping generation."
fi

# Start the main process
exec "$@"
