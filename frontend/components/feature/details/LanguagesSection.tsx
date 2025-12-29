import { language } from "@/types/list/Offer/language";
import detailsStyles from "../../../styles/OfferDetails.module.css"
import buttonStyles from "../../../styles/ButtonStyle.module.css"

type languageSection = {
    languages: language[]
}
const nullEle =
    <div className={detailsStyles["skill-section-row"]}>
        <div className={detailsStyles["skill-section-item1"]}>
            <img className={detailsStyles["file-text2"]} src="/icons/light/file-textLight.svg" />
            <div className={detailsStyles["city-name"]}>-</div>
        </div>
    </div>
export function LanguageSection({ languages }: languageSection) {
    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>Required languages</div>
                    <div className={detailsStyles["line-10"]}></div>
                </div>
                <div className={`${detailsStyles["skill-section-content"]} thighter-section`}>
                    {languages.length > 0 ? languages.slice(0, Math.min(5, languages.length)).map((language, index) => (
                        <div key={index} className={detailsStyles["skill-section-row"]}>
                            <div className={detailsStyles["skill-section-item1"]}>
                                <img className={detailsStyles["file-text2"]} src="/icons/light/file-textLight.svg" />
                                <div className={detailsStyles["city-name"]}>{language.language ?? "-"}</div>
                            </div>
                            <div className={detailsStyles["skill-section-item2"]}>
                                <img className={detailsStyles["award3"]} src="/icons/light/awardLight.svg" />
                                <div className={detailsStyles["city-name"]}>{language.level ?? "-"}</div>
                            </div>
                        </div>
                    )) : nullEle}
                </div>
            </div>
            {languages.length > 5 ?
                <div className={detailsStyles["frame-172"]}>
                    <div className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}>
                        <div className={detailsStyles["find-mathing-job"]}>Show more (20)</div>
                    </div>
                </div>
                :
                ""
            }
        </div>
    );
}