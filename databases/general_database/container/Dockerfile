FROM postgres:17.6-trixie
ENV POSTGRES_INITDB_ARGS="--auth=scram-sha-256 --data-checksums"

ENV TZ="Europe/Warsaw"

COPY --chown=postgres:postgres ./scripts/create_users.sh /docker-entrypoint-initdb.d/0_create_users.sh
RUN chmod +x /docker-entrypoint-initdb.d/*.sh || true

COPY --chown=postgres:postgres ./scripts/prepare_postgre.sql /docker-entrypoint-initdb.d/10_prepare_postgre.sql
COPY --chown=postgres:postgres ./scripts/create_database.ddl.sql /docker-entrypoint-initdb.d/20_create_database.ddl.sql
COPY --chown=postgres:postgres ./scripts/create_procedures.ddl.sql /docker-entrypoint-initdb.d/30_create_procedures.ddl.sql
COPY --chown=postgres:postgres ./scripts/create_functions.ddl.sql /docker-entrypoint-initdb.d/40_create_functions.ddl.sql
COPY --chown=postgres:postgres ./scripts/prefill_users.dml.sql /docker-entrypoint-initdb.d/50_prefill_users.dml.sql

EXPOSE 5432

HEALTHCHECK --interval=30s --timeout=5s --start-period=30s --retries=5 \
    CMD pg_isready -h 127.0.0.1 -U "$POSTGRES_USER" -d "$POSTGRES_DB" || exit 1