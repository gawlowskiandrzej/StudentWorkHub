"use client";
import registerStyles from '@/styles/RegisterStyle.module.css'
import loginStyles from '@/styles/LoginStyles.module.css'
import buttonStyles from '@/styles/ButtonStyle.module.css'
import { useTranslation } from 'react-i18next';
import { FloatingLabelInput } from '@/components/ui/floatingInput';
import { Checkbox } from '@/components/ui/checkbox';
import { useRegisterState } from '@/hooks/useRegister';
import { Button } from '@/components/ui/button';
import { Spinner } from '@/components/ui/spinner';

export function RegisterForm() {
    const { t } = useTranslation(["register", "common"]);
    const { state, update, submit, loading } = useRegisterState();
    return (
        <div className={registerStyles["register-form-with-button"]}>
            <div className={registerStyles["register-inputs"]}>
                <div className={loginStyles["login-to-your-account"]}>
                    {t("createAccount")}
                </div>

                <div className={registerStyles["register-inputs-items"]}>
                    <div className={registerStyles["username-surname-items"]}>
                        <FloatingLabelInput type='text' label={t("register:name")} onChange={(e) => update("name", e.target.value)} />
                        <FloatingLabelInput type='text' label={t("register:surname")} onChange={(e) => update("surname", e.target.value)} />
                    </div>

                    <div className={`${loginStyles["login-form-inputs"]} gap-5!`}>
                        <FloatingLabelInput type='text' label={t("common:email")} onChange={(e) => update("email", e.target.value)} />
                        <FloatingLabelInput type='password' label={t("common:password")} onChange={(e) => update("password", e.target.value)} />
                    </div>
                </div>
            </div>

            <div className={loginStyles["remember-me-clouse"]}>
                <label className='flex gap-2 cursor-pointer'>
                    <Checkbox  className={loginStyles["checkbox"]} checked={state.consent} onCheckedChange={(v) => update("consent", Boolean(v))} />

                    <div
                        className={
                            registerStyles["i-consent-to-the"]
                        }
                    >
                        <div className={registerStyles["clause-div"]}>
                            <div className={registerStyles["i-consent-to"]}>
                                *
                            </div>
                            <span
                                className={registerStyles["i-consent"]}
                            >
                                {t("register:clause")}
                            </span>
                        </div>
                    </div>
                </label>
            </div>
            <Button onClick={submit} disabled={loading} className={`transition-[width] duration-300 gap-4 ease-in-out py-5.5 px-4 inline-flex items-center cursor-pointer justify-center overflow-hidden ${buttonStyles["big-scale"]}`}>
                <img
                    className={loginStyles["log-out"]}
                    src="/icons/users0.svg"
                />
                <div className={loginStyles["sign-in"]}>{loading ? t("register:creatingAccount") : t("register:createAccount")}</div>
                {loading && <Spinner/>}
            </Button>           
        </div>

    );
}