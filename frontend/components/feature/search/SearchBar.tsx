import { useState } from "react";
import { Filter } from "./Filters";
import { useSearch } from "@/context/SearchContext";

export function SearchBar() {
    const {search, setSearch} = useSearch();
    const items = [
        {label: "Category1", value: "Category1"},
        {label: "Category2", value: "Category2"},
        {label: "Category3", value: "Category3"},
    ]

    return (
        <div className="search-section">
            <div className="searchbar">
                <div className="phrase-search">
                    <input className="phrase px-3" value={search.keyword} onChange={(e) =>
                    setSearch({ ...search, keyword: e.target.value })
                } placeholder="Search, company, keyword ..."></input>
                </div>
                <div className="major-study-search">
                    <Filter className="w-full" label="Major of study" variant="secondary" items={items} onChange={(value: string) =>
                            setSearch({ ...search, category: value })
                        } value={search.category}/>
                </div>
                <div className="city-search">
                    <input placeholder="City" value={search.city} onChange={(e) =>
                    setSearch({ ...search, city: e.target.value })
                } className="city px-3"></input>
                </div>
            </div>
        </div>
    );
}