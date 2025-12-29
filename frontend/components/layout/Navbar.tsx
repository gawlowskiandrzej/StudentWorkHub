"use client";
import { useRouter } from "next/navigation";
import navigationStyles from "../../styles/Navigation.module.css";
import buttonStyles from "../../styles/ButtonStyle.module.css";
import { Jura } from "next/font/google";

const jura = Jura({
  weight: ["400", "700"],
  subsets: ["latin"],
  display: "swap",
});

const Navbar = () => {
  const router = useRouter();

  return (
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
            your carrer starts now !
          </div>
        </div>
      </div>

      <div className={navigationStyles["signin-frame"]}>
        <div
          className={`${buttonStyles["main-button"]} ${navigationStyles["register-button"]}`}
        >
          <div className={"text-xs"}>Register</div>
        </div>
        <img
          className={navigationStyles["user"]}
          src="/icons/user0.svg"
        />
      </div>
    </nav>
  );
};

export default Navbar;
