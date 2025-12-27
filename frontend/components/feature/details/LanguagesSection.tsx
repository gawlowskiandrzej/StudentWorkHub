import { language } from "@/types/list/Offer/language";

type languageSection = {
    languages: language[]
}
const nullEle =
    <div className="skill-section-row">
        <div className="skill-section-item1">
            <img className="file-text2" src="/icons/light/file-textLight.svg" />
            <div className="city-name">-</div>
        </div>
    </div>
export function LanguageSection({ languages }: languageSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required languages</div>
                    <div className="line-10"></div>
                </div>
                <div className="skill-section-content thighter-section">
                    {languages.length > 0 ? languages.slice(0, Math.min(5, languages.length)).map((language, index) => (
                        <div key={index} className="skill-section-row">
                            <div className="skill-section-item1">
                                <img className="file-text2" src="/icons/light/file-textLight.svg" />
                                <div className="city-name">{language.language ?? "-"}</div>
                            </div>
                            <div className="skill-section-item2">
                                <img className="award3" src="/icons/light/awardLight.svg" />
                                <div className="city-name">{language.level ?? "-"}</div>
                            </div>
                        </div>
                    )) : nullEle}
                </div>
            </div>
            {languages.length > 5 ?
                <div className="frame-172">
                    <div className="main-button">
                        <div className="find-mathing-job">Show more (20)</div>
                    </div>
                </div>
                :
                ""
            }
        </div>
    );
}