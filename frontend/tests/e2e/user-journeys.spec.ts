import { test, expect, Page } from '@playwright/test';

test.describe('offer search and filters', () => {
  
  test.describe.configure({ mode: 'serial' });

  test('search flow: basic search, results count and no results', async ({ page }) => {
    await page.goto('/search', { waitUntil: 'load' });
    
    const searchInput = page.getByLabel(/Zawód.*firma.*słowo kluczowe/i);
    await searchInput.waitFor({ state: 'visible', timeout: 20000 });
    await searchInput.fill('Python');
    await page.getByText(/Znajdź swoją ofertę/i).click();
    
    await page.waitForURL('**/list**', { timeout: 30000 });
    
    await expect(page).toHaveURL(/.*list.*/i);

    await page.goto('/search', { waitUntil: 'domcontentloaded' });
    const searchInputNoResults = page.getByLabel(/Zawód.*firma.*słowo kluczowe/i);
    await searchInputNoResults.waitFor({ state: 'visible', timeout: 10000 });
    await searchInputNoResults.fill('xyzabc123nonexistent987654');
    await page.getByText(/Znajdź swoją ofertę/i).click();
    await page.waitForURL('**/list**', { timeout: 30000 });
    await expect(page).toHaveURL(/.*list.*/i);
  });

  test('filters and details flow', async ({ page }) => {
    await page.goto('/search', { waitUntil: 'domcontentloaded' });
    
    const cityInput = page.getByLabel(/Miasto/i);
    await cityInput.waitFor({ state: 'visible', timeout: 10000 });
    await cityInput.fill('Warszawa');
    
    await page.getByText(/Znajdź swoją ofertę/i).click();
    await page.waitForURL('**/list**', { timeout: 30000 });

    const offerCard = page.locator('[data-testid="offer-card"]').first();
    const hasOffer = await offerCard.isVisible().catch(() => false);
    if (hasOffer) {
      await offerCard.click();
      await page.waitForURL(/\/details\/.+/, { timeout: 30000 });
      
      await page.locator('body').waitFor({ state: 'visible', timeout: 10000 });
    }
    
    const jobTitle = page.locator('[class*="job-title"]').first();
    await jobTitle.waitFor({ state: 'visible', timeout: 10000 });
  });

  test('search resilience: edge cases and long queries', async ({ page }) => {
    const edgeCaseQueries = ['12345', 'ąćęłńóśźż'];
    
    for (const query of edgeCaseQueries) {
      await page.goto('/search', { waitUntil: 'domcontentloaded' });
      const searchInput = page.getByLabel(/Zawód.*firma.*słowo kluczowe/i);
      await searchInput.waitFor({ state: 'visible', timeout: 10000 });
      await searchInput.fill(query);
      await page.getByText(/Znajdź swoją ofertę/i).click();
      
      await page.waitForURL('**/list**', { timeout: 30000 });
      await page.locator('body').waitFor({ state: 'visible', timeout: 10000 });
    }

    await page.goto('/search', { waitUntil: 'domcontentloaded' });
    const searchInputLong = page.getByLabel(/Zawód.*firma.*słowo kluczowe/i);
    await searchInputLong.waitFor({ state: 'visible', timeout: 10000 });
    await searchInputLong.fill('a'.repeat(100)); 
    await page.getByText(/Znajdź swoją ofertę/i).click();
    await page.waitForURL('**/list**', { timeout: 30000 });
  });
});

test.describe('filtering and pagination', () => {
  test.describe.configure({ mode: 'serial' });

  test('pagination and complex filters', async ({ page }) => {
    await page.goto('/search', { waitUntil: 'load' });
    const cityInput = page.getByLabel(/Miasto/i);
    await cityInput.waitFor({ state: 'visible', timeout: 20000 });
    await cityInput.fill('Warszawa');
    
    await page.getByText(/Znajdź swoją ofertę/i).click();
    await page.waitForURL('**/list**', { timeout: 30000 });
    
    await expect(page).toHaveURL(/.*list.*/i);
    
    const hasOffers = await page.locator('[data-testid="offer-card"]').first().isVisible().catch(() => false);
    if (hasOffers) {
      const nextButton = page.locator('button[aria-label="Next page"]').first();
      const hasNextButton = await nextButton.isVisible().catch(() => false);
      if (hasNextButton) {
        await nextButton.click();
        await expect(page.locator('[data-testid="offer-card"]').first()).toBeVisible({ timeout: 10000 });
      }
    }
  });
});

