export function DynamicFilter(){
    return (
        <div className="offer-list-filter-section">
            <div className="filter-header">
              <img className="filter-icon" src="/icons/filter-icon0.svg" />
              <div className="filter-title">FilterTitle</div>
              <img className="chevron-up" src="/icons/chevron-up0.svg" />
            </div>
            <div className="checkboxes">
              <div className="offer-list-filter-item">
                <div className="frame-20">
                  <img className="check" src="/icons/check0.svg" />
                </div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
              <div className="offer-list-filter-item">
                <div className="frame-20"></div>
                <div className="option-long-name-name">OptionLongNameName</div>
              </div>
            </div>
          </div>
    );
}