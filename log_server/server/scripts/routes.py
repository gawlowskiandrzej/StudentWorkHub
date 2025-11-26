from app_core import app
from app_core import apps_logger
from helpers import parse_log_entry
from flask import render_template, request, jsonify

@app.route("/")
def logs_page():
    return render_template("index.html")

@app.route("/api/logs")
def logs_api():
    try:
        offset = int(request.args.get("offset", 0))
    except ValueError:
        offset = 0

    try:
        limit = int(request.args.get("limit", 100))
    except ValueError:
        limit = 100

    raw_entries = apps_logger.read(offset, limit)
    parsed_entries = [parse_log_entry(e) for e in raw_entries]

    return jsonify({
        "entries": parsed_entries,
        "offset": offset,
        "limit": limit
    })
