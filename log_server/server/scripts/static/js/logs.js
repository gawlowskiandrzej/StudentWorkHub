let logs = [];
let lastKnownFirstRaw = null;
const maxLogsToKeep = 500;

function sortLogs() {
    logs.sort((a, b) => {
        const da = new Date(a.date);
        const db = new Date(b.date);

        if (isNaN(da) || isNaN(db)) {
            return 0;
        }
        return db - da;
    });
}

function renderTable() {
    const tbody = document.querySelector("#logsTable tbody");
    tbody.innerHTML = "";

    logs.forEach(entry => {
        const tr = document.createElement("tr");

        const tdDate = document.createElement("td");
        tdDate.textContent = entry.date;

        const tdType = document.createElement("td");
        tdType.textContent = entry.type;

        const tdServerId = document.createElement("td");
        tdServerId.textContent = entry.serverId;

        const tdServerIdExt = document.createElement("td");
        tdServerIdExt.textContent = entry.serverIdExt;

        const tdTags = document.createElement("td");
        if (entry.tags && entry.tags.length > 0) {
            entry.tags.forEach(tag => {
                const span = document.createElement("span");
                span.className = "tag";
                span.textContent = tag;
                tdTags.appendChild(span);
            });
        }

        const tdMessage = document.createElement("td");
        tdMessage.textContent = entry.message;

        tr.appendChild(tdDate);
        tr.appendChild(tdType);
        tr.appendChild(tdServerId);
        tr.appendChild(tdServerIdExt);
        tr.appendChild(tdTags);
        tr.appendChild(tdMessage);

        tbody.appendChild(tr);
    });

    const lastUpdate = document.getElementById("lastUpdate");
    if (lastUpdate) {
        const now = new Date();
        lastUpdate.textContent = "Last update: " + now.toLocaleString();
    }
}

async function fetchInitialLogs() {
    const response = await fetch("/api/logs?offset=0&limit=100");
    if (!response.ok) {
        return;
    }
    const data = await response.json();
    logs = data.entries || [];
    if (logs.length > maxLogsToKeep) {
        logs = logs.slice(0, maxLogsToKeep);
    }

    sortLogs();

    if (logs.length > 0) {
        lastKnownFirstRaw = logs[0].raw;
    }
    renderTable();
}

async function checkForNewLogs() {
    if (!lastKnownFirstRaw) {
        await fetchInitialLogs();
        return;
    }

    let newLogs = [];
    let offset = 0;
    const limit = 10;
    let foundExisting = false;

    while (!foundExisting) {
        const response = await fetch(`/api/logs?offset=${offset}&limit=${limit}`);
        if (!response.ok) {
            break;
        }

        const data = await response.json();
        const entries = data.entries || [];

        if (entries.length === 0) {
            break;
        }

        const index = entries.findIndex(e => e.raw === lastKnownFirstRaw);

        if (index === -1) {
            newLogs = newLogs.concat(entries);
            offset += limit;
        } else {
            if (index > 0) {
                newLogs = newLogs.concat(entries.slice(0, index));
            }
            foundExisting = true;
        }
    }

    if (newLogs.length > 0) {
        logs = newLogs.concat(logs);
        if (logs.length > maxLogsToKeep) {
            logs = logs.slice(0, maxLogsToKeep);
        }

        sortLogs();

        lastKnownFirstRaw = logs[0].raw;
        renderTable();
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchInitialLogs();
    setInterval(checkForNewLogs, 30000);
});
