import listStyles from "../../../styles/OfferList.module.css";
import { useTranslation } from "react-i18next";
import { useSearch } from "@/store/SearchContext";


export function LastSearches() {
    const { recentSearches, setSearch } = useSearch();
    const {t} = useTranslation("list");
    return (
        <div className={listStyles["recent-searches-navigation"]}>
            <div className={listStyles["frame-38"]}>
                <img className={listStyles["history"]} src="/icons/history0.svg" />
                <div className={listStyles["recent-searches"]}>{t("lastSearches")}</div>
            </div>
            {recentSearches.map((item, index) => (
                <div key={index} className={`${listStyles["keyword-search"]} cursor-pointer`} onClick={() => setSearch(item)}>
                    {item.keyword}
                </div>
            ))}
        </div>
    );
}