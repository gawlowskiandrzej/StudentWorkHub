"use client";

import Image from "next/image";
import "../styles/Hero.css";
import { useRouter } from 'next/navigation';

export default function Home() {
  const router = useRouter();

  const goToSearchPage = () => {
    router.push('/search');
  };
  return (
   <div className="hero-page">
  <div className="section-1">
    <div className="main-content-section-1">
      <div className="main-content-frame">
        <div className="main-text-frame">
          <div className="header-sec-1">
            Find job offers that match your
            preferences.
          </div>
          <div className="sub-header-sec-1">
            Finding the best suited job has never been easier!
            <br />
            Want to give it a try?
          </div>
        </div>
          <button className="main-button find-mathing-job" onClick={goToSearchPage}>Find matching job</button>
      </div>
      <img
        className="unsplash-bb-s-bf-5-uv-50-a"
        src="/images/hero/unsplash-bb-s-bf-5-uv-50-a0.png"
      />
    </div>
  </div>
  <div className="section-2">
  <div className="home-trust-section">
    <div className="main-header-sec-2">Features and Benefits</div>
    <div className="main-content-sec-2">
      <img className="wave" src="/icons/wave0.svg" />
      <div className="feature-section-list">
        <div className="feature-element-item">
          <img
            className="unsplash-376-kn-i-spl-e"
            src="/images/hero/unsplash-376-kn-i-spl-e0.png"
          />
          <div className="feature-element-text">
            <div className="feature-1">Find your job faster</div>
            <div className="feature-desc">
              Precise search based on your interests, skills and abilities
              allows us to select only the most suitable offers for you
            </div>
          </div>
        </div>
        <div className="feature-element-item">
          <img
            className="unsplash-376-kn-i-spl-e"
            src="/images/hero/unsplash-376-kn-i-spl-e1.png"
          />
          <div className="feature-element-text">
            <div className="feature-1">Make it simple</div>
            <div className="the-feature-or-any-details">
              We have tried to simplify the search process so that it is
              straightforward, intuitive, and leads you directly to the specific
              location.
            </div>
          </div>
        </div>
        <div className="feature-element-item">
          <img
            className="unsplash-376-kn-i-spl-e"
            src="/images/hero/unsplash-376-kn-i-spl-e2.png"
          />
          <div className="feature-element-text">
            <div className="feature-1">Be successful</div>
            <div className="the-feature-or-any-details">
              We search for offers from the largest classNameifieds portals, which
              significantly increases your chances of finding a job.
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
  <div className="section-3">
    <div className="main-header-sec-3">
      <span>
        <span className="main-header-sec-3-span">How </span>
        <span className="main-header-sec-3-span2">StudentWorkHub</span>
        <span className="main-header-sec-3-span"> works?</span>
      </span>
    </div>
    <div className="how-works-section-items">
      <div className="works-feature">
        <div className="works-feature-icon">
          <div className="search-icon">
            <img className="vector" src="/icons/search0.svg" />
          </div>
        </div>
        <div className="work-feature-text">
          <div className="feature-12">Browse job offers</div>
          <div className="description-of-feature-1">
            With StudentWorkHub, you can browse offers from the largest job
            portals.
          </div>
        </div>
      </div>
      <div className="works-feature">
        <div className="works-feature-icon">
          <div className="user-icon">
            <img className="vector2" src="/icons/user0.svg" />
          </div>
        </div>
        <div className="work-feature-text">
          <div className="feature-12">Create a free account</div>
          <div className="description-of-feature-1">
            After creating an account, you will gain access to all the
            app&#039;s features like search and ranking.
          </div>
        </div>
      </div>
      <div className="works-feature">
        <div className="works-feature-icon">
          <img className="robot-icon" src="/icons/robot-icon0.svg" />
        </div>
        <div className="work-feature-text">
          <div className="feature-12">Using AI and algorithms</div>
          <div className="description-of-feature-1">
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
