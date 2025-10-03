import { test, expect } from '@playwright/test';

// Use the same URL the project's Playwright webServer uses
const base = 'http://localhost:5033';

test.describe('RedHorn site basic flows', () => {
  test('Home page shows hero and links', async ({ page }) => {
    await page.goto(`${base}/`);

    await expect(page.getByRole('heading', { name: 'Welcome to RedHorn' })).toBeVisible();
    await expect(page.getByText('A modern .NET 8 MVC application built with best practices')).toBeVisible();

    // Learn More navigates to Privacy
    await page.getByRole('link', { name: 'Learn More' }).click();
    await expect(page).toHaveURL(/\/(Home\/Privacy|Privacy)$/);
    await expect(page.getByRole('heading', { name: 'Privacy Policy' })).toBeVisible();
  });

  test('Privacy page displays policy text', async ({ page }) => {
    await page.goto(`${base}/Home/Privacy`);

    await expect(page.getByRole('heading', { name: 'Privacy Policy' })).toBeVisible();
    await expect(page.getByText("Use this page to detail your site's privacy policy.")).toBeVisible();
  });

  test('Ask form submits and shows success', async ({ page }) => {
    await page.goto(`${base}/Questions/Ask`);

    await expect(page.getByRole('heading', { name: 'Ask a Question' })).toBeVisible();

    // Fill the form using display names from QuestionViewModel
    await page.getByLabel('Your Name').fill('Playwright Tester');
    await page.getByLabel('Email Address').fill('playwright@test.example');
    await page.getByLabel('Question Category').selectOption('Technical');

    // Choose High priority
    await page.getByLabel('High').check();

    await page.getByLabel('Your Question').fill('This is an automated test question with enough length to pass validation.');

    // Submit and wait for navigation to Success
    await Promise.all([
      page.waitForNavigation(),
      page.getByRole('button', { name: 'Submit Question' }).click(),
    ]);

    await expect(page.getByRole('heading', { name: 'Question Submitted Successfully!' })).toBeVisible();
    await expect(page.getByText('Thank you! Your question has been submitted successfully.')).toBeVisible();
  });
});
