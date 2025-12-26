type benefitsSection = {
    benefits: string[] | null
}

export function BenefitsSection({ benefits }: benefitsSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Benefits</div>
                    <div className="line-10"></div>
                </div>
                <div className="skill-section-content">
                    <div className="skill-section-row">
                        <div className="skill-section-item">
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
                    </div>
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="gift4" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
                        </div>
                        <div className="skill-section-item2">
                            <img className="gift5" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="gift6" src="/icons/light/giftLight.svg" />
                            <div className="city-name">Benefitname</div>
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