"use client";
import searchStyle from "../../../styles/SearchView.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import footerStyle from '../../../styles/Footer.module.css'
import { Filter } from "@/components/feature/search/Filters";
import { useState } from "react";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { FilterWithDialog } from "@/components/feature/search/FilterWithDialog";
import { useRouter } from 'next/navigation';
import { ExtraFiltersState, useSearch } from "@/store/SearchContext";
import { useTranslation } from "react-i18next";
import { toLabelValueFormat } from '../../../utils/others/toLabelValueFormat';
import { useSimpleDictionaries } from "@/hooks/useDictionaries";
import { staticFilterItem } from "@/types/search/staticFilterItem";
import { Skeleton } from "@/components/ui/skeleton";
import { search } from "@/types/search/search";

const Search = () => {
    const {setExtraFilters ,addRecentSearch, setSearch, setHasSearched } = useSearch();
    const [localSearch, setLocalSearch] = useState<search>({});
    const [localExtraFilters, setLocalExtraFilters] = useState<ExtraFiltersState>({});
    const { t } = useTranslation(["searchView", "searchbar", "common"])
    const router = useRouter();
    const { dictionaries, loading, error } = useSimpleDictionaries();
    const setLocalExtraFilter = <K extends keyof ExtraFiltersState>(
    key: K,
    value: ExtraFiltersState[K]
    ) => {
        setLocalExtraFilters(prev => ({
            ...prev,
            [key]: value,
        }));
    };
    const items: staticFilterItem[][] = [
        toLabelValueFormat(dictionaries?.employmentType || []),
        toLabelValueFormat(dictionaries?.employmentSchedules || []),
        toLabelValueFormat(dictionaries?.salaryPeriods || [])
    ]
    const filterConfigs: { key: keyof ExtraFiltersState; label: string; items: staticFilterItem[] }[] = [
        { key: "employmentType", label: t("searchView:workTypeFilterTitle"), items: items[0] },
        { key: "employmentSchedules", label: t("searchView:workTime"), items: items[1] },
        { key: "salaryPeriods", label: t("searchView:employmentType"), items: items[2] },
    ];
    const gotoListPage = () => {
        addRecentSearch(localSearch);
        setSearch(localSearch);
        setExtraFilters(localExtraFilters);
        setHasSearched(true);
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
                <SearchBar value={localSearch} onChange={setLocalSearch} />
                <div className={searchStyle["search-sub-section"]}>
                    <div className={searchStyle["sub-filters"]}>
                        <div className={searchStyle["sub-filters"]}>
                            {filterConfigs.map(({ key, label, items }) => (
                                <div key={key} className={searchStyle["basic-filter-item"]}>
                                    <Filter
                                        clearable
                                        label={label}
                                        items={items}
                                        loading={loading}
                                        value={localExtraFilters[key]}
                                        onChange={(v) => setLocalExtraFilter(key, v)}
                                    />
                                </div>
                            ))}
                            {
                                loading ?
                                    <div className="flex flex-row gap-3">
                                        <Skeleton className="h-5 w-[100px] bg-primary" />
                                        <Skeleton className="h-5 w-[20px] bg-primary" />
                                    </div>
                                    :
                                    <div className={`${searchStyle["basic-filter-item"]}`}>
                                        <FilterWithDialog
                                            label={t("searchView:salary")}
                                            value={[localExtraFilters.salaryFrom ?? "", localExtraFilters.salaryTo ?? ""]}
                                            onChange={setLocalExtraFilter}>
                                        </FilterWithDialog>
                                    </div>
                            }

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