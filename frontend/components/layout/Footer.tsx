"use client";
import footerStyles from "../../styles/Footer.module.css";
import { Inter } from "next/font/google";
import { useTranslation } from "react-i18next";

const inter = Inter({
  weight: ["400", "700"],
  subsets: ["latin"],
  display: "swap",
});

const Footer = () => {
  const {t}  = useTranslation("footer");
  return (
    <footer
      className={`${footerStyles["footer"]} ${footerStyles["border-t"]} ${inter.className}`}
    >
      <div className={footerStyles["footer-content"]}>
        <div className={footerStyles["about-section"]}>
          <div className={footerStyles["about-us"]}>
            {t("aboutUsTitle")}
          </div>
          <div className={footerStyles["aboutus-desc"]}>
            {t("aboutUsSubtitle")}
          </div>
        </div>

        <div className={footerStyles["navlinks"]}>
          <div className={footerStyles["frame-2"]}>
            <div className={footerStyles["account"]}>{t("account")}</div>
            <div className={footerStyles["login"]}>{t("login")}</div>
            <div className={footerStyles["register"]}>{t("register")}</div>
            <div className={footerStyles["profile"]}>{t("profile")}</div>
          </div>

          <div className={footerStyles["frame-3"]}>
            <div className={footerStyles["offers"]}>{t("offers")}</div>
            <div className={footerStyles["matched"]}>{t("search")}</div>
            <div className={footerStyles["matched"]}>{t("matched")}</div>
            <div className={footerStyles["all-offers"]}>{t("allOffers")}</div>
          </div>

          <div className={footerStyles["frame-4"]}>
            <div className={footerStyles["links"]}>{t("links")}</div>
            <div className={footerStyles["privace-policy"]}>
              {t("privacyPolicy")}
            </div>
            <div className={footerStyles["legal-notice"]}>
              {t("legalNotice")}
            </div>
            <div className={footerStyles["project-information"]}>
              {t("projectInformation")}
            </div>
          </div>
        </div>

        <div className={footerStyles["socials"]}>
          <div className={footerStyles["follow-the-project"]}>
            {t("followProject")}
          </div>
          <div className={footerStyles["community-icons"]}>
            <img className={footerStyles["github"]} src="/icons/github0.svg" />
            <img className={footerStyles["facebook"]} src="/icons/facebook0.svg" />
            <img className={footerStyles["at-sign"]} src="/icons/at-sign0.svg" />
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
