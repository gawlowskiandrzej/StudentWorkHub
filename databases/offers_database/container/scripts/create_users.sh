#!/usr/bin/env sh
set -eu

# Fail fast if required env vars are missing
: "${POSTGRES_DB:?POSTGRES_DB is required}"
: "${POSTGRES_USER:?POSTGRES_USER is required}"

: "${APP_DB_USER:?APP_DB_USER is required}"
: "${APP_DB_PASSWORD:?APP_DB_PASSWORD is required}"

echo "[init] Creating roles:  ${APP_DB_USER}"

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<SQL
-- Security baseline: don't let random roles connect/create by default
REVOKE CONNECT ON DATABASE "$POSTGRES_DB" FROM PUBLIC;

-- public schema: by default PUBLIC can CREATE there - we don't want that
REVOKE CREATE ON SCHEMA public FROM PUBLIC;

DO \$\$
BEGIN
    -- Create or update app role
    IF NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = '${APP_DB_USER}') THEN
        EXECUTE format('CREATE ROLE %I LOGIN PASSWORD %L', '${APP_DB_USER}', '${APP_DB_PASSWORD}');
    ELSE
        EXECUTE format('ALTER ROLE %I WITH LOGIN PASSWORD %L', '${APP_DB_USER}', '${APP_DB_PASSWORD}');
    END IF;
END
\$\$;

-- Allow users to connect
GRANT CONNECT ON DATABASE "$POSTGRES_DB" TO "${APP_DB_USER}";

-- Allow use public schema (but not create objects)
GRANT USAGE ON SCHEMA public TO "${APP_DB_USER}";

-- APP USER: execute-only on routines (functions/procedures) in public schema
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO "${APP_DB_USER}";
GRANT EXECUTE ON ALL PROCEDURES IN SCHEMA public TO "${APP_DB_USER}";

-- Ensure future routines also get execute (created by the DB owner)
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO "${APP_DB_USER}";
SQL

echo "[init] Roles created/updated."
