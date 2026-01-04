"use client";
import { ListElement } from "@/components/feature/list/ListElement";
import listStyles from "../../../styles/OfferList.module.css";
import dynamicFilterStyles from "../../../styles/DynamicFilter.module.css";
import detailsStyles from "../../../styles/OfferDetails.module.css";
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
import { SearchFilterKeyword } from "@/components/feature/list/SearchFilterKeyword";
import { searchKeywordFilters } from "@/store/data/searchKeywordFilterData";
import { ListElementSkeleton } from "@/components/feature/list/ListElementSkeleton";
import { useMemo, useState } from "react";
import { useOffers } from "@/store/OffersContext";
import { DynamicFilterSkeleton } from "@/components/feature/list/DynamicFilterSkeleton";
import { Button } from "@/components/ui/button";
import { Spinner } from "@/components/ui/spinner";
import { Plus } from "lucide-react";
import { search } from "@/types/search/search";

export type FiltersState = Partial<
  Record<FilterKey, Set<FilterValue>>
>;

export default function OfferList() {
  const { search, setSearch, filters, toggleFilter, sorts, searchFilterKeywords, setSearchFilterKeywords, setSorting, addRecentSearch, setHasSearched, clearFilters } = useSearch();
  const [localSearch, setLocalSearch] = useState(search || {});
  const { limit, offset, setOffset } = usePagination();
  const { t } = useTranslation(["common", "list"]);
  const { offersResponse, loading, startScrapping, scrapping } = useOffers();
  const offers = offersResponse?.pagination.items ?? [];
  const [la, setLa] = useState(false);


  const { filteredOffers, total } = useOfferList(
    filters,
    sorts.sort!,
    offset,
    limit,
    searchFilterKeywords,
    offers
  );
  const items = [
    { label: t("list:sort.idDesc"), value: "IdDesc" },
    { label: t("list:sort.creationAsc"), value: "CreationDate" },
    { label: t("list:sort.salaryDesc"), value: "Salarydesc" },
    { label: t("list:sort.salaryDesc"), value: "Salaryasc" },
    { label: t("list:sort.nameAsc"), value: "Nameasc" },
  ];
  const searchNew = () => {
        addRecentSearch(localSearch);
        setSearch(localSearch);
        setHasSearched(true);
    };
  const dynamicFilters = useMemo(() => mapApiFilters(offersResponse?.dynamicFilter), [offersResponse?.dynamicFilter]);

  return (
    <div className={listStyles["offer-list-view"]}>
      <div className={listStyles["search-bar-component"]}>
        <div className={listStyles["search-bar-list"]}>
          <SearchBar value={localSearch} onChange={setLocalSearch} />
          <div className={listStyles["search-bar-buttons"]}>
            <Button
              disabled={scrapping}
              style={{ width: la ? 210 : 185 }} // px
              className={`transition-[width] duration-300 ease-in-out py-5.5 px-3 inline-flex items-center cursor-pointer justify-center overflow-hidden ${buttonStyle["big-scale"]}`}
              onClick={startScrapping}
            >
              <Plus/>
              {scrapping ? "Scrapowanie..." : "Pozyskaj nowe"}
              {scrapping && <Spinner />}
            </Button>
            <div onClick={searchNew} className={`${buttonStyle["main-button"]}`}>
              <img className={listStyles["search"]} src="/icons/search0.svg" /> {t("findMatchingJob")}
            </div>
          </div>
        </div>
        <LastSearches />
      </div>
      <div className={listStyles["offers-list"]}>
        <div className={dynamicFilterStyles["dynamic-filter"]}>
          {loading
            ? <DynamicFilterSkeleton />
            : dynamicFilters.map((filter) => (
              <DynamicFilter key={filter.key}
                filterKey={filter.key}
                header={filter.header}
                items={filter.items}
                selected={filters[filter.key]}
                onChange={toggleFilter}
              />
            ))}
          {searchKeywordFilters.map((keywordFilter, index) => (
            <SearchFilterKeyword key={index} filterKey={keywordFilter.filterKey} header={keywordFilter.header} value={searchFilterKeywords[keywordFilter.filterKey]} onChange={setSearchFilterKeywords} />
          ))}
          <div className={`${buttonStyle["main-button"]} ${detailsStyles["show-more-button"]}`} onClick={() => clearFilters()}>
            <div className={`${detailsStyles["find-mathing-job"]} text-sm`}>
              Wyczyść filtry
            </div>
          </div>
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
            <Pagination loading={loading} offset={offset} limit={limit} count={total} onChange={setOffset} />
          </div>
          {loading ? (
            Array.from({ length: 10 }).map((_, i) => <ListElementSkeleton key={i} />)
          ) : (
            filteredOffers.map((offer) => <ListElement key={offer.id} offer={offer} />)
          )}
          <div className={listStyles["second-pagination"]}>
            <Pagination loading={loading} offset={offset} limit={limit} count={total} onChange={setOffset} />
          </div>
        </div>

      </div>

    </div>
  );
}

