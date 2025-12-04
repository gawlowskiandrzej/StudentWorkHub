FROM ubuntu:latest

ENV DEBIAN_FRONTEND=noninteractive
ARG APP_DIR=/log_server
ENV APP_DIR=${APP_DIR}

RUN apt update && apt upgrade -y && \
    apt install -y --no-install-recommends \
        cron \
        python3 \
        python3-venv \
        python3-pip \
        python3-dev \
        build-essential \
        ca-certificates \
        openssl && \
    apt clean && rm -rf /var/lib/apt/lists/*

RUN echo "0 3 * * 0 root apt update && apt upgrade -y && apt autoremove -y && apt clean" \
    > /etc/cron.d/auto-apt-upgrade && \
    chmod 0644 /etc/cron.d/auto-apt-upgrade

RUN useradd --system --no-create-home --shell /usr/sbin/nologin appuser

WORKDIR $APP_DIR

RUN mkdir -p "$APP_DIR/logs" "$APP_DIR/public_keys" && \
    chown -R appuser:appuser "$APP_DIR/logs" && \
    chmod 770 "$APP_DIR/logs" && \
    chmod 1777 "$APP_DIR/public_keys"

COPY ./scripts/requirements.txt $APP_DIR/

RUN python3 -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"

RUN pip install --upgrade pip && \
    pip install --no-input -r requirements.txt

# WAŻNE: kopiuj cały katalog, nie "*"
COPY ./scripts/ $APP_DIR/

RUN mv $APP_DIR/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh && chown appuser:appuser /entrypoint.sh

RUN chown -R appuser:appuser $APP_DIR

USER appuser

EXPOSE 54000
EXPOSE 54001

ENTRYPOINT ["/entrypoint.sh"]
CMD ["python", "log_server.py"]
