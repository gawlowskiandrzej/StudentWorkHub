import { describe, expect, it, beforeEach } from "vitest";
import { Ranking } from "@/lib/ranking/ranking";
import { createMockOfferBatch } from "@tests/utils/mockData";

describe("Ranking dirty state", () => {
  let ranking: Ranking;

  beforeEach(() => {
    ranking = new Ranking();
  });

  it("toggles dirty flag after meaningful interaction and clears on sync", () => {
    const offers = createMockOfferBatch(2);
    ranking.loadOffersAndScore(offers);

    expect(ranking.hasUnsyncedChanges()).toBe(false);

    ranking.recordInteraction(offers[0].id, "click");
    expect(ranking.hasUnsyncedChanges()).toBe(true);

    ranking.getProfileForSync();
    expect(ranking.hasUnsyncedChanges()).toBe(false);
  });

  it("ignores sub-threshold view times and keeps state clean", () => {
    const offers = createMockOfferBatch(1);
    ranking.loadOffersAndScore(offers);

    ranking.recordInteraction(offers[0].id, "view_time", 100);
    expect(ranking.hasUnsyncedChanges()).toBe(false);
  });
});
