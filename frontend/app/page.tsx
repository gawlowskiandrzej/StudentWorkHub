"use client";

import Image from "next/image";
import "../styles/Hero.css";

export default function Home() {
  const handleClick = () => {
    alert("Hello my friend!");
  }
  return (
    <div className="hero-page">
  <div className="home-hero-section">
    <div className="main-content">
      <div className="main-content-frame">
        <div className="main-text-frame">
          <div className="bold-and-porefull-heading">
            Find job offers that match your
            <br />
            preferences.
          </div>
          <div className="bold-and-porefull-heading2">
            Finding the best suited job has never been easier!
            <br />
            Want to give it a try?
          </div>
        </div>
        <div className="main-button">
          <div className="find-mathing-job"><button onClick={handleClick}>Find matching job</button></div>
        </div>
      </div>
      <img
        className="unsplash-bb-s-bf-5-uv-50-a"
        src="/images/hero/unsplash-bb-s-bf-5-uv-50-a0.png"
      />
    </div>
  </div>
  <div className="home-trust-section">
    <div className="features-and-benefits">Features and Benefits</div>
    <div className="frame-155">
      <img className="vector" src="/icons/vector0.svg" />
      <div className="cards">
        <div className="feature-section-list">
          <div className="feature-element-item">
            <img
              className="unsplash-376-kn-i-spl-e"
              src="/images/hero/unsplash-376-kn-i-spl-e0.png"
            />
            <div className="feature-element-text">
              <div className="feature-1">Find your job faster</div>
              <div className="the-feature-or-any-details">
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
              <div className="the-feature-or-any-details2">
                We have tried to simplify the search process so that it is
                straightforward, intuitive, and leads you directly to the
                specific location.
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
                We search for offers from the largest classifieds portals, which
                significantly increases your chances of finding a job.
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div className="how-works-section">
    <div className="how-student-work-hub-works">
      <span>
        <span className="how-student-work-hub-works-span">How </span>
        <span className="how-student-work-hub-works-span2"> StudentWorkHub</span>
        <span className="how-student-work-hub-works-span"> works?</span>
      </span>
    </div>
    <div className="how-works-section-items">
      <div className="works-feature">
        <div className="works-feature-icon">
          <div className="search-icon">
            <img className="vector2" src="/icons/vector1.svg" />
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
            <img className="vector3" src="/icons/vector2.svg" />
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
