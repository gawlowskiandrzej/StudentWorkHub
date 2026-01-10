"use client";

import { AppRouterInstance } from "next/dist/shared/lib/app-router-context.shared-runtime";
import { SetStateAction, useEffect, useRef } from "react";
import navigationStyles from "../../../styles/Navigation.module.css";
import sideMenuStyles from "../../../styles/SideMenuStyle.module.css";
import { useUser } from "@/store/userContext";
import { Skeleton } from "@/components/ui/skeleton";
import { CARD_TYPES, CardType } from "@/types/options/cardTypes";
import { useTranslation } from "react-i18next";

export type sideMenuProps = {
  router: AppRouterInstance;
  menuOpen: boolean;
  setMenuOpen: (value: SetStateAction<boolean>) => void;
};

export function Sidemenu({ router, menuOpen, setMenuOpen }: sideMenuProps) {
  const { userData, loading,fetchUserData, logout, isAuthenticated } = useUser();
  const {t} = useTranslation("navigation");
  const hasFetchedRef = useRef(false);

  
  useEffect(() => {
          if (isAuthenticated && !hasFetchedRef.current) {
              hasFetchedRef.current = true;
              if (!userData) {
                  fetchUserData();
              }
          }
      }, [isAuthenticated, userData, fetchUserData]);
  const openOptions = (card: CardType) => {
    if (!isAuthenticated) {
    router.push("/login");
  } else {
    router.push(`/options?card=${card}`);
  }
    setMenuOpen(false);
  };
  const handleLogout = async () => {
    setMenuOpen(false);

    await logout();
    router.push("/");
  };

  return (
    <div
      className={`${navigationStyles.sideMenu} ${
        menuOpen ? navigationStyles.open : ""
      }`}
    >
      <div className={sideMenuStyles["side-menu"]}>
        <div className={sideMenuStyles["user-nav-sec"]}>
          <div className={sideMenuStyles["close-nav"]}>
            <div
              className={`${sideMenuStyles["frame-149"]} cursor-pointer`}
              onClick={() => setMenuOpen(false)}
            >
              <img className={sideMenuStyles["x"]} src="/icons/x0.svg" />
              <div className={sideMenuStyles["menu"]}>Menu</div>
            </div>
          </div>

          <div className={sideMenuStyles["profile-icon"]}>
            <img className={sideMenuStyles["user"]} src="/icons/user1.svg" />
          </div>

          <div className={sideMenuStyles["user-name-sur"]}>
            {isAuthenticated ? <>{`${userData?.first_name} ${userData?.last_name}`}</> : "Nie jesteś zalogowany"}
          </div>
        </div>

        <div className={sideMenuStyles["navigation"]}>
          <div className={sideMenuStyles["pofile-nav"]} onClick={() => {openOptions(CARD_TYPES[0])}}>
            <img className={sideMenuStyles["user2"]} src="/icons/user1.svg" />
            <div className={sideMenuStyles["side-menu-item"]}>{t("userInfo")}</div>
          </div>

          <div className={sideMenuStyles["pofile-nav"]} onClick={() => {openOptions(CARD_TYPES[1])}}>
            <img
              className={sideMenuStyles["user2"]}
              src="/icons/check-circle0.svg"
            />
            <div className={sideMenuStyles["side-menu-item"]}>{t("matchedForYou")}</div>
          </div>

          <div className={sideMenuStyles["pofile-nav"]} onClick={() => {openOptions(CARD_TYPES[2])}}>
            <img
              className={sideMenuStyles["user2"]}
              src="/icons/briefcase0.svg"
            />
            <div className={sideMenuStyles["side-menu-item"]}>
              {t("applicationHistory")}
            </div>
          </div>

          <div className={sideMenuStyles["pofile-nav"]} onClick={() => {openOptions(CARD_TYPES[3])}}>
            <img className={sideMenuStyles["user2"]} src="/icons/sliders0.svg" />
            <div className={sideMenuStyles["side-menu-item"]}>{t("settings")}</div>
          </div>

          <div
            className={sideMenuStyles["pofile-nav"]}
            onClick={loading ? undefined : handleLogout}
            aria-disabled={loading}
          >
            <img className={sideMenuStyles["user2"]} src="/icons/log-in0.svg" />
            <div className={sideMenuStyles["side-menu-item"]}>
              {loading ? t("loggingOut") : t("logout")}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
