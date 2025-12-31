"use client";
import searchStyle from "../../../styles/SearchView.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import footerStyle from '../../../styles/Footer.module.css'
import { Filter } from "@/components/feature/search/Filters";
import { useEffect, useState } from "react";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { FilterWithDialog } from "@/components/feature/search/FilterWithDialog";
import { useRouter } from 'next/navigation';
import { useSearch } from "@/store/SearchContext";
import { useTranslation } from "react-i18next";

const Search = () => {
    const [localSearch, setLocalSearch] = useState({
        keyword: "",
        category: "",
        city: ""
    });
    const {t} = useTranslation(["searchView", "searchbar", "common"])
    const { setSearch, extraFilters, setExtraFilter, clearFilters} = useSearch();
    const router = useRouter();
    const items = [
        { label: "Worktype1", value: "Worktype1" },
        { label: "Worktype2", value: "Worktype2" },
        { label: "Worktype3", value: "Worktype3" },
    ]; 
    useEffect(() => {
        clearFilters();
    }, [])
    const gotoListPage = () => {
    setSearch(localSearch);
    router.push('/list');
    };
    return (
        <div className={searchStyle["search-view"]}>
            <div className={searchStyle["search-view-content"]}>
                <div className={searchStyle["search-view-header"]}>
                    <span>
                        <span className={searchStyle["search-view-header-span"]}>
                            {t("searchView:searchViewTitlePart1")}
                        </span>
                        <span className={searchStyle["search-view-header-span2"]}> {t("searchView:searchViewTitlePart2")}</span>
                    </span>
                </div>
                    <SearchBar localSearch={localSearch} setLocalSearch={setLocalSearch}>
                    </SearchBar>
                    <div className={searchStyle["search-sub-section"]}>
                        <div className={searchStyle["sub-filters"]}>
                            <div className={searchStyle["basic-filter-item"]}>
                                <Filter
                                    label={t("searchView:workTypeFilterTitle")}
                                    items={items}
                                    onChange={(v) => setExtraFilter("workType", v)}
                                    value={extraFilters.workType}>
                                </Filter>
                            </div>
                            <div className={searchStyle["basic-filter-item"]}>
                                <Filter
                                    label={t("searchView:workTime")}
                                    items={items}
                                    onChange={(v) => setExtraFilter("workTime", v)}
                                    value={extraFilters.workTime}>
                                </Filter>
                            </div>
                            <div className={searchStyle["basic-filter-item"]}>
                                <Filter
                                    label={t("searchView:employmentType")}
                                    items={items}
                                    onChange={(v) => setExtraFilter("employmentType", v)}
                                    value={extraFilters.employmentType}>
                                </Filter>
                            </div>
                            <div className={`${searchStyle["basic-filter-item"]}`}>
                                <FilterWithDialog
                                    label={t("searchView:salary")}
                                    items={items}
                                    value={extraFilters.salary}
                                    onChange={(v) => setExtraFilter("salary", v)}>
                                </FilterWithDialog>
                            </div>
                        </div>
                        <div onClick={gotoListPage} className={buttonStyles["main-button"]}>
                            <img className={`${footerStyle["search"]} mt-0.5`} src="/icons/search0.svg" />
                            <div className={buttonStyles["find-matching-job"]}>{t("common:findMatchingJob")}</div>
                        </div>
                </div>
            </div>
        </div>
    );
}

export default Search;