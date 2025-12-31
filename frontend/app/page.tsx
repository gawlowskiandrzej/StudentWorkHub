"use client";

import { useTranslation } from "react-i18next";
import buttonStyles from "../styles/ButtonStyle.module.css"
import heroStyles from '../styles/Hero.module.css';

import { useRouter } from 'next/navigation';

export default function Home() {
  const router = useRouter();
  const {t} = useTranslation(["hero", "common"])
  const goToSearchPage = () => {
    router.push('/search');
  };
  return (
  <div className={heroStyles["hero-page"]}>
  <div className={heroStyles["section-1"]}>
    <div className={heroStyles["main-content-section-1"]}>
      <div className={heroStyles["main-content-frame"]}>
        <div className={heroStyles["main-text-frame"]}>
          <div className={heroStyles["header-sec-1"]}>
            {t("heroTitle")}
          </div>
          <div className={heroStyles["sub-header-sec-1"]}>
            {t("heroSubtitle1")}
            <br />
            {t("heroSubtitle2")}
          </div>
        </div>
        <button
          className={
            buttonStyles["main-button"] +
            " " +
            buttonStyles["find-mathing-job"] +
            " " +
            heroStyles["button-left"]
          }
          onClick={goToSearchPage}
        >
          {t("common:findMatchingJob")}
        </button>
      </div>
      <img
        className={heroStyles["unsplash-bb-s-bf-5-uv-50-a"]}
        src="/images/hero/unsplash-bb-s-bf-5-uv-50-a0.png"
      />
    </div>
  </div>

  <div className={heroStyles["section-2"]}>
    <div className={heroStyles["home-trust-section"]}>
      <div className={heroStyles["main-header-sec-2"]}>
        {t("hero:featureSectionTitle")}
      </div>
      <div className={heroStyles["main-content-sec-2"]}>
        <img className={heroStyles.wave} src="/icons/wave0.svg" />
        <div className={heroStyles["feature-section-list"]}>
          <div className={heroStyles["feature-element-item"]}>
            <img
              className={heroStyles["unsplash-376-kn-i-spl-e"]}
              src="/images/hero/unsplash-376-kn-i-spl-e0.png"
            />
            <div className={heroStyles["feature-element-text"]}>
              <div className={heroStyles["feature-1"]}>
                {t("hero:featureSectionItem1Title")}
              </div>
              <div className={heroStyles["feature-desc"]}>
                {t("hero:featureSectionItem1Subtile")}
              </div>
            </div>
          </div>

          <div className={heroStyles["feature-element-item"]}>
            <img
              className={heroStyles["unsplash-376-kn-i-spl-e"]}
              src="/images/hero/unsplash-376-kn-i-spl-e1.png"
            />
            <div className={heroStyles["feature-element-text"]}>
              <div className={heroStyles["feature-1"]}> {t("hero:featureSectionItem2Title")}</div>
              <div className={heroStyles["the-feature-or-any-details"]}>
                 {t("hero:featureSectionItem2Subtile")}
              </div>
            </div>
          </div>

          <div className={heroStyles["feature-element-item"]}>
            <img
              className={heroStyles["unsplash-376-kn-i-spl-e"]}
              src="/images/hero/unsplash-376-kn-i-spl-e2.png"
            />
            <div className={heroStyles["feature-element-text"]}>
              <div className={heroStyles["feature-1"]}> {t("hero:featureSectionItem3Title")}</div>
              <div className={heroStyles["the-feature-or-any-details"]}>
                 {t("hero:featureSectionItem3Subtile")}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div className={heroStyles["section-3"]}>
    <div className={heroStyles["main-header-sec-3"]}>
      <span>
        <span className={heroStyles["main-header-sec-3-span"]}>{t("hero:howWorksSectionTitlepart1")} </span>
        <span className={heroStyles["main-header-sec-3-span2"]}>
          StudentWorkHub
        </span>
        <span className={heroStyles["main-header-sec-3-span"]}> {t("hero:howWorksSectionTitlepart2")}</span>
      </span>
    </div>

    <div className={heroStyles["how-works-section-items"]}>
      <div className={heroStyles["works-feature"]}>
        <div className={heroStyles["works-feature-icon"]}>
          <div className={heroStyles["search-icon"]}>
            <img className={heroStyles.vector2} src="/icons/search0.svg" />
          </div>
        </div>
        <div className={heroStyles["work-feature-text"]}>
          <div className={heroStyles["feature-12"]}>{t("hero:howWorksSectionItem1Title")}</div>
          <div className={heroStyles["description-of-feature-1"]}>
            {t("hero:howWorksSectionItem1Subtitle")}
          </div>
        </div>
      </div>

      <div className={heroStyles["works-feature"]}>
        <div className={heroStyles["works-feature-icon"]}>
          <div className={heroStyles["user-icon"]}>
            <img className={heroStyles.vector2} src="/icons/user0.svg" />
          </div>
        </div>
        <div className={heroStyles["work-feature-text"]}>
          <div className={heroStyles["feature-12"]}>
            {t("hero:howWorksSectionItem2Title")}
          </div>
          <div className={heroStyles["description-of-feature-1"]}>
            {t("hero:howWorksSectionItem2Subtitle")}
          </div>
        </div>
      </div>

      <div className={heroStyles["works-feature"]}>
        <div className={heroStyles["works-feature-icon"]}>
          <img
            className={heroStyles["robot-icon"]}
            src="/icons/robot-icon0.svg"
          />
        </div>
        <div className={heroStyles["work-feature-text"]}>
          <div className={heroStyles["feature-12"]}>
            {t("hero:howWorksSectionItem3Title")}
          </div>
          <div className={heroStyles["description-of-feature-1"]}>
            {t("hero:howWorksSectionItem3Subtitle")}
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

  );
}
