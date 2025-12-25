"use client";
import React, { useState } from "react";
import { ListElement } from "@/components/feature/list/ListElement";
import offersJson from '@/store/data/DummyOffers.json';
import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import "../../../styles/OfferList.css";
import "../../../styles/ButtonStyle.css";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { DynamicFilter } from "@/components/feature/list/Filters";
import { RecentSearches } from "@/components/feature/list/RecentSearches";
import { Filter } from "@/components/feature/search/Filters";
import { Search } from "@/types/search/search";
import { useSearch } from "@/store/SearchContext";
import { Pagination } from "@/components/feature/list/Pagination";
import { mapToDynamicFilter } from "@/utils/offerFilters/mapToDynamicFilter";

export default function OfferList() {
  const {search} = useSearch();
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
  const items = [
    { label: "Creation Date", value: "CreationDate" },
    { label: "Salary desc", value: "Salarydesc" },
    { label: "Salary asc", value: "Salaryasc" },
    { label: "Name asc", value: "Nameasc" },
  ];
  const dynamicFilters = [
    mapToDynamicFilter("Stopień doświadczenia", offersJson.dynamicFilters.experienceLevels),
    mapToDynamicFilter("Doświadczenie", offersJson.dynamicFilters.experienceMonths),
    mapToDynamicFilter("Stopnień edukacji", offersJson.dynamicFilters.educationNames),
    mapToDynamicFilter("Języki", offersJson.dynamicFilters.languagesNames),
  ]
  return (
      <div className="offer-list-view">
        <div className="search-bar-component">
          <div className="search-bar-list">
            <SearchBar localSearch={localSearch} setLocalSearch={setLocalSearch} />
            <div className="main-button cursor-pointer">
              <img className="search" src="/icons/search0.svg" />
              <div className="find-matching-job">Find matching job</div>
            </div>
          </div>
          <RecentSearches setSearch={setLocalSearch} />
        </div>
        <div className="offers-list">
          <div className="dynamic-filter">
            {dynamicFilters.map((filter, index) => (
              <DynamicFilter key={index} header={filter.header} items={filter.items} />
            ))}
          </div>
          <div className="list-with-filter">
            <div className="filternav">
              <div className="offer-list-sort-select">
                <div className="sort-by">Sort by:</div>
                <Filter className="creation-date cursor-pointer"
                  label="Sort"
                  clearable={false}
                  items={items}
                  onChange={(v) => updateFilter("sort", v)}
                  value={sorts.sort}>
                </Filter>
              </div>
              <Pagination offset={offset} limit={10} count={offersJson.pagination.items.length} onChange={setOffset} />
            </div>
            {offersJson.pagination.items.map((offer) => (
              <ListElement key={offer.id} offer={offer} />
            ))}
          </div>
        </div>
        <div className="second-pagination">
          <Pagination offset={offset} limit={10} count={offersJson.pagination.items.length} onChange={setOffset} />
        </div>
      </div>
  );
}