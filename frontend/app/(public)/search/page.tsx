"use client";
import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";
import { Filter } from "@/components/feature/search/Filters";
import { useState } from "react";
import { SearchBar } from "@/components/feature/search/SearchBar";
import { FilterWithDialog } from "@/components/feature/search/FilterWithDialog";
import { useRouter } from 'next/navigation';

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
    const router = useRouter();
    const items = [
        { label: "Worktype1", value: "Worktype1" },
        { label: "Worktype2", value: "Worktype2" },
        { label: "Worktype3", value: "Worktype3" },
    ]; 
    const gotoListPage = () => {
    router.push('/list');
    };
    return (
        <div className="search-view">
            <div className="search-view-content">
                <div className="search-view-header">
                    <span>
                        <span className="search-view-header-span">
                            We have many offers for you.
                        </span>
                        <span className="search-view-header-span2"> Let them find you.</span>
                    </span>
                </div>
                    <SearchBar>
                    </SearchBar>
                    <div className="search-sub-section">
                        <div className="sub-filters">
                            <div className="basic-filter-item">
                                <Filter
                                    label="Work type"
                                    items={items}
                                    onChange={(v) => updateFilter("workType", v)}
                                    value={filters.workType}>
                                </Filter>
                            </div>
                            <div className="basic-filter-item">
                                <Filter
                                    label="Work time"
                                    items={items}
                                    onChange={(v) => updateFilter("workTime", v)}
                                    value={filters.workTime}>
                                </Filter>
                            </div>
                            <div className="basic-filter-item">
                                <Filter
                                    label="Employment type"
                                    items={items}
                                    onChange={(v) => updateFilter("employmentType", v)}
                                    value={filters.employmentType}>
                                </Filter>
                            </div>
                            <div className="basic-filter-item">
                                <FilterWithDialog
                                    label="Salary"
                                    items={items}
                                    onChange={(v) => updateFilter("salary", v)}
                                    value={filters.salary}>
                                </FilterWithDialog>
                            </div>
                        </div>
                        <div onClick={gotoListPage} className="main-button">
                            <img id="searchVec" className="search" src="/icons/search0.svg" />
                            <button className="find-matching-job">Find matching jobs</button>
                        </div>
                </div>
            </div>
        </div>
    );
}

export default Search;