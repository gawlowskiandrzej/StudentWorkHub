import { test, expect } from "@playwright/test";

test.describe.skip("performance", () => {
  test("offers search responds within 2s", async ({ request }) => {
    const start = Date.now();
    const response = await request.post("/api/offers/offers-database?perPage=-1", {
        data: {
            keyword: "test"
        }
    });
    const duration = Date.now() - start;

    expect(response.ok()).toBeTruthy();
    expect(duration).toBeLessThan(2000);
  });
});
