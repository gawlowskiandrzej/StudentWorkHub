import { AppRouterInstance } from "next/dist/shared/lib/app-router-context.shared-runtime";
import { SetStateAction } from "react";
import navigationStyles from "../../../styles/Navigation.module.css";
import sideMenuStyles from "../../../styles/SideMenuStyle.module.css";
export type sideMenuProps = {
    router: AppRouterInstance,
    menuOpen: boolean,
    setMenuOpen: (value: SetStateAction<boolean>) => void
}

export function Sidemenu({ router, menuOpen, setMenuOpen }: sideMenuProps) {
    const openOptions = () => {
        router.push("/options");
        setMenuOpen(false);
    }
    return (
        <div
            className={`${navigationStyles.sideMenu} ${menuOpen ? navigationStyles.open : ""
                }`}
        ><div className={sideMenuStyles["side-menu"]}>
                <div className={sideMenuStyles["user-nav-sec"]}>
                    <div className={sideMenuStyles["close-nav"]}>
                        <div className={`${sideMenuStyles["frame-149"]} cursor-pointer`} onClick={() => setMenuOpen(false)}>
                            <img className={sideMenuStyles["x"]} src="/icons/x0.svg" />
                            <div className={sideMenuStyles["menu"]}>
                                    Menu
                            </div>
                        </div>
                    </div>
                    <div className={sideMenuStyles["profile-icon"]}>
                        <img className={sideMenuStyles["user"]} src="/icons/user1.svg" />
                    </div>
                    <div className={sideMenuStyles["user-name-sur"]}>User name user surname</div>
                </div>
                <div className={sideMenuStyles["navigation"]}>
                    <div className={sideMenuStyles["pofile-nav"]} onClick={openOptions}>
                        <img className={sideMenuStyles["user2"]} src="/icons/user1.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Profile</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]} onClick={openOptions}>
                        <img className={sideMenuStyles["user2"]} src="/icons/check-circle0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Matched for you</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]} onClick={openOptions}>
                        <img className={sideMenuStyles["user2"]} src="/icons/briefcase0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]}>Application history</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]} onClick={openOptions}>
                        <img className={sideMenuStyles["user2"]} src="/icons/sliders0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Settings</div>
                    </div>
                    <div className={sideMenuStyles["pofile-nav"]} onClick={openOptions}>
                        <img className={sideMenuStyles["user2"]} src="/icons/log-in0.svg" />
                        <div className={sideMenuStyles["side-menu-item"]} >Logout</div>
                    </div>
                </div>
            </div>
        </div>
    );
}