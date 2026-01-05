import UserInfoCardStyles from '@/styles/UserInfoStyle.module.css'

export function UserInfoCard() {
    return (
        <div className={UserInfoCardStyles["user-profile-info-card"]}>
            <div className={UserInfoCardStyles["profile-element"]}>
                <div className={UserInfoCardStyles["header-profile"]}>
                    <div className={UserInfoCardStyles["category"]}>
                        Personal information
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
                            User name and Surname
                        </div>
                    </div>
                </div>
            </div>

            <div className={UserInfoCardStyles["line-8"]}></div>

            <div className={UserInfoCardStyles["job-preferences"]}>
                Job preferences
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
                            Main category
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            Category from list
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
                            Location
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            City name
                        </div>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            Work type from list [ Remotly, In office, Hybrid ]
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
                            Employment and salary
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            B2B (net/month): From 3000 to 10000
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
                            Urgent work
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            Yes
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
                            Languages
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            English, Polish, Deutsh
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
                            Your skills (search by phrase)
                        </div>
                    </div>
                    <div className={UserInfoCardStyles["profile-items-sub"]}>
                        <div className={UserInfoCardStyles["category-from-list"]}>
                            Proggraming - 24m, Helpdesk - 12m, Car driving - 12m
                        </div>
                    </div>
                </div>
            </div>
        </div>

    );

}