FROM postgres:17.6-trixie
ENV POSTGRES_INITDB_ARGS="--auth=scram-sha-256 --data-checksums"

ENV TZ="Europe/Warsaw"

COPY --chown=postgres:postgres ./scripts/00_hardening.sql /docker-entrypoint-initdb.d/00_hardening.sql
COPY --chown=postgres:postgres ./scripts/create_database.ddl.sql /docker-entrypoint-initdb.d/10_create_database.ddl.sql

EXPOSE 5432

HEALTHCHECK --interval=30s --timeout=5s --start-period=30s --retries=5 \
    CMD pg_isready -h 127.0.0.1 -U "$POSTGRES_USER" -d "$POSTGRES_DB" || exit 1