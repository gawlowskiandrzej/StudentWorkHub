import React from "react";
import { describe, it, expect, vi, beforeEach, Mock } from "vitest";
import { renderHook, act, waitFor } from "@testing-library/react";
import { SearchProvider, useSearch } from "@/store/SearchContext";
import { PaginationProvider, usePagination } from "@/store/PaginationContext";
import { OffersProvider, useOffers } from "@/store/OffersContext";
import { OfferApi } from "@/lib/api/controllers/offer";
import { createMockOfferBatch } from "@tests/utils/mockData";

vi.mock("@/lib/api/controllers/offer", () => ({
  OfferApi: {
    getOffersDatabase: vi.fn(),
  },
}));

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <PaginationProvider>
    <SearchProvider>
      <OffersProvider>{children}</OffersProvider>
    </SearchProvider>
  </PaginationProvider>
);

describe("Search-Offers-Pagination Integration", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("should reset pagination and fetch new offers when search parameters change", async () => {
    (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
      data: {
        pagination: { items: createMockOfferBatch(5), totalItems: 5 },
        dynamicFilter: {},
      },
    });

    const { result } = renderHook(() => ({
      search: useSearch(),
      pagination: usePagination(),
      offers: useOffers(),
    }), { wrapper });

    act(() => {
      result.current.pagination.setOffset(20);
    });
    expect(result.current.pagination.offset).toBe(20);

    await act(async () => {
      result.current.search.setSearch({ category: "it", keyword: "react", localization: "warsaw" });
      result.current.search.toggleFilter("languages", "en");
      result.current.search.setHasSearched(true);
    });
    
    act(() => {
        result.current.search.clearFilters();
    });

    expect(result.current.pagination.offset).toBe(0);
    
    await act(async () => {
        await result.current.offers.fetchOffers();
    });

    expect(OfferApi.getOffersDatabase).toHaveBeenCalledWith(
        expect.objectContaining({ keyword: "react" }),
        expect.anything()
    );
  });

  it("should apply ranking sorting when Ranking sort is selected", async () => {
    const { result } = renderHook(() => ({
        search: useSearch(),
        offers: useOffers(),
    }), { wrapper });

    act(() => {
        result.current.search.setSorting("sort", "Ranking");
    });

    expect(result.current.search.sorts.sort).toBe("Ranking");
  });
});
