"use client";
import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import "../../../styles/NowyStyl.css";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import { Filter } from "@/components/feature/search/Filters";
import { useState } from "react";
import { SearchBar } from "@/components/feature/search/SearchBar";

type FiltersState = {
  workType?: string;
  workTime?: string;
  employmentType?: string;
  salary?: string;
};

const Search = () => {
    const [filters, setFilters] = useState<FiltersState>({});
    const updateFilter = (key: keyof FiltersState, value: string) => {
    setFilters((prev) => ({
        ...prev,
        [key]: value,
    }));
    };
    const items = [
        {label: "Worktype1", value: "Worktype1"},
        {label: "Worktype2", value: "Worktype2"},
        {label: "Worktype3", value: "Worktype3"},
    ];

    return ( 
        <div className="search-view">
  <div className="search-view-content">
    <div className="search-view-header">
      <span>
        <span className="search-view-header-span">
          We have many offers for you.
        </span>
        <span className="search-view-header-span2">Let them find you.</span>
      </span>
    </div>
    <div className="frame-157">
      <div className="search-section">
        <div className="searchbar">
          <div className="phrase-search">
            <div className="div">Search, company, keyword ...</div>
          </div>
          <div className="major-study-search">
            <div className="major">Major of study</div>
          </div>
          <div className="city-search">
            <div className="city">City</div>
          </div>
        </div>
      </div>
      <div className="search-sub-section">
        <div className="sub-filters">
          <div className="basic-filter-item">
            <div className="work-type">Work type</div>
            <img className="vector" src="vector0.svg" />
          </div>
          <div className="basic-filter-item">
            <div className="work-type">Work time</div>
            <img className="vector2" src="vector1.svg" />
          </div>
          <div className="basic-filter-item">
            <div className="work-type">Employment type</div>
            <img className="vector3" src="vector2.svg" />
          </div>
          <div className="basic-filter-item">
            <div className="work-type">Salary</div>
            <img className="vector4" src="vector3.svg" />
          </div>
        </div>
        <div className="main-button">
          <img className="search" src="search0.svg" />
          <div className="find-matching-job">Find matching job</div>
        </div>
      </div>
    </div>
  </div>
</div>

    );
}
 
export default Search;