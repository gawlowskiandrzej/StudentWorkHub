import detailsStyles from '../../../styles/OfferDetails.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'

type benefitsSection = {
    benefits: string[] | null
}
const BENEFITS_PER_ROW = 3;
const nullEle =
    <div className={detailsStyles["skill-section-row"]}>
        <div className={detailsStyles["skill-section-item1"]}>
            <img className={detailsStyles["gift"]} src="/icons/light/giftLight.svg" />
            <div className={detailsStyles["city-name"]}>-</div>
        </div>
    </div>
export function BenefitsSection({ benefits }: benefitsSection) {
    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>Benefits</div>
                    <div className={detailsStyles["line-10"]}></div>
                </div>

                <div className={`${detailsStyles["skill-section-content"]} thight-section`}>
                    {benefits ? Array.from({ length: Math.ceil(Math.min(benefits.length, 6) / BENEFITS_PER_ROW) }).map((_, rowIndex) => {
                        const start = rowIndex * BENEFITS_PER_ROW;
                        const rowItems = benefits.slice(start, start + BENEFITS_PER_ROW);
                        return (
                            <div key={rowIndex} className={detailsStyles["skill-section-row"]}>
                                {rowItems.map((benefit, idx) => (
                                    <div key={idx} className={detailsStyles[`skill-section-item${idx + 1}`]}>
                                        <img className={detailsStyles["gift"]} src="/icons/light/giftLight.svg" />
                                        <div className={detailsStyles["city-name"]}>{benefit}</div>
                                    </div>
                                ))}
                            </div>
                        );
                    }) : nullEle}
                </div>
            </div>
            {benefits ? benefits.length > 5 ?
                <div className={detailsStyles["frame-172"]}>
                    <div className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}>
                        <div className={detailsStyles["find-mathing-job"]}>Show more ({benefits.length - 6})</div>
                    </div>
                </div>
                : ""
                : ""
            }

        </div>
    );
}