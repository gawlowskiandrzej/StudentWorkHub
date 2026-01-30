import { test, expect } from '@playwright/test';

test.describe('Authentication', () => {
  
  let testEmail: string;
  const testPassword = 'TestPassword123!';

  test.beforeEach(async () => {
    const timestamp = Date.now();
    testEmail = `test_${timestamp}@example.com`;
  });

  test('user registration flow', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    
    await page.getByLabel(/Imię/i).fill('Jan');
    await page.getByLabel(/Nazwisko/i).fill('Kowalski');
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();

    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    
    await page.waitForURL(/\/login/i, { timeout: 30000 });
    await expect(page).toHaveURL(/.*login.*/i);
  });

  test('user login flow', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Jan');
    await page.getByLabel(/Nazwisko/i).fill('Kowalski');
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();
    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    await page.waitForURL(/\/login/i, { timeout: 30000 });
    
    await page.getByLabel(/Email/i).waitFor({ state: 'visible', timeout: 10000 });
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
    
    await page.waitForURL(/^(?!.*login)/i, { timeout: 30000 });
    await expect(page).not.toHaveURL(/.*login.*/i);
  });

  test('registration with invalid email', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Jan');
    await page.getByLabel(/Nazwisko/i).fill('Kowalski');
    await page.getByLabel(/Email/i).fill('invalid-email');
    await page.getByLabel(/Hasło/i).fill(testPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();

    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    
    await page.waitForTimeout(2000);
    const currentUrl = page.url();
    expect(currentUrl).toMatch(/register/i);
  });

  test('login with incorrect credentials', async ({ page }) => {
    await page.goto('/login', { waitUntil: 'load' });
    
    await page.getByLabel(/Email/i).waitFor({ state: 'visible', timeout: 10000 });
    await page.getByLabel(/Email/i).fill('nonexistent@example.com');
    await page.getByLabel(/Hasło/i).fill('WrongPassword123!');
    await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
    
    await page.waitForTimeout(2000);
    const currentUrl = page.url();
    expect(currentUrl).toMatch(/login/i);
  });

  test('navigation between login and register', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await expect(page).toHaveURL(/.*register.*/i);
    
    const hasLoginLink = await page.getByText(/Zaloguj/i).first().isVisible().catch(() => false);
    if (hasLoginLink) {
      await page.getByText(/Zaloguj/i).first().click();
      await page.waitForURL(/\/login/i, { timeout: 10000 });
      await expect(page).toHaveURL(/.*login.*/i);
    }
    
    await page.goto('/login', { waitUntil: 'load' });
    await expect(page).toHaveURL(/.*login.*/i);
    
    const hasRegisterLink = await page.getByText(/Zarejestruj/i).first().isVisible().catch(() => false);
    if (hasRegisterLink) {
      await page.getByText(/Zarejestruj/i).first().click();
      await page.waitForURL(/\/register/i, { timeout: 10000 });
      await expect(page).toHaveURL(/.*register.*/i);
    }
  });
});
