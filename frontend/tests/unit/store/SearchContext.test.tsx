import React from "react";
import { describe, expect, it } from "vitest";
import { renderHook, act } from "@testing-library/react";
import { SearchProvider, useSearch } from "@/store/SearchContext";
import { PaginationProvider, usePagination } from "@/store/PaginationContext";

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <PaginationProvider>
    <SearchProvider>{children}</SearchProvider>
  </PaginationProvider>
);

describe("SearchContext", () => {
  it("toggleFilter adds and removes filter values", () => {
    const { result } = renderHook(() => useSearch(), { wrapper });

    act(() => {
      result.current.toggleFilter("languages", "en");
    });

    expect(result.current.filters.languages?.has("en")).toBe(true);

    act(() => {
      result.current.toggleFilter("languages", "en");
    });

    expect(result.current.filters.languages?.has("en")).toBe(false);
  });

  it("clearAll resets search, filters, extraFilters and search keywords", () => {
    const { result } = renderHook(() => useSearch(), { wrapper });

    act(() => {
      result.current.setSearch({ category: "it", keyword: "java", localization: "krk" });
      result.current.setExtraFilter("salaryFrom", "10000");
      result.current.setSearchFilterKeywords("skillName", "ts");
      result.current.toggleFilter("languages", "pl");
    });

    act(() => {
      result.current.clearAll();
    });

    expect(result.current.search).toEqual({ category: "", keyword: "", localization: "" });
    expect(result.current.extraFilters).toEqual({});
    expect(result.current.filters.languages?.size ?? 0).toBe(0);
    expect(result.current.searchFilterKeywords).toEqual({ skillName: "", educationName: "", benefitName: "" });
  });

  it("clearFilters resets filter-related state and offset but keeps search text", () => {
    const { result } = renderHook(() => ({ search: useSearch(), pagination: usePagination() }), { wrapper });

    act(() => {
      result.current.search.setSearch({ category: "it", keyword: "java", localization: "krk" });
      result.current.search.setExtraFilter("salaryTo", "20000");
      result.current.search.toggleFilter("languages", "pl");
      result.current.search.setSearchFilterKeywords("benefitName", "gym");
      result.current.pagination.setOffset(40);
    });

    act(() => {
      result.current.search.clearFilters();
    });

    expect(result.current.search.search.keyword).toBe("java");
    expect(result.current.search.extraFilters).toEqual({});
    expect(result.current.search.filters.languages?.size ?? 0).toBe(0);
    expect(result.current.search.searchFilterKeywords).toEqual({ skillName: "", educationName: "", benefitName: "" });
    expect(result.current.pagination.offset).toBe(0);
  });

  it("addRecentSearch keeps unique entries and caps at 5", () => {
    const { result } = renderHook(() => useSearch(), { wrapper });

    const make = (n: number) => ({ keyword: `k${n}`, category: `c${n}`, localization: "" });

    act(() => {
      result.current.addRecentSearch(make(1));
      result.current.addRecentSearch(make(2));
      result.current.addRecentSearch(make(3));
      result.current.addRecentSearch(make(4));
      result.current.addRecentSearch(make(5));
      result.current.addRecentSearch(make(6));
      result.current.addRecentSearch(make(3));
    });

    expect(result.current.recentSearches).toHaveLength(5);
    expect(result.current.recentSearches[0].keyword).toBe("k3");
    const uniq = new Set(result.current.recentSearches.map(s => s.keyword));
    expect(uniq.size).toBe(result.current.recentSearches.length);
  });

  it("setSorting updates sort key and resets offset", () => {
    const { result } = renderHook(() => ({ search: useSearch(), pagination: usePagination() }), { wrapper });

    act(() => {
      result.current.pagination.setOffset(50);
    });

    act(() => {
      result.current.search.setSorting("sort", "Salarydesc");
    });

    expect(result.current.search.sorts.sort).toBe("Salarydesc");
    expect(result.current.pagination.offset).toBe(0);
  });
});
