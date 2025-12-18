"use client";
import React from "react";
import { ListElement } from "@/components/feature/list/ListElement";

import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import "../../../styles/OfferList.css";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { DynamicFilter } from "@/components/feature/list/Filters";

export default function OfferList() {
  const offers = Array.from({ length: 3 }, (_, i) => (
  <ListElement key={i} />
));  
  return (
    <div className="offer-list-view">
      <div className="search-bar-component">
        <div className="search-bar-list">
          {/* Tutaj wstawić search bar */}
          <SearchBar/>
          <div className="main-button">
            <img className="search" src="/icons/search0.svg" />
            <div className="find-matching-job">Find matching job</div>
          </div>
        </div>
        <div className="recent-searches-navigation">
          <div className="frame-38">
            <img className="history" src="/icons/history0.svg" />
            <div className="recent-searches">Recent searches:</div>
          </div>
          <div className="keyword">keyword</div>
          <div className="keyword">keyword</div>
          <div className="keyword">keyword</div>
          <div className="keyword">keyword</div>
          <div className="keyword">keyword</div>
        </div>
      </div>
      <div className="offers-list">
        <div className="dynamic-filter">
          <DynamicFilter/>
          <DynamicFilter/>
        </div>
        <div className="list-with-filter">
          <div className="filternav">
            <div className="offer-list-sort-select">
              <div className="sort-by">Sort by:</div>
              <div className="creation-date">creation date</div>
              <img className="chevron-down" src="/icons/chevron-down0.svg" />
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