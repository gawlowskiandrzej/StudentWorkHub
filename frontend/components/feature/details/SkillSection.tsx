import { skill } from "@/types/list/Offer/skill";

type skillSection = {
    offerSkills: skill[]
}

export function SkillsSection({ offerSkills }: skillSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required skills</div>
                    <div className="line-10" />
                </div>
                <div className="skill-section-content thighter-section">
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
                            <div key={index} className="skill-section-row ">
                                <div className="skill-section-item1">
                                    <img className="check-circle" src="/icons/light/check-circleLight.svg" />
                                    <div className="city-name">{offer.skill ?? "-"}</div>
                                </div>
                                {
                                    <div className="skill-section-item2">
                                        <img className="pie-chart2" src="/icons/light/clockLight.svg" />
                                        <div className="city-name">{offer.experienceMonths ? `${offer.experienceMonths / 12} lata` : "-"}</div>
                                    </div>
                                }
                                {
                                    <div className="skill-section-item3">
                                        <img className="award" src="/icons/light/awardLight.svg" />
                                        <div className="city-name">
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
                <div className="frame-172">
                    <div className="main-button">
                        <div className="find-mathing-job">Show more ({offerSkills.length - 4})</div>
                    </div>
                </div>
                :
                ""
            }
        </div>
    );
}