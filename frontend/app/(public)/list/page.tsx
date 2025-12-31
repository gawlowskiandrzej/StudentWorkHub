"use client";
import React, { useMemo, useState } from "react";
import { ListElement } from "@/components/feature/list/ListElement";
import offersJson from '@/store/data/DummyOffers.json';
import listStyles from "../../../styles/OfferList.module.css";
import dynamicFilterStyles from "../../../styles/DynamicFilter.module.css";
import buttonStyle from "../../../styles/ButtonStyle.module.css";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { DynamicFilter } from "@/components/feature/list/Filters";
import { RecentSearches } from "@/components/feature/list/RecentSearches";
import { Filter } from "@/components/feature/search/Filters";
import { Search } from "@/types/search/search";
import { useSearch } from "@/store/SearchContext";
import { Pagination } from "@/components/feature/list/Pagination";
import { mapApiFilters } from "@/utils/offerFilters/mapToDynamicFilter";
import { useTranslation } from "react-i18next";
import { FilterKey, FilterValue } from "@/types/details/dynamicFilter";
import { matchOfferToFilters } from "@/utils/offerFilters/matchOfferToFilters";

export type FiltersState = Partial<
  Record<FilterKey, Set<FilterValue>>
>;

export default function OfferList() {
  const {search, filters, toggleFilter} = useSearch();
  const [offset, setOffset] = useState(0);
  const [localSearch, setLocalSearch]  = useState<Search>({
    keyword: search?.keyword || "",
    category: search?.category || "",
    city: search?.city || "",
  });
  const [sorts, setSort] = useState<{ sort?: string }>({ sort: "CreationDate" });
  const updateFilter = (key: "sort", value: string) => {
    setSort((prev) => ({
      ...prev,
      [key]: value,
    }));
  };

  const filteredOffers = useMemo(() => {
  return offersJson.pagination.items.filter(offer =>
    matchOfferToFilters(offer, filters)
  );
}, [filters]);
  const {t} = useTranslation(["common", "list"]);
  const items = [
    { label: t("list:sort.creationAsc"), value: "CreationDate" },
    { label: t("list:sort.salaryDesc"), value: "Salarydesc" },
    { label: t("list:sort.salaryDesc"), value: "Salaryasc" },
    { label: t("list:sort.nameAsc"), value: "Nameasc" },
  ];
  const dynamicFilters = mapApiFilters(offersJson.dynamicFilters)
  return (
      <div className={listStyles["offer-list-view"]}>
        <div className={listStyles["search-bar-component"]}>
          <div className={listStyles["search-bar-list"]}>
            <SearchBar localSearch={localSearch} setLocalSearch={setLocalSearch} />
            <div className={`${buttonStyle["main-button"]}`}>
              <img className={listStyles["search"]} src="/icons/search0.svg" />
              <div className={buttonStyle["find-matching-job"]}>{t("findMatchingJob")}</div>
            </div>
          </div>
          <RecentSearches setSearch={setLocalSearch} />
        </div>
        <div className={listStyles["offers-list"]}>
          <div className={dynamicFilterStyles["dynamic-filter"]}>
            {dynamicFilters.map((filter) => (
              <DynamicFilter key={filter.key}
              filterKey={filter.key}
              header={filter.header}
              items={filter.items}
              selected={filters[filter.key]}
              onChange={toggleFilter}
                />
            ))}
          </div>
          <div className={listStyles["list-with-filter"]}>
            <div className={listStyles["filternav"]}>
              <div className={listStyles["offer-list-sort-select"]}>
                <div className={listStyles["sort-by"]}>{t("list:sortByTitle")}</div>
                <Filter className={`${listStyles["creation-date"]}`}
                  label="Sort"
                  clearable={false}
                  items={items}
                  onChange={(v) => updateFilter("sort", v)}
                  value={sorts.sort}>
                </Filter>
              </div>
              <Pagination offset={offset} limit={10} count={filteredOffers.length} onChange={setOffset} />
            </div>
            {filteredOffers.map((offer) => (
              <ListElement key={offer.id} offer={offer} />
            ))}
          </div>
        </div>
        <div className={listStyles["second-pagination"]}>
          <Pagination offset={offset} limit={10} count={filteredOffers.length} onChange={setOffset} />
        </div>
      </div>
  );
}