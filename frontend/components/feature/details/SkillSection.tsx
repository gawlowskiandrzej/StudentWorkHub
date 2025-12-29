import { skill } from "@/types/list/Offer/skill";
import detailsStyles from '../../../styles/OfferDetails.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'
import { useMemo, useState } from "react";

type skillSection = {
    offerSkills: skill[]
}

export function SkillsSection({ offerSkills }: skillSection) {
    const [expanded, setExpanded] = useState(false);
    const sortedSkills = useMemo(() => {
        return [...offerSkills].sort((a, b) => {
            if (a.experienceMonths != b.experienceMonths) {
                return (b.experienceMonths ?? 0) - (a.experienceMonths ?? 0);
            }
            const levelA = (a.experienceLevel ?? []).sort().join(", ");
            const levelB = (b.experienceLevel ?? []).sort().join(", ");
            return levelB.localeCompare(levelA);
        });
    }, [offerSkills]);
    const nullEle =
        <div className={detailsStyles["skill-section-row"]}>
            <div className={detailsStyles["skill-section-item1"]}>
                <img
                    className={detailsStyles["check-circle"]}
                    src="/icons/light/check-circleLight.svg"
                />
                <div className={detailsStyles["city-name"]}>
                    -
                </div>
            </div>
        </div>
    const visibleSkills = expanded
        ? sortedSkills
        : sortedSkills.slice(0, 5);

    return (
        <div className={`${detailsStyles["required-skills-section"]}`}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>
                        Required skills
                    </div>
                    <div className={detailsStyles["line-10"]} />
                </div>

                <div
                    className={`${detailsStyles["skill-section-content"]} ${detailsStyles["thighter-section"]}`}
                >
                    {visibleSkills.length > 0 ? visibleSkills.map((offer, index) => (
                        <div key={index} className={detailsStyles["skill-section-row"]}>
                            <div className={detailsStyles["skill-section-item1"]}>
                                <img
                                    className={detailsStyles["check-circle"]}
                                    src="/icons/light/check-circleLight.svg"
                                />
                                <div className={detailsStyles["city-name"]}>
                                    {offer.skill ?? "-"}
                                </div>
                            </div>

                            <div className={detailsStyles["skill-section-item2"]}>
                                <img
                                    className={detailsStyles["pie-chart2"]}
                                    src="/icons/light/clockLight.svg"
                                />
                                <div className={detailsStyles["city-name"]}>
                                    {offer.experienceMonths
                                        ? `${offer.experienceMonths / 12} lata`
                                        : "-"}
                                </div>
                            </div>

                            <div className={detailsStyles["skill-section-item3"]}>
                                <img
                                    className={detailsStyles["award"]}
                                    src="/icons/light/awardLight.svg"
                                />
                                <div className={detailsStyles["city-name"]}>
                                    {offer.experienceLevel?.length
                                        ? offer.experienceLevel.join(" / ")
                                        : "-"}
                                </div>
                            </div>
                        </div>
                    )) : nullEle}
                </div>
            </div>

            {offerSkills.length > 5 && (
                <div className={detailsStyles["frame-172"]}>
                    <div
                        className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}
                        onClick={() => setExpanded((prev) => !prev)}
                    >
                        <div className={detailsStyles["find-mathing-job"]}>
                            {expanded
                                ? "Show less"
                                : `Show more (${offerSkills.length - 5})`}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}