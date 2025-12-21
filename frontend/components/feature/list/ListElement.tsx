"use client";

export function ListElement() {
    return (
            <div className="offer-list-header">
                <img className="company" src="/icons/company0.svg" />
                <div className="offer-header">
                    <div className="offer-header-info">
                    <div className="offer-header-content">
                        <div className="offer-header2">
                            <div className="job-title">JobTitle</div>
                            <div className="company-name">Company name</div>
                        </div>
                        <div className="offer-header-desc">
                            <div className="job-header-sub-item">
                                <img className="vector" src="/icons/location.svg" />
                                <div className="address">Address</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector" src="/icons/house.svg" />
                                <div className="employment-type">Employment type</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector" src="/icons/document.svg" />
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
            </div>
    );
}