import { skill } from "@/types/list/Offer/skill";
import detailsStyles from '../../../styles/OfferDetails.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'

type skillSection = {
    offerSkills: skill[]
}

export function SkillsSection({ offerSkills }: skillSection) {
    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>Required skills</div>
                    <div className={detailsStyles["line-10"]} />
                </div>
                <div className={`${detailsStyles["skill-section-content"]} ${detailsStyles["thighter-section"]}`}>
                    {offerSkills
                        .sort((a, b) => {
                            if (a.experienceMonths != b.experienceMonths) {
                                return (b.experienceMonths ?? 0) - (a.experienceMonths ?? 0);
                            }
                            const levelA = (a.experienceLevel ?? []).sort().join(", ");
                            const levelB = (b.experienceLevel ?? []).sort().join(", ");
                            return levelB.localeCompare(levelA);
                        })
                        .slice(0, Math.min(5, offerSkills.length))
                        .map((offer, index) => (
                            <div key={index} className={detailsStyles["skill-section-row"]}>
                                <div className={detailsStyles["skill-section-item1"]}>
                                    <img className={detailsStyles["check-circle"]} src="/icons/light/check-circleLight.svg" />
                                    <div className={detailsStyles["city-name"]}>{offer.skill ?? "-"}</div>
                                </div>
                                {
                                    <div className={detailsStyles["skill-section-item2"]}>
                                        <img className={detailsStyles["pie-chart2"]} src="/icons/light/clockLight.svg" />
                                        <div className={detailsStyles["city-name"]}>{offer.experienceMonths ? `${offer.experienceMonths / 12} lata` : "-"}</div>
                                    </div>
                                }
                                {
                                    <div className={detailsStyles["skill-section-item3"]}>
                                        <img className={detailsStyles["award"]} src="/icons/light/awardLight.svg" />
                                        <div className={detailsStyles["city-name"]}>
                                            {offer.experienceLevel?.length
                                                ? offer.experienceLevel.join(' / ')
                                                : "-"}
                                        </div>
                                    </div>
                                }
                            </div>
                        ))}
                </div>
            </div>
            {offerSkills.length > 5 ?
                <div className={detailsStyles["frame-172"]}>
                    <div className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}>
                        <div className={detailsStyles["find-mathing-job"]}>Show more ({offerSkills.length - 4})</div>
                    </div>
                </div>
                :
                ""
            }
        </div>
    );
}