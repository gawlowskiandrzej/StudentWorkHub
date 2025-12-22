import { useSearch } from "@/context/SearchContext";
import {Search} from "../../../types/search/search";

const lastSearches: Search[] = [
    { category: "Category1", keyword: "React", city: "Warszawa" },
    { category: "Category2", keyword: "UI", city: "Kraków" },
];

export function RecentSearches() {
    const {setSearch} = useSearch();
    return (
        <div className="recent-searches-navigation">
            <div className="frame-38">
                <img className="history" src="/icons/history0.svg" />
                <div className="recent-searches">Last searches:</div>
            </div>
            {lastSearches.map((item, index) => (
                <div key={index} className="keyword-search cursor-pointer" onClick={() => setSearch(item)}>
                    {item.keyword}
                </div>
            ))}
        </div>
    );
}