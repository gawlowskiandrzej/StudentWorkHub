"use client";
import { useTranslation } from "react-i18next";
import searchStyles from "../../../styles/SearchView.module.css";
import { Filter } from "./Filters";
import { Search } from "@/types/search/search";


export function SearchBar({localSearch, setLocalSearch}: {localSearch: Search, setLocalSearch: (search: Search) => void}) {
    const {t} = useTranslation("searchbar"); 
    const items = [
        {label: "Category1", value: "Category1"},
        {label: "Category2", value: "Category2"},
        {label: "Category3", value: "Category3"},
    ]

    return (
        <div className={searchStyles["search-section"]}>
            <div className={searchStyles["searchbar"]}>
                <div className={searchStyles["phrase-search"]}>
                    <input className={`${searchStyles["phrase"]} px-3`} value={localSearch?.keyword} onChange={(e) =>
                    setLocalSearch({ ...localSearch, keyword: e.target.value })
                } placeholder={t("searchKeywordPlaceholder")}></input>
                </div>
                <div className={searchStyles["major-study-search"]}>
                    <Filter className="w-full" label={t("searchbarCombobox")} variant="secondary" items={items} onChange={(value: string) =>
                            setLocalSearch({ ...localSearch, category: value })
                        } value={localSearch?.category}/>
                </div>
                <div className={searchStyles["city-search"]}>
                    <input placeholder={t("searchCityPlaceholder")} value={localSearch?.city} onChange={(e) =>
                    setLocalSearch({ ...localSearch, city: e.target.value })
                } className={`${searchStyles["city"]} px-3`}></input>
                </div>
            </div>
        </div>
    );
}