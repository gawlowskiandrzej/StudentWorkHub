const express = require("express");
const puppeteer = require("puppeteer-extra");
const StealthPlugin = require("puppeteer-extra-plugin-stealth");

puppeteer.use(StealthPlugin());

const app = express();
const PORT = 3000;

const userAgents = [
  "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36",
  "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.0 Safari/605.1.15",
  "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:128.0) Gecko/20100101 Firefox/128.0",
];

app.get("/scrape", async (req, res) => {
  const url = req.query.url;
  if (!url) return res.status(400).send("Missing url");

  const randomUA = userAgents[Math.floor(Math.random() * userAgents.length)];

  const browser = await puppeteer.launch({
    headless: true, 
    args: ["--no-sandbox", "--disable-setuid-sandbox"],
  });
  const page = await browser.newPage();

  try {
	  await page.setUserAgent(randomUA);
    await page.setExtraHTTPHeaders({
      "Accept-Language": "en-US,en;q=0.9",
    });

    // Emulacja viewportu
    await page.setViewport({ width: 1366, height: 768 });
	  await page.evaluateOnNewDocument(() => {
      Object.defineProperty(navigator, "platform", { get: () => "Win32" });
    });
	  
	  
    await page.goto(url, { waitUntil: "domcontentloaded", timeout: 0 });
    const content = await page.content();
    res.send(content);
  } catch (err) {
    console.error("Navigation failed:", err);
    res.status(500).send("Failed to load page");
  } finally {
    await browser.close();
  }
});

app.listen(PORT, () => {
  console.log(`Scraper API running on http://localhost:${PORT}`);
});
