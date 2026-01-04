#!/usr/bin/env bash
set -euo pipefail

KEY_DIR="/log_server/public_keys"
PUBLIC_KEY="${KEY_DIR}/offer_manager.pub"

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

# If either key is missing -> regenerate both
if [ ! -f "${PRIVATE_KEY}" ] || [ ! -f "${PUBLIC_KEY}" ]; then
  echo "[offer_manager] Keypair incomplete -> regenerating private+public keys..."

  # Start clean (ignore errors)
  rm -f "${PRIVATE_KEY}" "${PUBLIC_KEY}" 2>/dev/null || true

  # Generate private key
  openssl genpkey -algorithm RSA -pkeyopt rsa_keygen_bits:4096 -out "${PRIVATE_KEY}"
  chmod 0400 "${PRIVATE_KEY}" 2>/dev/null || true

  # Derive public key
  openssl pkey -in "${PRIVATE_KEY}" -pubout -out "${PUBLIC_KEY}"
  chmod 0644 "${PUBLIC_KEY}" 2>/dev/null || true

  echo "[offer_manager] Public key written to: ${PUBLIC_KEY}"
else
  echo "[offer_manager] Keypair OK, skipping generation."
fi

exec "$@"
