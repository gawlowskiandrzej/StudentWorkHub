"use client";
import { useTranslation } from "react-i18next";
import searchStyles from "../../../styles/SearchView.module.css";
import { Filter } from "./Filters";
import {leading_categories} from '@/store/data/Dictionaries.json';
import { toLabelValueFormat } from "@/utils/others/toLabelValueFormat";
import { useSearch } from "@/store/SearchContext";


export function SearchBar() {
    const {search, setSearch} = useSearch();
    const {t} = useTranslation("searchbar"); 
    const items = toLabelValueFormat(leading_categories);

    return (
        <div className={searchStyles["search-section"]}>
            <div className={searchStyles["searchbar"]}>
                <div className={searchStyles["phrase-search"]}>
                    <input className={`${searchStyles["phrase"]} px-3`} value={search?.keyword} onChange={(e) =>
                    setSearch({ ...search, keyword: e.target.value })
                } placeholder={t("searchKeywordPlaceholder")}></input>
                </div>
                <div className={searchStyles["major-study-search"]}>
                    <Filter clearable={true} className="w-full" label={t("searchbarCombobox")} variant="secondary" items={items} onChange={(value: string) =>
                            setSearch({ ...search, category: value })
                        } value={search?.category}/>
                </div>
                <div className={searchStyles["city-search"]}>
                    <input placeholder={t("searchCityPlaceholder")} value={search?.city} onChange={(e) =>
                    setSearch({ ...search, city: e.target.value })
                } className={`${searchStyles["city"]} px-3`}></input>
                </div>
            </div>
        </div>
    );
}