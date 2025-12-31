"use client";
import { createContext, useContext, useState, ReactNode } from "react";
import { search } from "@/types/search/search";
import { FiltersState } from "@/app/(public)/list/page";
import { FilterKey, FilterValue } from "@/types/details/dynamicFilter";
import { sortType } from "@/types/list/sort";
import { usePagination } from "./PaginationContext";

export type ExtraFiltersState = {
    workType?: string;
    workTime?: string;
    employmentType?: string;
    salary?: string;
};
type SearchContextType = {
    search: search;
    setSearch: (search: search) => void;

    filters: FiltersState;
    toggleFilter: (key: FilterKey, value: FilterValue) => void;

    recentSearches: search[];
    addRecentSearch: (search: search) => void;

    extraFilters: ExtraFiltersState;
    setExtraFilter: (key: keyof ExtraFiltersState, value?: string) => void;

    sorts: sortType;
    setSorting: (key: "sort", value: string) => void;

    clearFilters: () => void;
};
const MAX_RECENT = 5;
const SearchContext = createContext<SearchContextType | undefined>(undefined);

export function SearchProvider({ children }: { children: ReactNode }) {
    const [search, setSearch] = useState<search>({
        category: "",
        keyword: "",
        city: ""
    });

    const [filters, setFilters] = useState<FiltersState>({});
    const [extraFilters, setExtraFilters] = useState<ExtraFiltersState>({});
    const [recentSearches, setRecentSearches] = useState<search[]>([]);
    const { setOffset } = usePagination();
    const [sorts, setSort] = useState<sortType>({ sort: "IdDesc" });

    const clearFilters = () => { setFilters({}); setExtraFilters({}); setSearch({ category: "", city: "", keyword: "" }); setOffset(0); }

    const toggleFilter = (key: FilterKey, value: FilterValue) => {
        setOffset(0);
        setFilters(prev => {
            const set = new Set(prev[key] ?? []);
            set.has(value) ? set.delete(value) : set.add(value);

            return { ...prev, [key]: set };
        });

    };

    const setSorting = (key: "sort", value: string) => {
        setOffset(0);
        setSort(prev => ({ ...prev, [key]: value }));

    };

    const addRecentSearch = (next: search) => {
        setRecentSearches(prev => {
            const filtered = prev.filter(
                s =>
                    s.keyword !== next.keyword ||
                    s.city !== next.city ||
                    s.category !== next.category
            );

            return [next, ...filtered].slice(0, MAX_RECENT);
        });
    };

    const setExtraFilter = (
        key: keyof ExtraFiltersState,
        value?: string
    ) => {
        setExtraFilters(prev => ({
            ...prev,
            [key]: value,
        }));
    };

    return (
        <SearchContext.Provider value={{
            search,
            setSearch,
            filters,
            extraFilters,
            recentSearches,
            sorts,
            setSorting,
            addRecentSearch,
            setExtraFilter,
            toggleFilter,
            clearFilters,
        }}>
            {children}
        </SearchContext.Provider>
    );
}

export function useSearch() {
    const context = useContext(SearchContext);
    if (!context) {
        throw new Error("useSearch must be used within SearchProvider");
    }
    return context;
}
