"use client";
import { useRouter } from "next/navigation";
import "../../styles/Navigation.css";
import { Jura } from "next/font/google";

const jura = Jura({
  weight: ['400', '700'],
  subsets: ['latin'],
  display: 'swap',
});

const Navbar = () => {
    const router = useRouter();
    return (
       <nav className="navigation-with-short-logo">
          <div className="logo cursor-pointer" onClick={() => router.push('/')}>
            <img className="student-work-hub-logo-1" src="/images/all/student-work-hub-logo-10.png" />
            <div className={`logo-text ${jura.className}`}>
              <div className="student-work-hub">StudentWorkHub</div>
              <div className="subtitle-student-work-hub">your carrer starts now !</div>
            </div>
          </div>
          <div className="signin-frame">
            <div id="register-button" className="main-button">
              <div className="text-xs">Register</div>
            </div>
            <img className="user" src="/icons/user0.svg" />
          </div>
      </nav>
    );
}
 
export default Navbar;