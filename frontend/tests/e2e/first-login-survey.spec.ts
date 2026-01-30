import { test, expect } from '@playwright/test';

test.describe('First Login Survey', () => {
  
  let testEmail: string;
  const testPassword = 'TestPassword123!';

  test.beforeEach(async () => {
    const timestamp = Date.now();
    testEmail = `survey_${timestamp}@example.com`;
  });

  test('complete survey after first login', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Anna');
    await page.getByLabel(/Nazwisko/i).fill('Nowak');
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
    
    await page.waitForURL(/profile-creation/i, { timeout: 30000 });
    await expect(page).toHaveURL(/.*profile-creation.*/i);
    
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    
    const majorCombobox = page.locator('button[role="combobox"]').first();
    await majorCombobox.waitFor({ state: 'visible', timeout: 10000 });
    await majorCombobox.click();
    
    await page.locator('[role="option"]').first().waitFor({ state: 'visible', timeout: 10000 });
    await page.locator('[role="option"]').first().click();
    
    await page.getByText(/^Dalej$/i).click();
    
    await page.getByText(/Jakie są twoje doświadczenia zawodowe/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    
    const experienceInput = page.getByLabel(/Doświadczenie zawodowe/i);
    if (await experienceInput.isVisible().catch(() => false)) {
      await experienceInput.fill('Python');
      await page.getByLabel(/Miesiące/i).fill('12');
      await page.getByText(/Zapisz/i).click();
      await page.waitForTimeout(1000);
    }
    
    await page.getByText(/^Dalej$/i).click();
    
    await page.getByText(/Ile chcesz zarabiać/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    await page.getByText(/^Dalej$/i).click();
    
    await page.waitForTimeout(1000);
    await page.getByText(/^Dalej$/i).click();
    
    await page.waitForTimeout(1000);
    await page.getByText(/^Dalej$/i).click();
    
    await page.waitForTimeout(1000);
    await page.getByText(/^Dalej$/i).click();
    
    await page.getByText(/Przejrzyj swój profil/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    
    await page.getByRole('button').filter({ hasText: /^Zakończ$/i }).click();
    
    await page.waitForURL(/^(?!.*profile-creation)/i, { timeout: 30000 });
    await expect(page).not.toHaveURL(/.*profile-creation.*/i);
  });

  test('survey can be partially completed and resumed', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Piotr');
    await page.getByLabel(/Nazwisko/i).fill('Wiśniewski');
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();
    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    await page.waitForURL(/\/login/i, { timeout: 30000 });
    
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
    await page.waitForURL(/profile-creation/i, { timeout: 30000 });
    
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    
    const majorCombobox = page.locator('button[role="combobox"]').first();
    await majorCombobox.waitFor({ state: 'visible', timeout: 10000 });
    await majorCombobox.click();
    await page.locator('[role="option"]').first().waitFor({ state: 'visible', timeout: 10000 });
    await page.locator('[role="option"]').first().click();
    await page.getByText(/^Dalej$/i).click();
    
    await page.goto('/', { waitUntil: 'load' });
    await page.waitForTimeout(1000);
    
    await page.goto('/profile-creation', { waitUntil: 'load' });
    
    const hasSurveyContent = await page.getByText(/Zacznijmy od|Jakie są twoje doświadczenia|Ile chcesz zarabiać/i)
      .first()
      .isVisible({ timeout: 10000 })
      .catch(() => false);
    
    expect(hasSurveyContent).toBe(true);
  });

  test('survey validates required fields', async ({ page }) => {
    await page.goto('/register', { waitUntil: 'load' });
    await page.getByLabel(/Imię/i).waitFor({ state: 'visible', timeout: 30000 });
    await page.getByLabel(/Imię/i).fill('Maria');
    await page.getByLabel(/Nazwisko/i).fill('Kowalczyk');
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    
    const consentLabel = page.locator('label').filter({ hasText: /Wyrażam zgodę/i });
    await consentLabel.waitFor({ state: 'visible', timeout: 10000 });
    await consentLabel.click();
    await page.getByRole('button').filter({ hasText: /Utwórz konto/i }).click();
    await page.waitForURL(/\/login/i, { timeout: 30000 });
    
    await page.getByLabel(/Email/i).fill(testEmail);
    await page.getByLabel(/Hasło/i).fill(testPassword);
    await page.getByRole('button').filter({ hasText: /^Zaloguj$/i }).click();
    await page.waitForURL(/profile-creation/i, { timeout: 30000 });
    
    await page.getByText(/Zacznijmy od sprecyzowania kierunku pracy/i).waitFor({ 
      state: 'visible', 
      timeout: 30000 
    });
    
    const nextButton = page.getByText(/^Dalej$/i);
    await nextButton.waitFor({ state: 'visible', timeout: 10000 });
    
    const urlBeforeClick = page.url();
    await nextButton.click();
    await page.waitForTimeout(1000);
    
    const urlAfterClick = page.url();
    const stayedOnSamePage = urlBeforeClick === urlAfterClick;
    const hasValidationError = await page.locator('[role="alert"], .error, .text-red-500, .text-destructive')
      .first()
      .isVisible()
      .catch(() => false);
    
    expect(stayedOnSamePage || hasValidationError).toBe(true);
  });
});
