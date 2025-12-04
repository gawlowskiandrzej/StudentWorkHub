let logs = [];
let baseOffset = null;
let currentLoadMoreOffset = null;
const AUTO_REFRESH_INTERVAL_MS = 5000;

function parseEntryDate(entry) {
    if (!entry || !entry.date) {
        return null;
    }

    const raw = String(entry.date).trim();
    const date = new Date(raw);

    if (Number.isNaN(date.getTime())) {
        return null;
    }

    return date;
}

function formatLogDate(rawDate) {
    if (!rawDate) {
        return "";
    }

    let value = String(rawDate).trim();

    value = value.replace(/Z+$/, "");

    const firstTIndex = value.indexOf("T");
    if (firstTIndex !== -1) {
        value = value.slice(0, firstTIndex) + " " + value.slice(firstTIndex + 1);
    }

    return value;
}

function getDateKey(date) {
    if (!(date instanceof Date) || Number.isNaN(date.getTime())) {
        return "";
    }

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const day = String(date.getDate()).padStart(2, "0");

    return `${year}-${month}-${day}`;
}

function parseIdList(value) {
    if (!value) {
        return [];
    }

    return value
        .split(",")
        .map(part => part.trim())
        .filter(part => part.length > 0);
}

function sortLogs() {
    logs.sort((a, b) => {
        const da = parseEntryDate(a);
        const db = parseEntryDate(b);

        if (!da || !db) {
            return 0;
        }

        return db - da;
    });
}

function getFilteredLogs() {
    const dateFromInput = document.getElementById("filterDateFrom");
    const dateToInput = document.getElementById("filterDateTo");
    const typeSelect = document.getElementById("filterType");
    const idInput = document.getElementById("filterId");
    const idExtInput = document.getElementById("filterIdExt");
    const tagsInput = document.getElementById("filterTags");

    const dateFromValue = dateFromInput ? dateFromInput.value : "";
    const dateToValue = dateToInput ? dateToInput.value : "";
    const typeValue = typeSelect ? typeSelect.value : "";
    const idValue = idInput ? idInput.value : "";
    const idExtValue = idExtInput ? idExtInput.value : "";
    const tagsValue = tagsInput ? tagsInput.value : "";

    const idsFilter = parseIdList(idValue);
    const idsExtFilter = parseIdList(idExtValue);
    const tagsFilter = parseIdList(tagsValue).map(tag => tag.toLowerCase());

    const fromKey = dateFromValue || "";
    const toKey = dateToValue || "";

    return logs.filter(entry => {
        const entryDate = parseEntryDate(entry);
        const entryDateKey = entryDate ? getDateKey(entryDate) : "";

        if (fromKey && entryDateKey && entryDateKey < fromKey) {
            return false;
        }
        if (toKey && entryDateKey && entryDateKey > toKey) {
            return false;
        }

        if (typeValue && entry.type !== typeValue) {
            return false;
        }

        if (idsFilter.length > 0) {
            const entryServerId = entry.serverId != null ? String(entry.serverId) : "";
            if (!idsFilter.includes(entryServerId)) {
                return false;
            }
        }

        if (idsExtFilter.length > 0) {
            const entryServerIdExt = entry.serverIdExt != null ? String(entry.serverIdExt) : "";
            if (!idsExtFilter.includes(entryServerIdExt)) {
                return false;
            }
        }

        if (tagsFilter.length > 0) {
            const entryTags = Array.isArray(entry.tags)
                ? entry.tags.map(tag => String(tag).toLowerCase())
                : [];

            const hasAllTags = tagsFilter.every(filterTag => entryTags.includes(filterTag));
            if (!hasAllTags) {
                return false;
            }
        }

        return true;
    });
}

function renderTable() {
    const tbody = document.querySelector("#logsTable tbody");
    if (!tbody) {
        return;
    }

    const container = document.querySelector(".table-container");

    let previousScrollTop = 0;
    let previousScrollHeight = 0;
    let isAtTop = true;

    if (container) {
        previousScrollTop = container.scrollTop;
        previousScrollHeight = container.scrollHeight;
        isAtTop = previousScrollTop === 0;
    }

    const filteredLogs = getFilteredLogs();

    tbody.innerHTML = "";

    filteredLogs.forEach(entry => {
        const tr = document.createElement("tr");

        const tdDate = document.createElement("td");
        tdDate.textContent = formatLogDate(entry.date);

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

    if (container) {
        const newScrollHeight = container.scrollHeight;

        if (isAtTop) {
            container.scrollTop = 0;
        } else {
            const heightDiff = newScrollHeight - previousScrollHeight;
            container.scrollTop = previousScrollTop + heightDiff;
        }
    }

    const lastUpdateElement = document.getElementById("lastUpdate");
    if (lastUpdateElement) {
        const now = new Date();
        lastUpdateElement.textContent = "Last update: " + now.toLocaleString();
    }
}

async function fetchInitialLogs() {
    try {
        const response = await fetch("/api/logs");
        if (!response.ok) {
            alert("Failed to fetch initial logs (status " + response.status + ").");
            return;
        }

        const data = await response.json();
        logs = data.entries || [];

        if (typeof data.offset === "number") {
            baseOffset = data.offset;
            currentLoadMoreOffset = baseOffset;
        }

        sortLogs();
        renderTable();
    } catch (error) {
        console.error("Failed to fetch initial logs", error);
        alert("Failed to fetch initial logs. See console for details.");
    }
}

async function checkForNewLogs() {
    try {
        const response = await fetch("/api/logs");
        if (!response.ok) {
            alert("Failed to check for new logs (status " + response.status + ").");
            return;
        }

        const data = await response.json();
        const entries = data.entries || [];
        if (entries.length === 0) {
            return;
        }

        const existingRaw = new Set(logs.map(entry => entry.raw));
        const newEntries = entries.filter(entry => !existingRaw.has(entry.raw));

        if (newEntries.length === 0) {
            return;
        }

        logs = newEntries.concat(logs);

        if (typeof data.offset === "number") {
            baseOffset = data.offset;
            if (currentLoadMoreOffset == null) {
                currentLoadMoreOffset = baseOffset;
            }
        }

        sortLogs();
        renderTable();
    } catch (error) {
        console.error("Failed to check for new logs", error);
        alert("Failed to check for new logs. See console for details.");
    }
}

async function loadOlderLogs() {
    if (currentLoadMoreOffset === null || typeof currentLoadMoreOffset !== "number") {
        return;
    }

    const container = document.querySelector(".table-container");
    let previousScrollHeight = 0;
    let clientHeight = 0;

    if (container) {
        previousScrollHeight = container.scrollHeight;
        clientHeight = container.clientHeight;
    }

    const step = 500;
    let newOffset = currentLoadMoreOffset - step;
    if (newOffset < 0) {
        newOffset = 0;
    }

    try {
        const response = await fetch(`/api/logs?offset=${encodeURIComponent(newOffset)}&limit=${step}`);
        if (!response.ok) {
            alert("Failed to load older logs (status " + response.status + ").");
            return;
        }

        const data = await response.json();
        const entries = data.entries || [];

        if (entries.length === 0) {
            currentLoadMoreOffset = newOffset;
            return;
        }

        const existingRaw = new Set(logs.map(entry => entry.raw));
        const uniqueEntries = entries.filter(entry => !existingRaw.has(entry.raw));

        if (uniqueEntries.length === 0) {
            currentLoadMoreOffset = newOffset;
            return;
        }

        // Starsze logi – dokładamy na koniec
        logs = logs.concat(uniqueEntries);
        currentLoadMoreOffset = newOffset;

        sortLogs();
        renderTable();

        // Skok do granicy: tam, gdzie kończyły się stare logi
        if (container) {
            const newScrollHeight = container.scrollHeight;

            let targetScrollTop = previousScrollHeight;
            const maxScrollTop = newScrollHeight - clientHeight;

            if (targetScrollTop > maxScrollTop) {
                targetScrollTop = maxScrollTop;
            }
            if (targetScrollTop < 0) {
                targetScrollTop = 0;
            }

            container.scrollTop = targetScrollTop;
        }
    } catch (error) {
        console.error("Failed to load older logs", error);
        alert("Failed to load older logs. See console for details.");
    }
}

function setupFilters() {
    const applyButton = document.getElementById("applyFilters");
    const clearButton = document.getElementById("clearFilters");

    if (applyButton) {
        applyButton.addEventListener("click", () => {
            renderTable();
        });
    }

    if (clearButton) {
        clearButton.addEventListener("click", () => {
            const dateFromInput = document.getElementById("filterDateFrom");
            const dateToInput = document.getElementById("filterDateTo");
            const typeSelect = document.getElementById("filterType");
            const idInput = document.getElementById("filterId");
            const idExtInput = document.getElementById("filterIdExt");
            const tagsInput = document.getElementById("filterTags");

            if (dateFromInput) dateFromInput.value = "";
            if (dateToInput) dateToInput.value = "";
            if (typeSelect) typeSelect.value = "";
            if (idInput) idInput.value = "";
            if (idExtInput) idExtInput.value = "";
            if (tagsInput) tagsInput.value = "";

            renderTable();
        });
    }
}

document.addEventListener("DOMContentLoaded", () => {
    setupFilters();

    const loadMoreButton = document.getElementById("loadMore");
    if (loadMoreButton) {
        loadMoreButton.addEventListener("click", () => {
            loadOlderLogs();
        });
    }

    fetchInitialLogs();
    setInterval(checkForNewLogs, AUTO_REFRESH_INTERVAL_MS);
});
