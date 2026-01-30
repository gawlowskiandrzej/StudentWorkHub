import React, { useEffect } from "react";
import { describe, it, expect, vi, beforeEach, afterEach, Mock } from "vitest";
import { renderHook, act, waitFor } from "@testing-library/react";
import { OffersProvider, useOffers } from "@/store/OffersContext";
import { SearchProvider, useSearch } from "@/store/SearchContext";
import { PaginationProvider } from "@/store/PaginationContext";
import { OfferApi } from "@/lib/api/controllers/offer";
import { createMockOffer } from "@tests/utils/mockData";

vi.mock("@/lib/api/controllers/offer", () => ({
  OfferApi: {
    getOffersDatabase: vi.fn(),
    createScrapper: vi.fn(),
    getScrappedOffers: vi.fn(),
  },
}));

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <PaginationProvider>
    <SearchProvider>
      <OffersProvider>{children}</OffersProvider>
    </SearchProvider>
  </PaginationProvider>
);

describe("OffersContext Integration", () => {
  beforeEach(() => {
    vi.clearAllMocks();
    vi.useFakeTimers();
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  describe("fetchOffers", () => {
    it("should fetch offers and update state", async () => {
      const mockOffers = [createMockOffer({ id: 1 }), createMockOffer({ id: 2 })];
      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: { items: mockOffers, totalItems: 2 },
          dynamicFilter: { languages: [] },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.fetchOffers();
      });

      expect(result.current.offersResponse?.pagination.items).toHaveLength(2);
      expect(result.current.loading).toBe(false);
      expect(result.current.error).toBeNull();
    });

    it("should handle error during fetch", async () => {
      (OfferApi.getOffersDatabase as Mock).mockRejectedValue(new Error("Network error"));

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.fetchOffers();
      });

      expect(result.current.error).toBe("Nie udało się pobrać ofert");
      expect(result.current.loading).toBe(false);
    });
  });

  describe("scrapping workflow", () => {
    it("should start scrapping and poll for results", async () => {
      (OfferApi.createScrapper as Mock).mockResolvedValue({
        data: { jobIds: ["job-1"] },
      });

      (OfferApi.getScrappedOffers as Mock).mockResolvedValueOnce({
        data: {
          scrappingStatus: {
            jobInfos: [{ jobId: "job-1", status: "processing" }],
            scrappingDone: false,
          },
        },
      });

      const newOffer = createMockOffer({ id: 100 });
      (OfferApi.getScrappedOffers as Mock).mockResolvedValueOnce({
        data: {
          scrappingStatus: {
            jobInfos: [{ jobId: "job-1", status: "finished" }],
            scrappingDone: true,
          },
          databaseOffersResponse: {
            pagination: { items: [newOffer], totalItems: 1 },
            dynamicFilter: {},
          },
        },
      });

      // Use real timers but mock the interval to fire immediately for testing
      // Or just wait. Since it's 3 seconds, real timers might be okay for a test if we reduce the interval.
      // But we can't easily change the interval in the source code from the test.
      
      vi.useFakeTimers();

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.startScrapping();
      });

      expect(result.current.scrapping).toBe(true);

      // Trigger the interval
      await act(async () => {
        vi.advanceTimersByTime(3100);
      });

      // Additional wait for any async microtasks
      await act(async () => {
        await Promise.resolve();
      });

      await act(async () => {
        vi.advanceTimersByTime(3100);
      });

      await act(async () => {
        await Promise.resolve();
      });

      expect(result.current.offersResponse?.pagination.items).toHaveLength(1);
      expect(result.current.scrapping).toBe(false);

      vi.useRealTimers();
    });
  });
});
