> ℹ This is still development version, used as a standalone server to test working with offers database, future versions will be developed with centralized scripts, security and deployment and use safer authentication methods.

# Configuration and deployment of offers_database container #
## Deployment ##
1. Change `.env.example` to `.env` and change value for `POSTGRES_PASSWORD`.
2. Open console in **container** folder.
3. Run:
   ```powershell
   docker compose build
   docker compose up
   ```
## Rebuilding database ##
In case of **scripts** changing to test new changes, we need to recreate the container.
> ⚠ This will delete all container data, including data stored on the host.

1. Destroy the container:
   ```powershell
   docker compose down
   ```
2. Destroy attached volume
    ```powershell
    docker volume rm pgdata
    ```
3. Rebuild the container with new scripts content:
   ```powershell
   docker compose up
   ```
## Database communication ##
From now on you should use provided library to communicate with database. You can learn more from README.md in `offers_database/lib` folder.

# Container creation details #
## Dockerfile ##
- `FROM postgres:17.6-trixie` - uses version **17.6** based on **Debian trixie**, instead of **latest** to avoid using different versions between developers.
- `ENV POSTGRES_INITDB_ARGS="--auth=scram-sha-256 --data-checksums"` - Enforces **SCRAM-SHA-256** algorithm to secure authentication, and enables checkums on database data to allow for faster error detection.
- `COPY --chown=postgres:postgres xyz xyz` - Copies .sql scripts from `scripts` folder, to `docker-entrypoint-initdb.d`. PostgreSQL will run those scripts while `docker compose up`.
- `EXPOSE 5432` - Informs that the container wants to use 5432 port.
- `HEALTHCHECK --interval=30s --timeout=5s --start-period=30s --retries=5 CMD pg_isready -h 127.0.0.1 -U "$POSTGRES_USER" -d "$POSTGRES_DB" || exit 1` - Tests every 30 seconds *(after first 30 seconds)* if database is up. Uses `POSTGRES_USER` and `POSTGRES_DB` variables from .env file. If container doesnt respond it is marked by `unhealthy` status.

## 00_hardening.sql ##
- `ALTER SYSTEM SET password_encryption = 'scram-sha-256';` - Sets **SCRAM-SHA-256** as default hashing algorith for new passwords.
- `ALTER SYSTEM SET log*` - Enables log collection, confogures log preservation, rotation, enables logging connection creation/deletion and log format.
- `ALTER SYSTEM SET shared_preload_libraries = 'pg_stat_statements';` - Enables `pg_stat` module to allow analysis of statements generated load.
- `CREATE EXTENSION IF NOT EXISTS pg_stat_statements;` - Creates extension for `pg_stat` module if it doesn't already exist in database.
- `ALTER SYSTEM SET log_min_duration_statement = 3000;` - Logs every statement that takes more than 3s, to easier catch abuse, and bottlenecks.
- `SELECT pg_reload_conf();` - Reloads **Postgre** configuration.

## Docker-compose.yaml ##
- `context: .` - Searches for files in the same directory.
- `image: local/postgres-secure:17.6` - `compose build` will create new image on local system with `postgres-secure:17.6` name.
- `env_file: ./.env` - Uses `.env` file from current directory.
- `ports: - "5432:5432"` - Exposes **5432** port on host system.
- `volumes: - pgdata:/var/lib/postgresql/data` - Mounts `pgdata` volume.
- `volumes: - ./scripts:/docker-entrypoint-initdb.d:ro` - Mounts `scripts` folder as a read-only (**:ro**) directory.
- `volumes: pgdata:` - Creates `pgdata` volume.

## create_database.ddl.sql ##
This scripts creates tables, relations, indexes, etc.

## sample_data.dml.sql ##
This scripts fills previously created database with 22 fake external offers.
