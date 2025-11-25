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
        build-essential && \
    apt clean && rm -rf /var/lib/apt/lists/*

RUN echo "0 3 * * 0 root apt update && apt upgrade -y && apt autoremove -y && apt clean" \
    > /etc/cron.d/auto-apt-upgrade && \
    chmod 0644 /etc/cron.d/auto-apt-upgrade

RUN useradd --system --no-create-home --shell /usr/sbin/nologin appuser

WORKDIR $APP_DIR

COPY ./scripts/requirements.txt $APP_DIR/

RUN python3 -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"

RUN pip install --upgrade pip && \
    pip install --no-input -r requirements.txt

COPY ./scripts/ $APP_DIR/

RUN chown -R appuser:appuser $APP_DIR

USER appuser

EXPOSE 54000
EXPOSE 54001

CMD ["python", "log_server.py"]
