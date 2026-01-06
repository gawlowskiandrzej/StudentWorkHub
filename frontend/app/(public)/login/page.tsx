"use client";
import loginStyles from '@/styles/LoginStyles.module.css'
import buttonStyles from '@/styles/ButtonStyle.module.css'
import { Checkbox } from '@/components/ui/checkbox'
import { FloatingLabelInput } from '@/components/ui/floatingInput'
import { useTranslation } from 'react-i18next'
export default function Login() {
    const { t } = useTranslation(["common", "loginView"]);
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
            <div className={loginStyles["login-form-column"]}>
                <div className={loginStyles["login-form"]}>
                    <div className={loginStyles["login-to-your-account"]}>
                        {t("loginView:loginToYourAccount")}
                    </div>

                    <div className={loginStyles["login-form-inputs"]}>
                        <FloatingLabelInput type='text' label='Email' />
                        <FloatingLabelInput type='password' label={t("password")} />
                    </div>
                </div>
                <div className={loginStyles["remember-me-clouse"]}>
                    <label className='flex gap-2 cursor-pointer'>
                        <Checkbox className={loginStyles["checkbox"]} />
                        <div className={loginStyles["remember-me"]}>
                            {t("common:rememberMe")}
                        </div>
                    </label>
                    <a href='/register' className='text-sm'>Nie masz konta? Zarejestruj się!</a>
                </div>
                <div className='mt-5'>
                    <div className={buttonStyles["main-button"]}>
                        <img
                            className={loginStyles["log-out"]}
                            src="/icons/log-in0.svg"
                            alt=""
                        />
                        <div className={loginStyles["sign-in"]}>
                            {t("common:signIn")}
                        </div>
                    </div>
                </div>

            </div>
        </div>
    )
}
