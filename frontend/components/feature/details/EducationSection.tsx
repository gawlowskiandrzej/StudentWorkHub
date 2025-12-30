import detailsStyles from '../../../styles/OfferDetails.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

type educationSection = {
    educations: string[] | null
}
const EDUCATION_PER_ROW = 3;
const INITIAL_VISIBLE = 6;
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
    const [expanded, setExpanded] = useState(false);
    const {t} = useTranslation("details");
    const visibleEducation = expanded
        ? educations
        : educations?.slice(0, INITIAL_VISIBLE);
    const rowsCount = Math.ceil((visibleEducation?.length ?? 0) / EDUCATION_PER_ROW);
    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>{t("requiredEducation")}</div>
                    <div className={detailsStyles["line-10"]}></div>
                </div>
                <div className={detailsStyles["skill-section-content thight-section"]}>
                    {educations ? Array.from({ length: rowsCount }).map((_, rowIndex) => {
                        const start = rowIndex * EDUCATION_PER_ROW;
                        const rowItems = visibleEducation?.slice(
                            start,
                            start + EDUCATION_PER_ROW
                        );
                        return (
                            <div
                                key={rowIndex}
                                className={detailsStyles["skill-section-row"]}
                            >
                                {rowItems?.map((education, idx) => (
                                    <div
                                        key={idx}
                                        className={detailsStyles[`skill-section-item${idx + 1}`]}
                                    >
                                        <img
                                            className={detailsStyles["education"]}
                                            src="/icons/light/educationLight.svg"
                                            alt='education'
                                        />
                                        <div className={detailsStyles["city-name"]}>
                                            {education}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        );
                    }) : nullEle}
                </div>
            </div>
            {educations ? educations.length > INITIAL_VISIBLE ?
                <div className={detailsStyles["frame-172"]}>
                    <div onClick={() => setExpanded((prev) => !prev)}  className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}>
                        <div className={detailsStyles["find-mathing-job"]}>{expanded
                                ? t("showLess")
                                : `${t("showMore")} (${educations.length - INITIAL_VISIBLE})`}</div>
                    </div>
                </div>
                : ""
                : ""
            }
        </div>
    );
}