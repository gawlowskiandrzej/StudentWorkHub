import sideMenuStyles from '@/styles/SideMenuStyle.module.css'

export function OptionsNavigation() {
    return (
        <div className={`${sideMenuStyles["navigation"]} `}>
                    <div className={sideMenuStyles["pofile-nav"]}>
                        <img className={sideMenuStyles["user2"]} src="/icons/user1.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Profile</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]}>
                        <img className={sideMenuStyles["user2"]} src="/icons/check-circle0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Matched for you</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]}>
                        <img className={sideMenuStyles["user2"]} src="/icons/briefcase0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]}>Application history</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]}>
                        <img className={sideMenuStyles["user2"]} src="/icons/sliders0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Settings</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]}>
                        <img className={sideMenuStyles["user2"]} src="/icons/log-in0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Logout</div>
                    </div>
                </div>

    );
}