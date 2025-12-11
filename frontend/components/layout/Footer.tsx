import "../../styles/Footer.css";
import { Inter } from "next/font/google";

const inter = Inter({
  weight: ['400', '700'],
  subsets: ['latin'],
  display: 'swap',
});


const Footer = () => {
    return (
        <footer className={`footer border-t ${inter.className}`}>
            <div className="footer-content">
                <div className="about-section">
                <div className="about-us">About us</div>
                <div className="aboutus-desc">
                    StudentWorkHub is an engineering project created by three people who
                    <br />
                    want to find a job quickly and easily, based on their profession and
                    hobbies.
                </div>
                </div>
                <div className="navlinks">
                <div className="frame-2">
                    <div className="account">Account</div>
                    <div className="login">Login</div>
                    <div className="register">Register</div>
                    <div className="profile">Profile</div>
                </div>
                <div className="frame-3">
                    <div className="offers">Offers</div>
                    <div className="matched">Search</div>
                    <div className="matched">Matched</div>
                    <div className="all-offers">All offers</div>
                </div>
                <div className="frame-4">
                    <div className="links">Links</div>
                    <div className="privace-policy">Privace Policy</div>
                    <div className="legal-notice">Legal Notice</div>
                    <div className="project-information">Project information</div>
                </div>
                </div>
                <div className="socials">
                <div className="follow-the-project">Follow the project</div>
                <div className="community-icons">
                    <img className="github" src="/icons/github0.svg" />
                    <img className="facebook" src="/icons/facebook0.svg" />
                    <img className="at-sign" src="/icons/at-sign0.svg" />
                </div>
                </div>
            </div>
            <div className="copyright">
                <div className="student-work-hub">StudentWorkHub</div>
                <div className="copy-right-text">Copyright 2025. All rights reserved.</div>
            </div>
        </footer>
    );
}
 
export default Footer;