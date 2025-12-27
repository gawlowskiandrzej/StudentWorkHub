type benefitsSection = {
    benefits: string[] | null
}
const BENEFITS_PER_ROW = 3;
const nullEle =
    <div className="skill-section-row">
        <div className="skill-section-item1">
            <img className="gift" src="/icons/light/giftLight.svg" />
            <div className="city-name">-</div>
        </div>
    </div>
export function BenefitsSection({ benefits }: benefitsSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Benefits</div>
                    <div className="line-10"></div>
                </div>

                <div className="skill-section-content thight-section">
                    {benefits ? Array.from({ length: Math.ceil(Math.min(benefits.length, 6) / BENEFITS_PER_ROW) }).map((_, rowIndex) => {
                        const start = rowIndex * BENEFITS_PER_ROW;
                        const rowItems = benefits.slice(start, start + BENEFITS_PER_ROW);
                        return (
                            <div key={rowIndex} className="skill-section-row">
                                {rowItems.map((benefit, idx) => (
                                    <div key={idx} className={`skill-section-item${idx + 1}`}>
                                        <img className="gift" src="/icons/light/giftLight.svg" />
                                        <div className="city-name">{benefit}</div>
                                    </div>
                                ))}
                            </div>
                        );
                    }) : nullEle}
                    {/* <div className="skill-section-row">
                        <div className="skill-section-item1">
                            <img className="gift" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
                        </div>
                        <div className="skill-section-item2">
                            <img className="gift2" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="gift3" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
                        </div>
                    </div> */}
                </div>
            </div>
            {benefits ? benefits.length > 5 ?
                <div className="frame-172">
                    <div className="main-button">
                        <div className="find-mathing-job">Show more ({benefits.length - 6})</div>
                    </div>
                </div>
                : ""
                : ""
            }

        </div>
    );
}