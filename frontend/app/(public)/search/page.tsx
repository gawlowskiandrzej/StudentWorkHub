import "../../../styles/SearchView.css";
import "../../../styles/Hero.css";

const Search = () => {
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
                <div className="search-section">
                <div className="searchbar">
                    <div className="phrase-search">
                    <div className="phrase">Search, company, keyword ...</div>
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
                    </div>
                    <div className="basic-filter-item">
                    <div className="work-type">Work time</div>
                    </div>
                    <div className="basic-filter-item">
                    <div className="work-type">Employment type</div>
                    </div>
                    <div className="basic-filter-item">
                    <div className="work-type">Salary</div>
                    </div>
                </div>
                <div className="main-button">
                    <img id="searchVec" className="search" src="/icons/search0.svg" />
                    <div className="find-matching-job">Find matching job</div>
                </div>
                </div>
            </div>
            </div>
    );
}
 
export default Search;