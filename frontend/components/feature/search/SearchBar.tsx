import { Filter } from "./Filters";
import { Search } from "@/types/search/search";

export function SearchBar({localSearch, setLocalSearch}: {localSearch: Search, setLocalSearch: (search: Search) => void}) {
    const items = [
        {label: "Category1", value: "Category1"},
        {label: "Category2", value: "Category2"},
        {label: "Category3", value: "Category3"},
    ]

    return (
        <div className="search-section">
            <div className="searchbar">
                <div className="phrase-search">
                    <input className="phrase px-3" value={localSearch?.keyword} onChange={(e) =>
                    setLocalSearch({ ...localSearch, keyword: e.target.value })
                } placeholder="Search, company, keyword ..."></input>
                </div>
                <div className="major-study-search">
                    <Filter className="w-full" label="Major of study" variant="secondary" items={items} onChange={(value: string) =>
                            setLocalSearch({ ...localSearch, category: value })
                        } value={localSearch?.category}/>
                </div>
                <div className="city-search">
                    <input placeholder="City" value={localSearch?.city} onChange={(e) =>
                    setLocalSearch({ ...localSearch, city: e.target.value })
                } className="city px-3"></input>
                </div>
            </div>
        </div>
    );
}