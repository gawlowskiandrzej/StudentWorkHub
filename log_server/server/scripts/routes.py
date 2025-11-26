import time
from app_core import app
from app_core import apps_logger
from helpers import parse_log_entry
from flask import render_template, request, jsonify
from app_core import logger

RATE_LIMIT_WINDOW_SECONDS = 1.0 
RATE_LIMIT_MAX_REQUESTS_PER_WINDOW = 5
_ip_requests = {} 

@app.before_request
def global_rate_limit():
    """
    Simple global rate limiting:
    - max RATE_LIMIT_MAX_REQUESTS_PER_WINDOW requests
      in RATE_LIMIT_WINDOW_SECONDS per IP
    - applies to ALL endpoints, including static files
    """
    client_ip = request.remote_addr or "unknown"
    now = time.time()

    info = _ip_requests.get(client_ip)

    if info is None:
        _ip_requests[client_ip] = {"start": now, "count": 1}
        return

    window_start = info["start"]
    count = info["count"]

    if now - window_start < RATE_LIMIT_WINDOW_SECONDS:
        if count >= RATE_LIMIT_MAX_REQUESTS_PER_WINDOW:
            logger.warning("Rate limit hit for %s on %s (count=%d, limit=%d)",
                            client_ip,
                            request.path,
                            count,
                            RATE_LIMIT_MAX_REQUESTS_PER_WINDOW)
            return jsonify({"error": "Too Many Requests"}), 429

        info["count"] = count + 1
    else:
        _ip_requests[client_ip] = {"start": now, "count": 1}
    
@app.route("/")
def logs_page():
    return render_template("index.html")

@app.route("/api/logs")
def logs_api():
    try:
        offset = int(request.args.get("offset", 0))
    except ValueError:
        offset = apps_logger.get_current_offset()

    try:
        limit = int(request.args.get("limit", 100))
    except ValueError:
        limit = 100

    raw_entries = apps_logger.read(offset-limit, limit)
    parsed_entries = [parse_log_entry(e) for e in raw_entries]

    return jsonify({
        "entries": parsed_entries,
        "offset": offset,
        "limit": limit
    })
