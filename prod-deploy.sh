#!/usr/bin/env bash
set -euo pipefail

# -----------------------------------
# StudentWorkHub - Production Deploy
# -----------------------------------
# Run as root from the repository root (where docker-compose-prod.yml and Caddyfile are located).
#
# WARNING:
# - This script REMOVES the entire /srv/StudentWorkHub directory (configs/env/backups) and recreates it.
# - If you have anything valuable there (especially backups), copy it out first.
#
# It will:
# - rm -rf /srv/StudentWorkHub
# - create /srv/StudentWorkHub/{env,backups/db_general,configs}
# - copy *.env.example -> /srv/StudentWorkHub/env/*.env
# - copy (overwrite) Caddyfile -> /srv/StudentWorkHub/configs/Caddyfile
# - generate strong random passwords (>= 32 chars; upper/lower/digit/special guaranteed)
# - replace specific KEY=... lines in env files
# - optionally set GEMINI_KEY (if empty, keeps placeholder)
# - docker compose build + up -d --scale worker=4
#
# Logs:
# - Full output is saved to: /srv/StudentWorkHub/build.log

BASE_DIR="/srv/StudentWorkHub"
ENV_DIR="${BASE_DIR}/env"
BACKUPS_DIR="${BASE_DIR}/backups"
CONFIGS_DIR="${BASE_DIR}/configs"
BUILD_LOG="${BASE_DIR}/build.log"

COMPOSE_FILE="docker-compose-prod.yml"

# Source example files (relative to repo root)
SRC_DB_BACKUP_EXAMPLE="./databases/db_backup/.env.example"
SRC_DB_GENERAL_EXAMPLE="./databases/general_database/container/.env.example"
SRC_DB_OFFERS_EXAMPLE="./databases/offers_database/container/.env.example"
SRC_MAIN_EXAMPLE="./main.env.example"

# Offer collector config (relative to repo root)
SRC_OFFER_MANAGER_APPSETTINGS="./offer_collector/offer_manager/appsettings.json"

# Required config file (relative to repo root)
SRC_CADDYFILE="./Caddyfile"

require_root() {
  if [[ "${EUID}" -ne 0 ]]; then
    echo "ERROR: This script must be run as root." >&2
    exit 1
  fi
}

require_repo_files() {
  local missing=0

  for f in "$COMPOSE_FILE" \
           "$SRC_DB_BACKUP_EXAMPLE" "$SRC_DB_GENERAL_EXAMPLE" "$SRC_DB_OFFERS_EXAMPLE" "$SRC_MAIN_EXAMPLE" \
           "$SRC_OFFER_MANAGER_APPSETTINGS" \
           "$SRC_CADDYFILE"; do
    if [[ ! -f "$f" ]]; then
      echo "ERROR: Missing required file: $f" >&2
      missing=1
    fi
  done

  if [[ "$missing" -ne 0 ]]; then
    exit 1
  fi
}

escape_sed_repl() {
  # Escape \, &, and the delimiter |
  echo -n "$1" | sed -e 's/[\/&|\\]/\\&/g'
}

escape_sed_pattern() {
  # Escape regex meta chars for sed search patterns
  # ([], \, /, ., ^, $, *, +, ?, { }, ( ), |)
  echo -n "$1" | sed -e 's/[][\/.\^$*+?{}()|\\]/\\&/g'
}

replace_placeholder_in_file() {
  # Replace ALL occurrences of a placeholder string in a file
  local file="$1"
  local placeholder="$2"
  local value="$3"

  local escaped_placeholder
  local escaped_value
  escaped_placeholder="$(escape_sed_pattern "$placeholder")"
  escaped_value="$(escape_sed_repl "$value")"

  sed -i -E "s|${escaped_placeholder}|${escaped_value}|g" "$file"
}


set_env_var() {
  # Replace entire KEY=... line; if missing, append KEY=value
  local file="$1"
  local key="$2"
  local value="$3"

  local escaped_value
  escaped_value="$(escape_sed_repl "$value")"

  if grep -qE "^${key}=" "$file"; then
    sed -i -E "s|^${key}=.*$|${key}=${escaped_value}|" "$file"
  else
    printf '%s=%s\n' "$key" "$value" >> "$file"
  fi
}

generate_strong_password() {
  # Strong password:
  # - length >= 32 (or provided length if >= 20)
  # - guaranteed: upper, lower, digit, special
  # - avoids quotes/whitespace to keep .env safe
  local length="${1:-32}"
  if [[ "$length" -lt 20 ]]; then
    length=20
  fi

  local specials='!@#%^_-+=:.,'
  local charset="A-Za-z0-9${specials}"

  while true; do
    local pwd
    pwd="$(
      LC_ALL=C tr -dc "$charset" </dev/urandom | head -c "$length" || true
    )"

    if [[ "${#pwd}" -ge "$length" ]] \
      && [[ "$pwd" =~ [A-Z] ]] \
      && [[ "$pwd" =~ [a-z] ]] \
      && [[ "$pwd" =~ [0-9] ]] \
      && [[ "$pwd" =~ [\!\@\#\%\^\_\-\+\=\:\.\,] ]]; then
      echo -n "$pwd"
      return 0
    fi
  done
}

maybe_start_docker() {
  if command -v systemctl >/dev/null 2>&1; then
    if systemctl list-unit-files | grep -qE '^docker\.service'; then
      systemctl is-active --quiet docker || systemctl start docker
      return 0
    fi
  fi

  if command -v service >/dev/null 2>&1; then
    service docker status >/dev/null 2>&1 || service docker start >/dev/null 2>&1 || true
  fi
}

main() {
  require_root

  local script_dir
  script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
  cd "$script_dir"

  require_repo_files

  # Optional Gemini key prompt (only when interactive)
  local gemini_key=""
  if [[ -t 0 ]]; then
    read -r -p "Enter GEMINI_KEY (leave blank to keep placeholder): " gemini_key || true
  fi

  # Start fresh: remove entire base directory
  rm -rf "$BASE_DIR"

  # Recreate required directories
  mkdir -p "$ENV_DIR" "$BACKUPS_DIR/db_general" "$CONFIGS_DIR"

  # Redirect all output to build log (keep console minimal)
  exec 3>&1
  exec >"$BUILD_LOG" 2>&1

  echo "=== StudentWorkHub deploy started: $(date -Iseconds) ==="
  echo "Repository directory: ${script_dir}"
  echo "Compose file:         ${COMPOSE_FILE}"
  echo "Env file for Compose: ${ENV_DIR}/main.env"
  echo "Build log:            ${BUILD_LOG}"
  echo "Base directory reset: ${BASE_DIR}"
  echo

  # Copy example env files into /srv/StudentWorkHub/env with correct names
  cp -f "$SRC_DB_BACKUP_EXAMPLE"   "${ENV_DIR}/db_backup.env"
  cp -f "$SRC_DB_GENERAL_EXAMPLE"  "${ENV_DIR}/db_general.env"
  cp -f "$SRC_DB_OFFERS_EXAMPLE"   "${ENV_DIR}/db_offers.env"
  cp -f "$SRC_MAIN_EXAMPLE"        "${ENV_DIR}/main.env"

  # Copy (overwrite) Caddyfile into configs
  cp -f "$SRC_CADDYFILE" "${CONFIGS_DIR}/Caddyfile"
  chmod 644 "${CONFIGS_DIR}/Caddyfile"

  # Generate strong passwords
  local backup_user_password
  local restic_password
  local general_postgres_password
  local offers_postgres_password
  local general_app_password
  local offers_app_password

  backup_user_password="$(generate_strong_password 32)"
  restic_password="$(generate_strong_password 32)"
  general_postgres_password="$(generate_strong_password 32)"
  offers_postgres_password="$(generate_strong_password 32)"
  general_app_password="$(generate_strong_password 32)"
  offers_app_password="$(generate_strong_password 32)"

  # Update db_backup.env
  set_env_var "${ENV_DIR}/db_backup.env" "PGPASSWORD" "$backup_user_password"
  set_env_var "${ENV_DIR}/db_backup.env" "RESTIC_PASSWORD" "$restic_password"

  # Update db_general.env
  set_env_var "${ENV_DIR}/db_general.env" "POSTGRES_PASSWORD" "$general_postgres_password"
  set_env_var "${ENV_DIR}/db_general.env" "BACKUP_DB_PASSWORD" "$backup_user_password"
  set_env_var "${ENV_DIR}/db_general.env" "APP_DB_PASSWORD" "$general_app_password"

  # Update db_offers.env
  set_env_var "${ENV_DIR}/db_offers.env" "POSTGRES_PASSWORD" "$offers_postgres_password"
  set_env_var "${ENV_DIR}/db_offers.env" "APP_DB_PASSWORD" "$offers_app_password"

  # Update main.env
  set_env_var "${ENV_DIR}/main.env" "POSTGRES_PASSWORD" "$offers_app_password"
  set_env_var "${ENV_DIR}/main.env" "GENERAL_PASSWORD" "$general_app_password"

  # Optional GEMINI_KEY
  if [[ -n "${gemini_key}" ]]; then
    set_env_var "${ENV_DIR}/main.env" "GEMINI_KEY" "$gemini_key"
  fi

  # Update offer_manager appsettings.json placeholders (used during image build)
  replace_placeholder_in_file "$SRC_OFFER_MANAGER_APPSETTINGS" "[DB_OFFERS_PASSWORD]" "$offers_app_password"
  replace_placeholder_in_file "$SRC_OFFER_MANAGER_APPSETTINGS" "[DB_GENERAL_PASSWORD]" "$general_app_password"
  if [[ -n "${gemini_key}" ]]; then
    replace_placeholder_in_file "$SRC_OFFER_MANAGER_APPSETTINGS" "[GEMINI_API_KEY]" "$gemini_key"
  fi


  # Permissions
  chown -R root:root "$BASE_DIR"
  chmod 700 "$ENV_DIR"
  chmod 600 "$ENV_DIR"/*.env
  chmod 700 "$BACKUPS_DIR" "$BACKUPS_DIR/db_general" || true
  chmod 755 "$CONFIGS_DIR" || true

  # Start Docker if possible
  maybe_start_docker

  # Build and run compose (use main.env for ${...} interpolation)
  docker compose --env-file "${ENV_DIR}/main.env" -f "$COMPOSE_FILE" build
  docker compose --env-file "${ENV_DIR}/main.env" -f "$COMPOSE_FILE" up -d --scale worker=4

  echo
  echo "=== StudentWorkHub deploy finished: $(date -Iseconds) ==="
  echo "Build log saved to: ${BUILD_LOG}"

  # Minimal console success output
  echo "SUCCESS. Build log: ${BUILD_LOG}" >&3
}

# Minimal console output on failure (details are in build.log if it exists)
if ! main; then
  echo "FAILED. Build log: /srv/StudentWorkHub/build.log" >&2
  exit 1
fi
