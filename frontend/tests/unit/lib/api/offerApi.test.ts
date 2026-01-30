import { beforeEach, describe, expect, it, vi } from "vitest";
import { OfferApi } from "@/lib/api/controllers/offer";
import { apiClient } from "@/lib/api/apiClient";

vi.mock("@/lib/api/apiClient", () => {
  const post = vi.fn();
  return { apiClient: { post } };
});

type MockPost = ReturnType<typeof vi.fn>;

describe("OfferApi", () => {
  let post: MockPost;

  beforeEach(() => {
    post = (apiClient.post as unknown) as MockPost;
    post.mockReset();
    post.mockResolvedValue({ data: null, error: null });
  });

  it("sends perPage=-1 and cleans empty filters when fetching DB offers", async () => {
    const search = { keyword: "java", localization: "warsaw" } as const;
    const filters = { employmentTypes: [], city: "", tags: undefined } as any;

    await OfferApi.getOffersDatabase(search, filters);

    expect(post).toHaveBeenCalledTimes(1);
    const [path, body] = post.mock.calls[0];
    expect(path).toBe("/offers/offers-database?perPage=-1");
    expect(body).toMatchObject({ keyword: "java", localization: "warsaw" });
    expect(body).not.toHaveProperty("employmentTypes");
    expect(body).not.toHaveProperty("city");
  });

  it("creates scrapper with merged search and filters", async () => {
    const search = { keyword: "python" } as const;
    const filters = { remote: true, salaries: [10000, 20000] } as any;

    await OfferApi.createScrapper(search, filters);

    const [path, body] = post.mock.calls[0];
    expect(path).toBe("/offers/create-scrappers");
    expect(body).toMatchObject({ keyword: "python", remote: true, salaries: [10000, 20000] });
  });

  it("requests scrapped offers with usedAi and addToDatabase flags", async () => {
    const ids = { jobIds: ["a", "b"] };

    await OfferApi.getScrappedOffers(ids);

    const [path, body] = post.mock.calls[0];
    expect(path).toBe("/offers/offers-scrapped?usedAi=true&addToDatabase=true");
    expect(body).toEqual(ids);
  });
});
