import "../../../styles/SearchView.css";

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
                <div className="frame-158">
                    <div className="frame-13">
                    <div className="search-company-keyword">Search, company, keyword ...</div>
                    </div>
                    <div className="frame-9">
                    <div className="major-of-study">Major of study</div>
                    </div>
                    <div className="frame-12">
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
    );
}
 
export default Search;