type educationSection = {
    educations: string[] | null
}

export function EducationSection({ educations }: educationSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required education</div>
                    <div className="line-10"></div>
                </div>
                <div className="skill-section-content">
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="education" src="/icons/light/educationLight.svg" />
                            <div className="city-name">EducationName</div>
                        </div>
                        <div className="skill-section-item2">
                            <img className="education2" src="/icons/light/educationLight.svg" />
                            <div className="city-name">EducationName</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="education3" src="/icons/light/educationLight.svg" />
                            <div className="city-name">EducationName</div>
                        </div>
                    </div>
                    <div className="skill-section-row">
                        <div className="skill-section-item3">
                            <img className="education4" src="/icons/light/educationLight.svg" />
                            <div className="city-name">EducationName</div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="frame-172">
                <div className="main-button">
                    <div className="find-mathing-job">Show more (20)</div>
                </div>
            </div>
        </div>
    );
}