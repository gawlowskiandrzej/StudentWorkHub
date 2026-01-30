import React from "react";
import { describe, it, expect, vi, beforeEach } from "vitest";
import { renderHook } from "@testing-library/react";
import { RankingProvider, useRanking } from "@/store/RankingContext";
import { useOfferList } from "@/hooks/useOfferList";
import { createMockOfferBatch } from "@tests/utils/mockData";
import { UserProvider } from "@/store/userContext";

vi.mock("@/lib/ranking", () => {
  return {
    getRanking: vi.fn().mockImplementation(() => ({
      setUserPreferences: vi.fn(),
      loadOffersAndScore: vi.fn().mockImplementation((offers) => 
        offers.map((o: any) => ({ offerId: o.id, score: 0.5, confidence: 0.8 }))
      ),
      getOfferScore: vi.fn().mockReturnValue(0.5),
      getWeights: vi.fn().mockReturnValue([1, 0, 0]),
      getConfidence: vi.fn().mockReturnValue(0.8),
      getFeatureWeights: vi.fn().mockReturnValue({ f1: 1, f2: 0, f3: 0 }),
      getFeatureImportance: vi.fn().mockReturnValue({ f1: 0.4, f2: 0.3, f3: 0.3 }),
      getProfileForSync: vi.fn().mockReturnValue({
        vector: [1, 0, 0],
        mean_dist: 0.1,
        order_by_option: "id",
        means_weight_sum: 1,
        means_weight_ssum: 1,
        means_weight_count: 1
      }),
      rankOffers: vi.fn().mockImplementation((offers) => 
        offers.map((o: any) => ({ offerId: o.id, score: 0.5, confidence: 0.8 }))
      ),
    })),
    resetRanking: vi.fn(),
    FEATURE_KEYS: ["f1", "f2", "f3"],
    FEATURE_COUNT: 3,
  };
});

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <UserProvider>
    <RankingProvider>{children}</RankingProvider>
  </UserProvider>
);

describe("Ranking - OfferList Integration", () => {
  it("should rank offers using RankingContext and useOfferList customSort", () => {
    const offers = createMockOfferBatch(10);
    
    const { result: rankingRes } = renderHook(() => useRanking(), { wrapper });
    
    const customSort = (items: any[]) => {
      const scored = rankingRes.current.rankOffersList(items);
      return scored
        .map(s => items.find(o => o.id === s.offerId)!)
        .filter(Boolean);
    };

    const { result: listRes } = renderHook(() => 
      useOfferList({}, "Ranking", 0, 10, {}, offers, customSort)
    , { wrapper });

    expect(listRes.current.filteredOffers).toHaveLength(10);
    const firstOffer = listRes.current.filteredOffers[0];
    expect(rankingRes.current.getOfferScore(firstOffer)).toBe(0.5);
  });
});
