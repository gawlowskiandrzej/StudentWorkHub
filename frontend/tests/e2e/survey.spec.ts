import { test, expect } from '@playwright/test';

test.describe('prefferences survey', () => {
  
  test.describe.configure({ mode: 'serial' });

  let sharedEmail: string;
  let sharedPassword = 'Password123!';

  test.beforeAll(async () => {
    const timestamp = Date.now();
    sharedEmail = `test_${timestamp}@example.com`;
  });

  test('user authentication and survey flow', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Jan');
    await page.getByLabel(/Nazwisko/i).fill('Kowalski');
    await page.getByLabel(/Email/i).fill(sharedEmail);
    await page.getByLabel(/Hasło/i).fill(sharedPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();

    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    await page.waitForURL(/\/login/i, { timeout: 30000 });
    
    await page.getByLabel(/Email/i).waitFor({ state: 'visible', timeout: 10000 });
    await page.getByLabel(/Email/i).fill(sharedEmail);
    await page.getByLabel(/Hasło/i).fill(sharedPassword);
    await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
    await page.waitForURL(/^(?!.*login)/i, { timeout: 30000 });
    
    await page.goto('/profile-creation', { waitUntil: 'domcontentloaded' });
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ state: 'visible', timeout: 30000 });
    
    const majorSelect = page.locator('button[role="combobox"]').first();
    await majorSelect.waitFor({ state: 'visible', timeout: 10000 });
    await majorSelect.click();
    await page.locator('[role="option"]').first().waitFor({ state: 'visible', timeout: 10000 });
    await page.locator('[role="option"]').first().click();
    await page.getByText(/^Dalej$/i).click();
    
    await page.getByText(/Jakie są twoje doświadczenia zawodowe/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Doświadczenie zawodowe/i).fill('JavaScript');
    await page.getByLabel(/Miesiące/i).fill('24');
    await page.getByText(/Zapisz/i).click();
    await page.getByText(/^Dalej$/i).click();

    await page.getByText(/Ile chcesz zarabiać/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByText(/^Dalej$/i).click(); // Earnings
    await page.getByText(/^Dalej$/i).click(); // Job Status
    await page.getByText(/^Dalej$/i).click(); // Languages
    await page.getByText(/^Dalej$/i).click(); // Location
    
    await page.getByText(/Przejrzyj swój profil/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByText(/Zakończ/i).click();

    await page.waitForURL(/\/(options|list|search)/, { timeout: 30000 });
  });

  test('survey navigation and persistence', async ({ page }) => {
    await page.goto('/login', { waitUntil: 'domcontentloaded' });
    if (page.url().includes('/login')) {
        await page.getByLabel(/Email/i).waitFor({ state: 'visible', timeout: 10000 });
        await page.getByLabel(/Email/i).fill(sharedEmail);
        await page.getByLabel(/Hasło/i).fill(sharedPassword);
        await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
        await page.waitForURL(/^(?!.*login)/i, { timeout: 30000 });
    }
    
    await page.goto('/profile-creation', { waitUntil: 'domcontentloaded' });
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ state: 'visible', timeout: 30000 });

    const majorSelect = page.locator('button[role="combobox"]').first();
    await majorSelect.waitFor({ state: 'visible', timeout: 10000 });
    await majorSelect.click();
    await page.locator('[role="option"]').first().waitFor({ state: 'visible', timeout: 10000 });
    await page.locator('[role="option"]').first().click();
    const selectedText = await majorSelect.innerText();
    
    await page.getByText(/^Dalej$/i).click();
    await page.getByText(/Jakie są twoje doświadczenia zawodowe/i).waitFor({ state: 'visible', timeout: 30000 });
    
    await page.getByText(/Wstecz/i).click();
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ state: 'visible', timeout: 10000 });
    await expect(majorSelect).toContainText(selectedText);
    
    await page.reload();
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ state: 'visible', timeout: 30000 });
    await expect(majorSelect).toContainText(selectedText);
  });
});
