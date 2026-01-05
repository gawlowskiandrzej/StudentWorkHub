import OptionsNavigationStyles from '@/styles/OptionNavigation.module.css'

export function OptionsNavigation() {
    return (
        <div className={OptionsNavigationStyles["options-nav-menu"]}>
            <div className={OptionsNavigationStyles["user-profile-nav-item"]}>
                <img
                    className={OptionsNavigationStyles["user"]}
                    src="../icons/user1.svg"
                    alt="Profile"
                />
                <div className={OptionsNavigationStyles["profile"]}>Profile</div>
            </div>

            <div className={OptionsNavigationStyles["user-profile-nav-item"]}>
                <img
                    className={OptionsNavigationStyles["user"]}
                    src="../icons/check-circle0.svg"
                    alt="Matched for you"
                />
                <div className={OptionsNavigationStyles["profile"]}>Matched for you</div>
            </div>

            <div className={OptionsNavigationStyles["user-profile-nav-item"]}>
                <img
                    className={OptionsNavigationStyles["user"]}
                    src="../icons/briefcase0.svg"
                    alt="Application history"
                />
                <div className={OptionsNavigationStyles["profile"]}>
                    Application history
                </div>
            </div>

            <div className={OptionsNavigationStyles["user-profile-nav-item"]}>
                <img
                    className={OptionsNavigationStyles["user"]}
                    src="../icons/sliders0.svg"
                    alt="Settings"
                />
                <div className={OptionsNavigationStyles["profile"]}>Settings</div>
            </div>

            <div className={OptionsNavigationStyles["user-profile-nav-item"]}>
                <img
                    className={OptionsNavigationStyles["user"]}
                    src="../icons/log-in0.svg"
                    alt="Logout"
                />
                <div className={OptionsNavigationStyles["profile"]}>Logout</div>
            </div>
        </div>

    );
}