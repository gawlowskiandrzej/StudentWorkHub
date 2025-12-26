import { language } from "@/types/list/Offer/language";

type languageSection = {
    languages: language[]
}

export function LanguageSection({ languages }: languageSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required languages</div>
                    <div className="line-10"></div>
                </div>
                <div className="skill-section-content">
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="award3" src="/icons/light/awardLight.svg" />
                            <div className="city-name">LanguageLevel</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="file-text2" src="/icons/light/file-textLight.svg" />
                            <div className="city-name">LanguageName</div>
                        </div>
                    </div>
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="award4" src="/icons/light/awardLight.svg" />
                            <div className="city-name">LanguageLevel</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="file-text3" src="/icons/light/file-textLight.svg" />
                            <div className="city-name">LanguageName</div>
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