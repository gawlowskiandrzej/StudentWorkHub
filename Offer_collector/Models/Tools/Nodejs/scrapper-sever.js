const express = require("express");
const puppeteer = require("puppeteer-extra");
const StealthPlugin = require("puppeteer-extra-plugin-stealth");

puppeteer.use(StealthPlugin());

const app = express();
const PORT = 3000;

app.get("/scrape", async (req, res) => {
  const url = req.query.url;
  if (!url) return res.status(400).send("Missing url");

  const browser = await puppeteer.launch({ headless: true });
  const page = await browser.newPage();

  try {
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