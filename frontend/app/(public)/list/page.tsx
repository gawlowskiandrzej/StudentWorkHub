"use client";
import { useEffect, useState } from "react";
import { ListElement } from "@/components/feature/list/ListElement";
import offersJson from '@/store/data/DummyOffers.json';
import listStyles from "../../../styles/OfferList.module.css";
import dynamicFilterStyles from "../../../styles/DynamicFilter.module.css";
import buttonStyle from "../../../styles/ButtonStyle.module.css";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { DynamicFilter } from "@/components/feature/list/Filters";
import { LastSearches } from "@/components/feature/list/RecentSearches";
import { Filter } from "@/components/feature/search/Filters";
import { useSearch } from "@/store/SearchContext";
import { Pagination } from "@/components/feature/list/Pagination";
import { mapApiFilters } from "@/utils/offerFilters/mapToDynamicFilter";
import { useTranslation } from "react-i18next";
import { FilterKey, FilterValue } from "@/types/details/dynamicFilter";
import { usePagination } from "@/store/PaginationContext";
import { useOfferList } from "@/hooks/useOfferList";

export type FiltersState = Partial<
  Record<FilterKey, Set<FilterValue>>
>;

export default function OfferList() {
  const { filters, toggleFilter, sorts, setSorting } = useSearch();
  const { limit, offset, setOffset } = usePagination();
  

  const { offers, total } = useOfferList(
    filters,
    sorts.sort!,
    offset,
    limit
  );

  const { t } = useTranslation(["common", "list"]);
  const items = [
    { label: t("list:sort.idDesc"), value: "IdDesc" },
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
          <SearchBar />
          <div className={`${buttonStyle["main-button"]}`}>
            <img className={listStyles["search"]} src="/icons/search0.svg" />
            <div className={buttonStyle["find-matching-job"]}>{t("findMatchingJob")}</div>
          </div>
        </div>
        <LastSearches />
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
                onChange={(v) => setSorting("sort", v)}
                value={sorts.sort}>
              </Filter>
            </div>
            <Pagination offset={offset} limit={limit} count={total} onChange={setOffset} />
          </div>
          {offers.map((offer) => (
            <ListElement key={offer.id} offer={offer} />
          ))}
          <div className={listStyles["second-pagination"]}>
            <Pagination offset={offset} limit={limit} count={total} onChange={setOffset} />
          </div>
        </div>
        
      </div>
      
    </div>
  );
}