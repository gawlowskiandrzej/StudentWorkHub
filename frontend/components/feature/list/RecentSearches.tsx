import { useSearch } from "@/store/SearchContext";
import {Search} from "../../../types/search/search";
import listStyles from "../../../styles/OfferList.module.css";

const lastSearches: Search[] = [
    { category: "Category1", keyword: "React", city: "Warszawa" },
    { category: "Category2", keyword: "UI", city: "Kraków" },
];

export function RecentSearches({ setSearch }: { setSearch: (search: Search) => void }) {
    return (
        <div className={listStyles["recent-searches-navigation"]}>
            <div className={listStyles["frame-38"]}>
                <img className={listStyles["history"]} src="/icons/history0.svg" />
                <div className={listStyles["recent-searches"]}>Last searches:</div>
            </div>
            {lastSearches.map((item, index) => (
                <div key={index} className={`${listStyles["keyword-search"]} cursor-pointer`} onClick={() => setSearch(item)}>
                    {item.keyword}
                </div>
            ))}
        </div>
    );
}