"use client";
import React from "react";
import { ListElement } from "@/components/feature/list/ListElement";
import "../../../styles/OfferList.css";
import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import { SearchBar } from "@/components/feature/search/SearchBar";

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
            <img className="search" src="search0.svg" />
            <div className="find-matching-job">Find matching job</div>
          </div>
        </div>
        <div className="recent-searches-navigation">
          <div className="frame-38">
            <img className="history" src="history0.svg" />
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
          <div className="offer-list-filter-section">
            <div className="filter-header">
              <img className="filter-icon" src="filter-icon0.svg" />
              <div className="filter-title">FilterTitle</div>
              <img className="chevron-up" src="chevron-up0.svg" />
            </div>
            <div className="checkboxes">
              <div className="offer-list-filter-item">
                <div className="frame-20">
                  <img className="check" src="check0.svg" />
                </div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
              <div className="offer-list-filter-item">
                <div className="frame-20"></div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
            </div>
          </div>
          <div className="offer-list-filter-section2">
            <div className="filter-header">
              <img className="filter-icon2" src="filter-icon1.svg" />
              <div className="filter-title">FilterTitle</div>
              <img className="chevron-up2" src="chevron-up1.svg" />
            </div>
            <div className="checkboxes">
              <div className="offer-list-filter-item">
                <div className="frame-20">
                  <img className="check2" src="check1.svg" />
                </div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
              <div className="offer-list-filter-item">
                <div className="frame-20"></div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
            </div>
          </div>
        </div>
        <div className="list-with-filter">
          <div className="filternav">
            <div className="offer-list-sort-select">
              <div className="sort-by">Sort by:</div>
              <div className="creation-date">creation date</div>
              <img className="chevron-down" src="chevron-down0.svg" />
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
          {/* <div className="offer-list-header">
            <img className="company" src="company0.svg" />
            <div className="offer-header">
              <div className="offer-header-content">
                <div className="offer-header2">
                  <div className="job-title">JobTitle</div>
                  <div className="company-name">Company name</div>
                </div>
                <div className="offer-header-desc">
                  <div className="job-header-sub-item">
                    <img className="vector" src="vector0.svg" />
                    <div className="address">Address</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector2" src="vector1.svg" />
                    <div className="employment-type">Employment type</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector3" src="vector2.svg" />
                    <div className="employment-schedules">Employment schedules</div>
                  </div>
                </div>
              </div>
              <div className="offer-header-salary-date">
                <div className="added-10-12-2025">Added 10.12.2025</div>
                <div className="job-salary">Job salary</div>
              </div>
            </div>
          </div>
          <div className="offer-list-header">
            <img className="company2" src="company1.svg" />
            <div className="offer-header">
              <div className="offer-header-content">
                <div className="offer-header2">
                  <div className="job-title">JobTitle</div>
                  <div className="company-name">Company name</div>
                </div>
                <div className="offer-header-desc">
                  <div className="job-header-sub-item">
                    <img className="vector4" src="vector3.svg" />
                    <div className="address">Address</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector5" src="vector4.svg" />
                    <div className="employment-type">Employment type</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector6" src="vector5.svg" />
                    <div className="employment-schedules">Employment schedules</div>
                  </div>
                </div>
              </div>
              <div className="offer-header-salary-date">
                <div className="added-10-12-2025">Added 10.12.2025</div>
                <div className="job-salary">Job salary</div>
              </div>
            </div>
          </div>
          <div className="offer-list-header">
            <img className="company3" src="company2.svg" />
            <div className="offer-header">
              <div className="offer-header-content">
                <div className="offer-header2">
                  <div className="job-title">JobTitle</div>
                  <div className="company-name">Company name</div>
                </div>
                <div className="offer-header-desc">
                  <div className="job-header-sub-item">
                    <img className="vector7" src="vector6.svg" />
                    <div className="address">Address</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector8" src="vector7.svg" />
                    <div className="employment-type">Employment type</div>
                  </div>
                  <div className="job-header-sub-item">
                    <img className="vector9" src="vector8.svg" />
                    <div className="employment-schedules">Employment schedules</div>
                  </div>
                </div>
              </div>
              <div className="offer-header-salary-date">
                <div className="added-10-12-2025">Added 10.12.2025</div>
                <div className="job-salary">Job salary</div>
              </div>
            </div>
          </div> */}
        </div>
      </div>
    </div>

  );
}