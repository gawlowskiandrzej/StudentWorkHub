#!/usr/bin/env bash
set -euo pipefail

KEY_DIR="/log_server/public_keys"

# Server expects .pem in public_keys
PUBLIC_PEM="${KEY_DIR}/offer_manager.pem"

# Optional compatibility copy (can be removed if not needed)
PUBLIC_PUB="${KEY_DIR}/offer_manager.pub"

PRIVATE_DIR="/var/lib/offer_manager"
PRIVATE_KEY="${PRIVATE_DIR}/offer_manager.key"

mkdir -p "${KEY_DIR}"
chmod 1777 "${KEY_DIR}" 2>/dev/null || true

# Ensure private dir exists; fallback if no permissions
if ! mkdir -p "${PRIVATE_DIR}" 2>/dev/null; then
  PRIVATE_DIR="/tmp/offer_manager"
  PRIVATE_KEY="${PRIVATE_DIR}/offer_manager.key"
  mkdir -p "${PRIVATE_DIR}"
fi

generate_private_key() {
  echo "[offer_manager] Generating private key: ${PRIVATE_KEY}"
  openssl genpkey -algorithm RSA -pkeyopt rsa_keygen_bits:4096 -out "${PRIVATE_KEY}"
  chmod 0400 "${PRIVATE_KEY}" 2>/dev/null || true
}

write_public_key_pem() {
  echo "[offer_manager] Writing public key PEM: ${PUBLIC_PEM}"
  openssl pkey -in "${PRIVATE_KEY}" -pubout -out "${PUBLIC_PEM}"
  chmod 0644 "${PUBLIC_PEM}" 2>/dev/null || true
}

# 1) Ensure private key
if [ ! -f "${PRIVATE_KEY}" ]; then
  echo "[offer_manager] Private key missing -> regenerating..."
  rm -f "${PRIVATE_KEY}" 2>/dev/null || true
  generate_private_key
fi

# 2) Ensure public PEM in /log_server/public_keys for log_server
if [ ! -f "${PUBLIC_PEM}" ]; then
  # If you already have a .pub from earlier runs, just copy it to .pem (it is PEM content in your setup)
  if [ -f "${PUBLIC_PUB}" ]; then
    echo "[offer_manager] Public .pem missing but .pub exists -> copying ${PUBLIC_PUB} -> ${PUBLIC_PEM}"
    cp -f "${PUBLIC_PUB}" "${PUBLIC_PEM}"
    chmod 0644 "${PUBLIC_PEM}" 2>/dev/null || true
  else
    write_public_key_pem
  fi
else
  echo "[offer_manager] Public key PEM OK: ${PUBLIC_PEM}"
fi

# 3) Optional: keep .pub as a copy for backward compatibility
if [ ! -f "${PUBLIC_PUB}" ]; then
  echo "[offer_manager] Creating compatibility .pub copy: ${PUBLIC_PUB}"
  cp -f "${PUBLIC_PEM}" "${PUBLIC_PUB}"
  chmod 0644 "${PUBLIC_PUB}" 2>/dev/null || true
fi

exec "$@"
