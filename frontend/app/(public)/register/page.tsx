"use client";
import registerStyles from '@/styles/RegisterStyle.module.css'
import loginStyles from '@/styles/LoginStyles.module.css'
import { RegisterForm } from '@/components/feature/register/RegisterForm';

export default function Register() {
    return (
        <div className={loginStyles["login-account"]}>
            <img
                className={registerStyles["unsplash-0-zx-1-b-dv-5-bny"]}
                src="/images/register/unsplash-0-zx-1-b-dv-5-bny0.png"
            />
            <img
                className={registerStyles["unsplash-g-1-kr-4-ozfoac"]}
                src="/images/register/unsplash-g-1-kr-4-ozfoac0.png"
            />

            <div className={loginStyles["login-form-column"]}>
                <RegisterForm/>
            </div>
        </div>

    );
}