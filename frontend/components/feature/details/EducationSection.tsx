type educationSection = {
    educations: string[] | null
}
const EDUCATION_PER_ROW = 3;
const nullEle =
    <div className="skill-section-row">
        <div className={`skill-section-item1`}>
            <img
                className={`education`}
                src="/icons/light/educationLight.svg"
                alt=""
            />
            <div className="city-name">-</div>
        </div></div>

export function EducationSection({ educations }: educationSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required education</div>
                    <div className="line-10"></div>
                </div>
                <div className="skill-section-content thight-section">
                    {educations ? Array.from({ length: Math.ceil(Math.min(educations.length, 6) / EDUCATION_PER_ROW) }).map((_, rowIndex) => {
                        const start = rowIndex * EDUCATION_PER_ROW;
                        const rowItems = educations.slice(start, start + EDUCATION_PER_ROW);
                        return (
                            <div key={rowIndex} className="skill-section-row">
                                {rowItems.map((education, idx) => (
                                    <div key={idx} className={`skill-section-item${idx + 1}`}>
                                        <img
                                            className={`education`}
                                            src="/icons/light/educationLight.svg"
                                            alt=""
                                        />
                                        <div className="city-name">{education}</div>
                                    </div>
                                ))}
                            </div>
                        );
                    }) : nullEle}
                </div>
            </div>
            {educations ? educations.length > 5 ?
                <div className="frame-172">
                    <div className="main-button">
                        <div className="find-mathing-job">Show more ({educations.length - 5})</div>
                    </div>
                </div>
                : ""
                : ""
            }
        </div>
    );
}