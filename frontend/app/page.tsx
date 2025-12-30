"use client";

import { useT } from "@/i18n/i18nWrapper";
import buttonStyles from "../styles/ButtonStyle.module.css"
import heroStyles from '../styles/Hero.module.css';

import { useRouter } from 'next/navigation';

export default function Home() {
  const router = useRouter();
  const t = useT("hero");
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
            Find job offers that match your
            preferences.
          </div>
          <div className={heroStyles["sub-header-sec-1"]}>
            Finding the best suited job has never been easier!
            <br />
            Want to give it a try?
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
          Find matching job
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
        Features and Benefits
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
                Find your job faster
              </div>
              <div className={heroStyles["feature-desc"]}>
                Precise search based on your interests, skills and abilities
                allows us to select only the most suitable offers for you
              </div>
            </div>
          </div>

          <div className={heroStyles["feature-element-item"]}>
            <img
              className={heroStyles["unsplash-376-kn-i-spl-e"]}
              src="/images/hero/unsplash-376-kn-i-spl-e1.png"
            />
            <div className={heroStyles["feature-element-text"]}>
              <div className={heroStyles["feature-1"]}>Make it simple</div>
              <div className={heroStyles["the-feature-or-any-details"]}>
                We have tried to simplify the search process so that it is
                straightforward, intuitive, and leads you directly to the
                specific location.
              </div>
            </div>
          </div>

          <div className={heroStyles["feature-element-item"]}>
            <img
              className={heroStyles["unsplash-376-kn-i-spl-e"]}
              src="/images/hero/unsplash-376-kn-i-spl-e2.png"
            />
            <div className={heroStyles["feature-element-text"]}>
              <div className={heroStyles["feature-1"]}>Be successful</div>
              <div className={heroStyles["the-feature-or-any-details"]}>
                We search for offers from the largest portals, which
                significantly increases your chances of finding a job.
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
        <span className={heroStyles["main-header-sec-3-span"]}>How </span>
        <span className={heroStyles["main-header-sec-3-span2"]}>
          StudentWorkHub
        </span>
        <span className={heroStyles["main-header-sec-3-span"]}> works?</span>
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
          <div className={heroStyles["feature-12"]}>Browse job offers</div>
          <div className={heroStyles["description-of-feature-1"]}>
            With StudentWorkHub, you can browse offers from the largest job
            portals.
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
            Create a free account
          </div>
          <div className={heroStyles["description-of-feature-1"]}>
            After creating an account, you will gain access to all the
            app&#039;s features like search and ranking.
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
            Using AI and algorithms
          </div>
          <div className={heroStyles["description-of-feature-1"]}>
            Artificial intelligence and ranking algorithms will allow you to
            find only the most accurate offers.
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

  );
}
