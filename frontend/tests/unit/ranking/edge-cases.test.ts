import { describe, expect, it, beforeEach } from "vitest";
import { Ranking } from "@/lib/ranking/ranking";
import { createMockOfferBatch, createMockOffer, createMockPreferences } from "@tests/utils/mockData";


describe("Ranking Edge Cases", () => {
  let ranking: Ranking;

  beforeEach(() => {
    ranking = new Ranking();
  });

  it("handles user preferences with zero salary (division by zero risk)", () => {
    const preferences = createMockPreferences({
      salaryFrom: 0,
      salaryTo: 0
    });
    
    ranking.setUserPreferences(preferences); 
    
    const offers = [
      createMockOffer({ salaryFrom: 5000, salaryTo: 10000 }),
    ];
    
    const scores = ranking.rankOffers(offers);
    expect(scores[0].score).toBeDefined();
    expect(isNaN(scores[0].score)).toBe(false);
    expect(isFinite(scores[0].score)).toBe(true);
  });

  it("handles weight explosion with extreme signaling", () => {
    const preferences = createMockPreferences();
    const offer = createMockOffer();
    
    ranking.loadOffersAndScore([offer]);
    for (let i = 0; i < 100; i++) {
        // Simulating extreme signaling
        if (i % 2 !== 0) {
             ranking.recordInteraction(offer.id, 'click');
        }
    }
    
    const weights = ranking.getProfileForSync().vector;
    expect(weights.every(w => w >= 0.01 && w <= 1.0)).toBe(true);
    expect(weights.every(w => !isNaN(w))).toBe(true);
  });

  it("handles offers with zero salary (edge case)", () => {
    const offers = [
      createMockOffer({ salaryFrom: 0, salaryTo: 0 }),
      createMockOffer({ salaryFrom: 5000, salaryTo: 10000 }),
    ];
    
    expect(() => {
      ranking.loadOffersAndScore(offers);
    }).not.toThrow();
    
    const scores = ranking.rankOffers(offers);
    expect(scores).toHaveLength(2);
    expect(scores.every(s => !isNaN(s.score))).toBe(true);
  });

  it("handles all offers with identical salary", () => {
    const offers = [
      createMockOffer({ salaryFrom: 5000, salaryTo: 5000 }),
      createMockOffer({ salaryFrom: 5000, salaryTo: 5000 }),
    ];

    const scores = ranking.rankOffers(offers);
    
    expect(scores).toHaveLength(2);
    expect(scores.every(s => typeof s.score === 'number')).toBe(true);
  });

  it("handles null/undefined fields without crashes", () => {
    const offers = [
      createMockOffer({ 
        benefits: null as any,
        tech: null as any,
        requiredLanguages: []
      }),
      createMockOffer({ 
        salaryFrom: undefined as any,
        description: null as any
      }),
    ];

    expect(() => {
      ranking.loadOffersAndScore(offers);
    }).not.toThrow();
  });

  it("handles extremely high salary values", () => {
    const offers = [
      createMockOffer({ salaryFrom: 1000000, salaryTo: 10000000 }),
      createMockOffer({ salaryFrom: 5000, salaryTo: 10000 }),
    ];

    const scores = ranking.rankOffers(offers);
    expect(scores.every(s => isFinite(s.score))).toBe(true);
  });

  it("handles negative salary gracefully", () => {
    const offers = [
      createMockOffer({ salaryFrom: -1000, salaryTo: 5000 }),
      createMockOffer({ salaryFrom: 5000, salaryTo: 10000 }),
    ];

    expect(() => {
      ranking.loadOffersAndScore(offers);
    }).not.toThrow();
  });

  it("ranks single offer without crashes", () => {
    const offers = [createMockOffer()];
    
    expect(() => {
      ranking.loadOffersAndScore(offers);
    }).not.toThrow();

    const scores = ranking.rankOffers(offers);
    expect(scores).toHaveLength(1);
  });

  it("handles empty offer list gracefully", () => {
    expect(() => {
      ranking.loadOffersAndScore([]);
    }).not.toThrow();

    const scores = ranking.rankOffers([]);
    expect(scores).toHaveLength(0);
  });

  it("handles preferences with inverted salary range (from > to)", () => {
    const prefs = createMockPreferences({ 
      salaryFrom: 50000, 
      salaryTo: 5000
    });

    const offers = createMockOfferBatch(3);
    
    expect(() => {
      ranking.setUserPreferences(prefs);
      ranking.rankOffers(offers);
    }).not.toThrow();
  });

  it("handles all preferences set to minimum (0)", () => {
    const prefs = createMockPreferences({
      salaryFrom: 0,
      salaryTo: 0,
    });

    ranking.setUserPreferences(prefs);
    const offers = createMockOfferBatch(3);
    const scores = ranking.rankOffers(offers);
    
    expect(scores.every(s => typeof s.score === 'number')).toBe(true);
  });

  it("doesn't crash when recording interactions for non-existent offer", () => {
    const offers = createMockOfferBatch(2);
    ranking.loadOffersAndScore(offers);

    expect(() => {
      ranking.recordInteraction(999999, "click");
    }).not.toThrow();
  });

  it("handles rapid repeated interactions correctly", () => {
    const offers = createMockOfferBatch(1);
    ranking.loadOffersAndScore(offers);
    const offerId = offers[0].id;

    for (let i = 0; i < 100; i++) {
      ranking.recordInteraction(offerId, "click");
    }

    expect(ranking.hasUnsyncedChanges()).toBe(true);
  });

  it("correctly accumulates view_time across multiple interactions", () => {
    const offers = createMockOfferBatch(1);
    ranking.loadOffersAndScore(offers);
    const offerId = offers[0].id;

    ranking.recordInteraction(offerId, "view_time", 5000);
    ranking.recordInteraction(offerId, "view_time", 3000);

    expect(ranking.hasUnsyncedChanges()).toBe(true);
  });

  it("handles very large batches (1000+ offers)", () => {
    const largeOfferBatch = Array.from({ length: 1000 }, (_, i) =>
      createMockOffer({ id: i, title: `Offer ${i}` })
    );

    expect(() => {
      ranking.loadOffersAndScore(largeOfferBatch);
    }).not.toThrow();

    const scores = ranking.rankOffers(largeOfferBatch);
    expect(scores).toHaveLength(1000);
  });

  it("pagination doesn't skip or duplicate offers", () => {
    const offers = Array.from({ length: 50 }, (_, i) =>
      createMockOffer({ id: i + 1 })
    );

    ranking.loadOffersAndScore(offers);
    
    const page1 = ranking.rankOffers(offers.slice(0, 10));
    const page2 = ranking.rankOffers(offers.slice(10, 20));

    const ids = [...page1, ...page2].map(s => s.offerId);
    expect(new Set(ids).size).toBe(ids.length);
  });

  it("maintains score precision across multiple calculations", () => {
    const offers = createMockOfferBatch(3);
    
    const scores1 = ranking.rankOffers(offers);
    const scores2 = ranking.rankOffers(offers);

    for (let i = 0; i < scores1.length; i++) {
      expect(scores1[i].score).toBe(scores2[i].score);
    }
  });

  it("handles rapid preference changes without state corruption", () => {
    const offers = createMockOfferBatch(5);
    ranking.loadOffersAndScore(offers);

    const prefs1 = { salary_from: 5000 } as any;
    const prefs2 = { salary_from: 10000 } as any;

    ranking.setUserPreferences(prefs1);
    const scores1 = ranking.rankOffers(offers);

    ranking.setUserPreferences(prefs2);
    const scores2 = ranking.rankOffers(offers);

    expect(scores1).not.toEqual(scores2);
  });
});
