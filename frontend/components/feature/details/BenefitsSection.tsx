import { useState } from "react";
import detailsStyles from "../../../styles/OfferDetails.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import { useTranslation } from "react-i18next";

type benefitsSection = {
    benefits: string[] | null;
};

const BENEFITS_PER_ROW = 3;
const INITIAL_VISIBLE = 6;

const nullEle = (
    <div className={detailsStyles["skill-section-row"]}>
        <div className={detailsStyles["skill-section-item1"]}>
            <img
                className={detailsStyles["gift"]}
                src="/icons/light/giftLight.svg"
            />
            <div className={detailsStyles["city-name"]}>-</div>
        </div>
    </div>
);

export function BenefitsSection({ benefits }: benefitsSection) {
    const [expanded, setExpanded] = useState(false);
    const {t} = useTranslation("details");

    const visibleBenefits = expanded
        ? benefits
        : benefits?.slice(0, INITIAL_VISIBLE);

    const rowsCount = Math.ceil(
        (visibleBenefits?.length ?? 0) / BENEFITS_PER_ROW
    );

    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>{t("benefits")}</div>
                    <div className={detailsStyles["line-10"]} />
                </div>

                <div className={`${detailsStyles["skill-section-content"]} thight-section`}>
                    {(benefits && benefits.length > 0) ? Array.from({ length: rowsCount }).map((_, rowIndex) => {
                        const start = rowIndex * BENEFITS_PER_ROW;
                        const rowItems = visibleBenefits?.slice(
                            start,
                            start + BENEFITS_PER_ROW
                        );

                        return (
                            <div
                                key={rowIndex}
                                className={detailsStyles["skill-section-row"]}
                            >
                                {rowItems?.map((benefit, idx) => (
                                    <div
                                        key={idx}
                                        className={detailsStyles[`skill-section-item${idx + 1}`]}
                                    >
                                        <img
                                            className={detailsStyles["gift"]}
                                            src="/icons/light/giftLight.svg"
                                        />
                                        <div className={detailsStyles["city-name"]}>
                                            {benefit}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        );
                    }): nullEle}
                </div>
            </div>

            {benefits && (benefits.length > INITIAL_VISIBLE && (
                <div className={detailsStyles["frame-172"]}>
                    <div
                        className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}
                        onClick={() => setExpanded((prev) => !prev)}
                    >
                        <div className={detailsStyles["find-mathing-job"]}>
                            {expanded
                                ? t("showLess")
                                : `${t("showMore")} (${benefits.length - INITIAL_VISIBLE})`}
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
}
