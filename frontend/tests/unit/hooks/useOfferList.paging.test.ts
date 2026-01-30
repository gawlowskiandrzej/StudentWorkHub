import { describe, expect, it } from "vitest";
import { renderHook } from "@testing-library/react";
import { useOfferList } from "@/hooks/useOfferList";
import { FiltersState } from "@/app/(public)/list/page";
import { searchFilterKeywords } from "@/types/list/searchFilterKeywords";

const makeOffers = (n: number) =>
  Array.from({ length: n }).map((_, i) => ({ id: i + 1, title: `Offer ${i + 1}`, creationDate: i, salaryFrom: i * 1000 }));

describe("useOfferList pagination and sorting", () => {
  it("paginates according to offset and limit after filtering", () => {
    const offers = makeOffers(30);
    const filters: FiltersState = {};
    const keywords: searchFilterKeywords = { skillName: "", educationName: "", benefitName: "" };

    const { result } = renderHook(() =>
      useOfferList(filters, "IdDesc", 10, 5, keywords, offers as any)
    );

    expect(result.current.filteredOffers).toHaveLength(5);
    expect(result.current.filteredOffers[0].id).toBe(20);
  });

  it("applies custom sorter before pagination when provided", () => {
    const offers = makeOffers(10);
    const filters: FiltersState = {};
    const keywords: searchFilterKeywords = { skillName: "", educationName: "", benefitName: "" };

    const { result } = renderHook(() =>
      useOfferList(filters, "Ranking", 0, 3, keywords, offers as any, items => [...items].reverse())
    );

    expect(result.current.filteredOffers.map(o => o.id)).toEqual([10, 9, 8]);
  });
});
