"use client";
import React, { useState } from "react";
import { ListElement } from "@/components/feature/list/ListElement";

import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import "../../../styles/OfferList.css";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { DynamicFilter } from "@/components/feature/list/Filters";
import { RecentSearches } from "@/components/feature/list/RecentSearches";
import { Filter } from "@/components/feature/search/Filters";
import { Search } from "@/types/search/search";
import { useSearch } from "@/context/SearchContext";

export default function OfferList() {
  const {search} = useSearch();
  const [localSearch, setLocalSearch]  = useState<Search>({
    keyword: search?.keyword || "",
    category: search?.category || "",
    city: search?.city || "",
  });
  const offers = Array.from({ length: 3 }, (_, i) => (
    <ListElement key={i} />
  ));
  const [filters, setFilters] = useState<{ sort?: string }>({ sort: "CreationDate" });
  const updateFilter = (key: "sort", value: string) => {
    setFilters((prev) => ({
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
            <DynamicFilter />
            <DynamicFilter />
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
                  value={filters.sort}>
                </Filter>
              </div>
              <div className="offer-list-pagination">
                <div className="offer-count">
                  <div className="_100">1</div>
                </div>
                <div className="from">from</div>
                <div className="_100">100</div>
              </div>
            </div>
            {offers}
          </div>
        </div>
      </div>
  );
}