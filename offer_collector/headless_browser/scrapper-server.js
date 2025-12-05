const express = require("express");
const puppeteer = require("puppeteer-extra");
const StealthPlugin = require("puppeteer-extra-plugin-stealth");

puppeteer.use(StealthPlugin());

const app = express();
const PORT = 3000;

let browser;

(async () => {
  browser = await puppeteer.launch({
    headless: true,
    args: [
    '--no-sandbox',
    '--disable-setuid-sandbox',
    '--disable-dev-shm-usage',
    '--disable-accelerated-2d-canvas',
    '--disable-gpu',
    '--disable-notifications',
    '--disable-background-networking',
    '--disable-default-apps',
    '--disable-extensions',
    '--disable-sync',
    '--disable-translate',
    '--metrics-recording-only',
    '--mute-audio'
  ]
  });

  console.log("Browser launched");
})();

app.get("/health", (req, res) => {
    res.status(200).send("OK");
});


app.get("/scrape", async (req, res) => {
  if (!browser) return res.status(503).send("Browser not ready");

  const url = req.query.url;
  if (!url) return res.status(400).send("Missing url");

  const page = await browser.newPage();
  
  await page.setRequestInterception(true);
  page.on("request", (req) => {
    const blocked = ["image", "stylesheet", "font", "media"];

    if (blocked.includes(req.resourceType())) {
      req.abort();
    } else {
      req.continue();
    }
  });
  
  try {
    await page.goto(url, { waitUntil: "domcontentloaded", timeout: 0 });
    const html = await page.content();
    res.send(html);
  } catch (e) {
    res.status(500).send("Failed to load page");
  } finally {
    await page.close();
  }
});

app.listen(3000, () => console.log("Scraper API running"));