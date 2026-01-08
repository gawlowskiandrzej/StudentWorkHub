#!/usr/bin/env bash
set -euo pipefail

echo "[INFO] Backup started at $(date -Iseconds)"

# ---- Validate required env vars ----
: "${PGHOST:?PGHOST is required}"
: "${PGPORT:?PGPORT is required}"
: "${PGDATABASE:?PGDATABASE is required}"
: "${PGUSER:?PGUSER is required}"
: "${PGPASSWORD:?PGPASSWORD is required}"

: "${RESTIC_PASSWORD:?RESTIC_PASSWORD is required}"
RESTIC_TAG="${RESTIC_TAG:-postgres}"

# Repositories (two targets)
REPO_LOCAL="${REPO_LOCAL:-/backups/db_general/restic_repo}"
REPO_HOST="${REPO_HOST:-/repo_host/restic_repo}"

export PGPASSWORD
export RESTIC_PASSWORD

# ---- Create dump (directory format for better dedup) ----
TS="$(date +%Y%m%d_%H%M%S)"
DUMP_DIR="/tmp/pgdump_${PGDATABASE}_${TS}"
mkdir -p "${DUMP_DIR}"

echo "[INFO] Running pg_dump to ${DUMP_DIR} ..."
pg_dump \
  --host="${PGHOST}" \
  --port="${PGPORT}" \
  --username="${PGUSER}" \
  --dbname="${PGDATABASE}" \
  --format=directory \
  --file="${DUMP_DIR}" \
  --jobs=4 \
  --no-owner --no-acl --no-privileges

backup_to_repo() {
  local repo_path="$1"
  local label="$2"

  echo "[INFO] Backing up to ${label}: ${repo_path}"
  mkdir -p "${repo_path}"
  export RESTIC_REPOSITORY="${repo_path}"

  # Init repo if first run
  if [[ ! -f "${RESTIC_REPOSITORY}/config" ]]; then
    echo "[INFO] Initializing restic repo: ${label}"
    restic init
  fi

  # Backup snapshot
  restic backup "${DUMP_DIR}" --tag "${RESTIC_TAG}" --hostname "docker-backup"

  # Retention policy (adjust as you like)
  restic forget --tag "${RESTIC_TAG}" --keep-daily 7 --keep-weekly 4 --keep-monthly 6 --prune
}

backup_to_repo "${REPO_LOCAL}" "docker-volume"
backup_to_repo "${REPO_HOST}"  "host-folder"

# Cleanup
rm -rf "${DUMP_DIR}"

echo "[INFO] Backup finished at $(date -Iseconds)"
