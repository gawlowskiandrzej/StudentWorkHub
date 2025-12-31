"use client";
import { createContext, useContext, useState, ReactNode } from "react";
import { Search } from "@/types/search/search";
import { FiltersState } from "@/app/(public)/list/page";
import { FilterKey, FilterValue } from "@/types/details/dynamicFilter";
import { salary } from "@/types/list/Offer/salary";

export type ExtraFiltersState = {
    workType?: string;
    workTime?: string;
    employmentType?: string;
    salary?: salary;
};
type SearchContextType = {
    search: Search;
    setSearch: (search: Search) => void;

    filters: FiltersState;
    toggleFilter: (key: FilterKey, value: FilterValue) => void;

    extraFilters: ExtraFiltersState;
    setExtraFilter: (key: keyof ExtraFiltersState, value?: string) => void;


    clearFilters: () => void;
};

const SearchContext = createContext<SearchContextType | undefined>(undefined);

export function SearchProvider({ children }: { children: ReactNode }) {
    const [search, setSearch] = useState<Search>({
        category: "",
        keyword: "",
        city: ""
    });

    const [filters, setFilters] = useState<FiltersState>({});
    const [extraFilters, setExtraFilters] = useState<ExtraFiltersState>({});
    const clearFilters = () => {setFilters({}); setExtraFilters({})}

    const toggleFilter = (key: FilterKey, value: FilterValue) => {
        setFilters(prev => {
            const set = new Set(prev[key] ?? []);
            set.has(value) ? set.delete(value) : set.add(value);

            return { ...prev, [key]: set };
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
