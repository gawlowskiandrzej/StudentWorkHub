import loginStyles from '@/styles/LoginStyles.module.css'
import buttonStyles from '@/styles/ButtonStyle.module.css'
import { Checkbox } from '@/components/ui/checkbox'
import { FloatingLabelInput } from '@/components/ui/floatingInput'
import { useTranslation } from 'react-i18next'
import { useLoginState } from '@/hooks/useLogin'
import { Button } from '@/components/ui/button'
import { Spinner } from '@/components/ui/spinner'

export function LoginForm() {
    const { t } = useTranslation(["common", "loginView"]);
    const { state, update, submit, loading } = useLoginState();
    return (
        <div className={loginStyles["login-form-column"]}>
            <div className={loginStyles["login-form"]}>
                <div className={loginStyles["login-to-your-account"]}>
                    {t("loginView:loginToYourAccount")}
                </div>

                <div className={loginStyles["login-form-inputs"]}>
                    <FloatingLabelInput type='text' label={t("common:email")} onChange={(e) => update("email", e.target.value)} />
                    <FloatingLabelInput type='password' label={t("password")} onChange={(e) => update("password", e.target.value)} />
                </div>
            </div>
            <div className={`${loginStyles["remember-me-clouse"]} w-full!`}>
                <label className='flex gap-2 cursor-pointer'>
                    <Checkbox className={loginStyles["checkbox"]} checked={state.rememberMe} onCheckedChange={(v) => update("rememberMe", Boolean(v))} />
                    <div className={loginStyles["remember-me"]}>
                        {t("common:rememberMe")}
                    </div>
                </label>
                <a href='/register' className='text-sm'>Nie masz konta? Zarejestruj się!</a>
            </div>
            <div className='mt-5'>
                <Button onClick={submit} disabled={loading} className={`transition-[width] duration-300 gap-4 ease-in-out py-5.5 px-4 inline-flex items-center cursor-pointer justify-center overflow-hidden ${buttonStyles["big-scale"]}`}>
                    <img
                        className={loginStyles["log-out"]}
                        src="/icons/log-in0.svg"
                        alt=""
                    />
                    <div className={loginStyles["sign-in"]}>
                        {loading ? t("loginView:signInLoading") :t("common:signIn")}
                    </div>
                    {loading && <Spinner/>}
                </Button>
            </div>

        </div>
    );
}