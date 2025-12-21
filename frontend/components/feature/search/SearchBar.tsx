import { useState } from "react";
import { Filter } from "./Filters";

export function SearchBar() {
    const [category, setCategory] = useState<string | undefined>(undefined);
    const items = [
        {label: "Category1", value: "Category1"},
        {label: "Category2", value: "Category2"},
        {label: "Category3", value: "Category3"},
    ]

    return (
        <div className="search-section">
            <div className="searchbar">
                <div className="phrase-search">
                    <input className="phrase px-3" placeholder="Search, company, keyword ..."></input>
                </div>
                <div className="major-study-search">
                    <Filter className="w-100" label="Major of study" variant="secondary" items={items} onChange={setCategory} value={category}>
                        
                    </Filter>
                </div>
                <div className="city-search">
                    <input placeholder="City" className="city px-3"></input>
                </div>
            </div>
        </div>
    );
}