import { useState } from "react";
import { language } from "@/types/list/Offer/language";
import detailsStyles from "../../../styles/OfferDetails.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";

type languageSection = {
    languages: language[];
};

const INITIAL_VISIBLE = 6;

const nullEle = (
    <div className={detailsStyles["skill-section-row"]}>
        <div className={detailsStyles["skill-section-item1"]}>
            <img
                className={detailsStyles["file-text2"]}
                src="/icons/light/file-textLight.svg"
            />
            <div className={detailsStyles["city-name"]}>-</div>
        </div>
    </div>
);

export function LanguageSection({ languages }: languageSection) {
    const [expanded, setExpanded] = useState(false);

    const visibleLanguages = expanded
        ? languages
        : languages.slice(0, INITIAL_VISIBLE);

    return (
        <div className={detailsStyles["required-skills-section"]}>
            <div className={detailsStyles["frame-168"]}>
                <div className={detailsStyles["frame-166"]}>
                    <div className={detailsStyles["required-skills"]}>
                        Required languages
                    </div>
                    <div className={detailsStyles["line-10"]}></div>
                </div>

                <div className={`${detailsStyles["skill-section-content"]} thighter-section`}>
                    {visibleLanguages.length > 0 ? visibleLanguages.map((lang, index) => (
                        <div key={index} className={detailsStyles["skill-section-row"]}>
                            <div className={detailsStyles["skill-section-item1"]}>
                                <img
                                    className={detailsStyles["file-text2"]}
                                    src="/icons/light/file-textLight.svg"
                                />
                                <div className={detailsStyles["city-name"]}>
                                    {lang.language ?? "-"}
                                </div>
                            </div>
                            <div className={detailsStyles["skill-section-item2"]}>
                                <img
                                    className={detailsStyles["award3"]}
                                    src="/icons/light/awardLight.svg"
                                />
                                <div className={detailsStyles["city-name"]}>
                                    {lang.level ?? "-"}
                                </div>
                            </div>
                        </div>
                    )): nullEle}
                </div>
            </div>

            {languages.length > INITIAL_VISIBLE && (
                <div className={detailsStyles["frame-172"]}>
                    <div
                        className={`${buttonStyles["main-button"]} ${detailsStyles["show-more-button"]}`}
                        onClick={() => setExpanded((prev) => !prev)}
                    >
                        <div className={detailsStyles["find-mathing-job"]}>
                            {expanded
                                ? "Show less"
                                : `Show more (${languages.length - INITIAL_VISIBLE})`}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
