"use client";
import { useRouter } from "next/navigation";
import { useState } from "react";
import navigationStyles from "../../styles/Navigation.module.css";
import buttonStyles from "../../styles/ButtonStyle.module.css";
import { Jura } from "next/font/google";
import { useTranslation } from "react-i18next";
import { Menu, X } from "lucide-react";
import { Sidemenu } from "../feature/other/Sidemenu";

const jura = Jura({
  weight: ["400", "700"],
  subsets: ["latin"],
  display: "swap",
});

const Navbar = () => {
  const router = useRouter();
  const { t } = useTranslation(["navigation", "common"]);
  const [menuOpen, setMenuOpen] = useState(false);

  return (
    <>
      <nav className={navigationStyles["navigation-with-short-logo"]}>
        <div
          className={`${navigationStyles["logo"]} cursor-pointer`}
          onClick={() => router.push("/")}
        >
          <img
            className={navigationStyles["student-work-hub-logo-1"]}
            src="/images/all/student-work-hub-logo-10.png"
          />
          <div className={`${navigationStyles["logo-text"]} ${jura.className}`}>
            <div className={navigationStyles["student-work-hub"]}>
              StudentWorkHub
            </div>
            <div className={navigationStyles["subtitle-student-work-hub"]}>
              {t("common:siteSubHeader")}
            </div>
          </div>
        </div>

        <div className={navigationStyles["signin-frame"]}>
          <div onClick={() => {router.push("/login")}}
            className={`${buttonStyles["main-button"]} ${navigationStyles["register-button"]}`}
          >
            <div className="text-xs">{t("navigation:login")}</div>
          </div>
          <div onClick={() => {router.push("/register")}}
            className={`${buttonStyles["main-button"]} ${navigationStyles["register-button"]}`}
          >
            <div className="text-xs">{t("navigation:register")}</div>
          </div>

          <img className={navigationStyles["user"]} src="/icons/user0.svg" />

          <Menu
            className="cursor-pointer"
            onClick={() => setMenuOpen(true)}
          />
        </div>
      </nav>

      {/* Overlay */}
      {menuOpen && (
        <div
          className={navigationStyles.overlay}
          onClick={() => setMenuOpen(false)}
        />
      )}

      {/* Slide menu */}
      <Sidemenu menuOpen={menuOpen} router={router} setMenuOpen={setMenuOpen}/>
    </>
  );
};

export default Navbar;
