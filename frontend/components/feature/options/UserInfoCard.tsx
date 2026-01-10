"use client";

import { useEffect, useRef } from 'react';
import UserInfoCardStyles from '@/styles/UserInfoStyle.module.css'
import { useUser } from '@/store/userContext';
import { useProfileCreationDictionaries } from '@/hooks/useDictionaries';
import { useTranslation } from "react-i18next";

export function UserInfoCard() {

    const { t } = useTranslation("userInfoCard");
    const { userData, preferences, fetchPreferences, fetchUserData, isAuthenticated } = useUser();
    const { fullDictionaries, loading } = useProfileCreationDictionaries();
    const hasFetchedRef = useRef(false);

    useEffect(() => {
    if (isAuthenticated && !userData) {
        fetchUserData();
    }
    }, [isAuthenticated, userData, fetchUserData]);

    if (loading || !fullDictionaries) {
        return <div className={UserInfoCardStyles["user-profile-info-card"]}>{t("loading") || "Loading..."}</div>;
    }

    // get labels from dictionaries
    const getCategoryLabel = (categoryId: number | undefined) => {
        if (!categoryId || !fullDictionaries) return t("notSpecified");
        return fullDictionaries.leading_categories.find(c => c.id === categoryId)?.name || t("notSpecified");
    };

    const getWorkplaceTypeLabel = (types: string[] | undefined) => {
        if (!types || types.length === 0) return t("notSpecified");
        return types.map(t => t).join(", ");
    };

    const getLanguageLabel = (languages: any[] | undefined) => {
        if (!languages || languages.length === 0) return t("notSpecified");
        return languages.map((lang: any) => {
            const langName = fullDictionaries.languages.find(l => l.id === lang.language_id)?.name;
            const levelName = fullDictionaries.language_levels.find(lv => lv.id === lang.language_level_id)?.name;
            return `${langName}${levelName ? ` (${levelName})` : ''}`;
        }).join(", ");
    };

    return (
        <div className={UserInfoCardStyles["user-profile-info-card"]}>
            <div className={UserInfoCardStyles["profile-element"]}>
                <div className={UserInfoCardStyles["header-profile"]}>
                    <div className={UserInfoCardStyles["category"]}>
                        {t("personalInfo")}
                    </div>
                    <div className={UserInfoCardStyles["edit-icon-container"]}>
                        <img
                            className={UserInfoCardStyles["tag"]}
                            src="../../icons/edit-pen.svg"
                        />
                    </div>

                </div>

            <div className={UserInfoCardStyles["username-sec"]}>
                    <div className={UserInfoCardStyles["user-icon"]}>
                        <img
                            className={UserInfoCardStyles["user2"]}
                            src="../../icons/userWhite.svg"
                        />
                    </div>
                    <div className={UserInfoCardStyles["username-label"]}>
                        <div className={UserInfoCardStyles["user-name-and-surname"]}>
                            {userData?.first_name} {userData?.last_name}
                        </div>
                    </div>
                </div>
            </div>

            <div className={UserInfoCardStyles["line-8"]}></div>

            <div className={UserInfoCardStyles["job-preferences"]}>
                {t("jobPreferences")}
            </div>

            <div className={UserInfoCardStyles["user-profile-settings-elements"]}>
                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={`${UserInfoCardStyles["user2"]} ml-1`}
                                    src="../../icons/tag0.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("preferredCategory")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {getCategoryLabel(preferences?.leading_category_id)}
                        </div>
                    </div>
                </div>

                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={UserInfoCardStyles["user2"]}
                                    src="../../icons/map-pinWhite.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("preferredLocationAndWorkType")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {preferences?.city_name || t("notSpecified")}
                        </div>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {getWorkplaceTypeLabel(preferences?.work_types)}
                        </div>
                    </div>
                </div>

                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={UserInfoCardStyles["user2"]}
                                    src="../../icons/dollar-signWhite.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("employmentAndSalary")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {preferences?.salary_from && preferences?.salary_to
                                ? `${preferences.salary_from} - ${preferences.salary_to} zł / ${t("month")}`
                                : t("notSpecified")}
                        </div>
                    </div>
                </div>

                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={UserInfoCardStyles["user2"]}
                                    src="../../icons/alert-circle0.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("urgentWork")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {preferences?.job_status === "actively_looking" ? t("yes") : t("no")}
                        </div>
                    </div>
                </div>

                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={UserInfoCardStyles["user2"]}
                                    src="../../icons/flagWhite.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("languages")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {getLanguageLabel(preferences?.languages)}
                        </div>
                    </div>
                </div>

                <div className={UserInfoCardStyles["profile-element"]}>
                    <div className={UserInfoCardStyles["profile-user-item"]}>
                        <div className={UserInfoCardStyles["edit-icon-container"]}>
                            <img
                                className={UserInfoCardStyles["tag"]}
                                src="../../icons/edit-pen.svg"
                            />
                        </div>
                        <div className={UserInfoCardStyles["preety-icon-container"]}>
                            <div className={UserInfoCardStyles["user-icon"]}>
                                <img
                                    className={UserInfoCardStyles["user2"]}
                                    src="../../icons/check-circle0.svg"
                                />
                            </div>
                        </div>
                        <div className={UserInfoCardStyles["category"]}>
                            {t("yourSkillsSearchByPhrase")}
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            {preferences?.skills && preferences.skills.length > 0
                                ? preferences.skills.map((skill: any) => `${skill.skill_name} - ${skill.experience_months} ${t("monthsShort")}`).join(", ")
                                : t("notSpecified")}
                        </div>
                    </div>
                </div>
            </div>
        </div>

    );

}