import detailsStyles from '../../../styles/OfferDetails.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'

type educationSection = {
    educations: string[] | null
}
const EDUCATION_PER_ROW = 3;
const nullEle =
    <div className={detailsStyles["skill-section-row"]}>
        <div className={detailsStyles["skill-section-item1"]}>
            <img
                className={`education`}
                src="/icons/light/educationLight.svg"
                alt=""
            />
            <div className={detailsStyles["city-name"]}>-</div>
        </div></div>

export function EducationSection({ educations }: educationSection) {
    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>Required education</div>
                    <div className={detailsStyles["line-10"]}></div>
                </div>
                <div className={detailsStyles["skill-section-content thight-section"]}>
                    {educations ? Array.from({ length: Math.ceil(Math.min(educations.length, 6) / EDUCATION_PER_ROW) }).map((_, rowIndex) => {
                        const start = rowIndex * EDUCATION_PER_ROW;
                        const rowItems = educations.slice(start, start + EDUCATION_PER_ROW);
                        return (
                            <div key={rowIndex} className={detailsStyles["skill-section-row"]}>
                                {rowItems.map((education, idx) => (
                                    <div key={idx} className={`${detailsStyles[`skill-section-item${idx + 1}`]}`}>
                                        <img
                                            className={detailsStyles[`education`]}
                                            src="/icons/light/educationLight.svg"
                                            alt=""
                                        />
                                        <div className={detailsStyles["city-name"]}>{education}</div>
                                    </div>
                                ))}
                            </div>
                        );
                    }) : nullEle}
                </div>
            </div>
            {educations ? educations.length > 5 ?
                <div className={detailsStyles["frame-172"]}>
                    <div className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}>
                        <div className={detailsStyles["find-mathing-job"]}>Show more ({educations.length - 5})</div>
                    </div>
                </div>
                : ""
                : ""
            }
        </div>
    );
}