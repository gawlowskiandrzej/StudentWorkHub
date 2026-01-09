FROM postgres:17.6-trixie

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        restic cron bash tzdata coreutils ca-certificates \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY backup.sh /app/backup.sh
RUN chmod +x /app/backup.sh

COPY crontab /etc/cron.d/db_backup
RUN chmod 0644 /etc/cron.d/db_backup \
    && sed -i 's/\r$//' /etc/cron.d/db_backup \
    && touch /var/log/cron.log

CMD ["bash", "-lc", "cron -f"]
