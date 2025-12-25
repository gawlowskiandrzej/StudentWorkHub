"use client";
import { createContext, useContext, useState, ReactNode } from "react";
import { Search } from "@/types/search/search"; 

type SearchContextType = {
    search: Search;
    setSearch: (search: Search) => void;
};

const SearchContext = createContext<SearchContextType | undefined>(undefined);

export function SearchProvider({ children }: { children: ReactNode }) {
    const [search, setSearch] = useState<Search>({
        category: "",
        keyword: "",
        city: "",
    });

    return (
        <SearchContext.Provider value={{ search, setSearch }}>
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
