import footerStyles from "../../styles/Footer.module.css";
import { Inter } from "next/font/google";

const inter = Inter({
  weight: ["400", "700"],
  subsets: ["latin"],
  display: "swap",
});

const Footer = () => {
  return (
    <footer className={`${footerStyles["footer"]} ${footerStyles["border-t"]} ${inter.className}`}>
      <div className={footerStyles["footer-content"]}>
        <div className={footerStyles["about-section"]}>
          <div className={footerStyles["about-us"]}>About us</div>
          <div className={footerStyles["aboutus-desc"]}>
            StudentWorkHub is an engineering project created by three people who
            <br />
            want to find a job quickly and easily, based on their profession and hobbies.
          </div>
        </div>

        <div className={footerStyles["navlinks"]}>
          <div className={footerStyles["frame-2"]}>
            <div className={footerStyles["account"]}>Account</div>
            <div className={footerStyles["login"]}>Login</div>
            <div className={footerStyles["register"]}>Register</div>
            <div className={footerStyles["profile"]}>Profile</div>
          </div>

          <div className={footerStyles["frame-3"]}>
            <div className={footerStyles["offers"]}>Offers</div>
            <div className={footerStyles["matched"]}>Search</div>
            <div className={footerStyles["matched"]}>Matched</div>
            <div className={footerStyles["all-offers"]}>All offers</div>
          </div>

          <div className={footerStyles["frame-4"]}>
            <div className={footerStyles["links"]}>Links</div>
            <div className={footerStyles["privace-policy"]}>Privace Policy</div>
            <div className={footerStyles["legal-notice"]}>Legal Notice</div>
            <div className={footerStyles["project-information"]}>Project information</div>
          </div>
        </div>

        <div className={footerStyles["socials"]}>
          <div className={footerStyles["follow-the-project"]}>Follow the project</div>
          <div className={footerStyles["community-icons"]}>
            <img className={footerStyles["github"]} src="/icons/github0.svg" />
            <img className={footerStyles["facebook"]} src="/icons/facebook0.svg" />
            <img className={footerStyles["at-sign"]} src="/icons/at-sign0.svg" />
          </div>
        </div>
      </div>

      <div className={footerStyles["copyright"]}>
        <div className={footerStyles["student-work-hub"]}>StudentWorkHub</div>
        <div className={footerStyles["copy-right-text"]}>Copyright 2025. All rights reserved.</div>
      </div>
    </footer>
  );
};

export default Footer;
