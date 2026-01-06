"use client";
import loginStyles from '@/styles/LoginStyles.module.css'
import { LoginForm } from '@/components/feature/login/LoginForm';
export default function Login() {
    return (
        <div className={loginStyles["login-account"]}>
            <img
                className={loginStyles["unsplash-0-zx-1-b-dv-5-bny"]}
                src="/images/login/unsplash-0-zx-1-b-dv-5-bny0.png"
                alt=""
            />
            <img
                className={loginStyles["unsplash-g-1-kr-4-ozfoac"]}
                src="/images/login/unsplash-g-1-kr-4-ozfoac0.png"
                alt=""
            />
            <LoginForm/>
        </div>
    )
}
