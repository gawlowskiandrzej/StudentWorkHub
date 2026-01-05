#!/usr/bin/env bash
set -euo pipefail

# Use APP_DIR from environment, fallback to /log_server if not set
APP_DIR="${APP_DIR:-/log_server}"

KEY_DIR="${APP_DIR}/public_keys"
PRIVATE_KEY="${APP_DIR}/server.key"
CERT_PATH="${KEY_DIR}/log_server.crt"

mkdir -p "${KEY_DIR}"
chmod 1777 "${KEY_DIR}" 2>/dev/null || true

# If either cert OR key is missing -> regenerate both
if [ ! -f "${CERT_PATH}" ] || [ ! -f "${PRIVATE_KEY}" ]; then
  echo "[log_server] TLS material incomplete -> regenerating private key + certificate..."

  # Start clean (ignore errors)
  rm -f "${CERT_PATH}" "${PRIVATE_KEY}" 2>/dev/null || true

  openssl req -x509 -newkey rsa:4096 \
    -keyout "${PRIVATE_KEY}" \
    -out "${CERT_PATH}" \
    -days 3650 -nodes \
    -subj "/CN=log-server"

  # file ownership będzie appuser:appuser, bo skrypt działa jako appuser
  chmod 604 "${CERT_PATH}" 2>/dev/null || true
  chmod 0400 "${PRIVATE_KEY}" 2>/dev/null || true

  echo "[log_server] Certificate generated at ${CERT_PATH}"
else
  echo "[log_server] Certificate and private key OK, skipping generation."
fi

exec "$@"
