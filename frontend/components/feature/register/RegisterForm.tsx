"use client";

import { useEffect, useMemo } from "react";
import { useRouter } from "next/navigation";

import registerStyles from "@/styles/RegisterStyle.module.css";
import loginStyles from "@/styles/LoginStyles.module.css";
import buttonStyles from "@/styles/ButtonStyle.module.css";

import { useTranslation } from "react-i18next";
import { FloatingLabelInput } from "@/components/ui/floatingInput";
import { Checkbox } from "@/components/ui/checkbox";
import { useRegisterState } from "@/hooks/useRegister";
import { Button } from "@/components/ui/button";
import { Spinner } from "@/components/ui/spinner";

const MAIN_PATH = "/";

const AUTH_LS_KEYS = {
    jwt: "auth.jwt",
    jwtExpiresAtMs: "auth.jwtExpiresAtMs",
} as const;

function hasValidJwtInLocalStorage(): boolean {
    try {
        const jwt = localStorage.getItem(AUTH_LS_KEYS.jwt);
        if (!jwt) return false;

        const expiresAtStr = localStorage.getItem(AUTH_LS_KEYS.jwtExpiresAtMs);
        if (!expiresAtStr) return true;

        const expiresAt = Number(expiresAtStr);
        if (!Number.isFinite(expiresAt)) return true;

        return expiresAt > Date.now();
    } catch {
        return false;
    }
}

export function RegisterForm() {
    const router = useRouter();
    const isAuthed = useMemo(() => hasValidJwtInLocalStorage(), []);

    useEffect(() => {
        if (isAuthed) router.replace(MAIN_PATH);
    }, [isAuthed, router]);

    const { t } = useTranslation(["register", "common"]);
    const { state, update, submit, loading, error } = useRegisterState();

    const errorSlotClass = loginStyles["login-error-slot"];
    const errorBoxClass = loginStyles["login-error-box"];
    const errorHiddenClass = loginStyles["login-error-hidden"];

    const buttonClass =
        `transition-[width] duration-300 gap-4 ease-in-out py-5.5 px-4 ` +
        `inline-flex items-center cursor-pointer justify-center overflow-hidden ` +
        `${buttonStyles["big-scale"]}`;

    const resolveErrorText = (err: string | null) => {
        if (!err) return null;
        if (err === "CONSENT_REQUIRED") return "Musisz zaakceptować zgodę.";
        return err;
    };


    const handleSubmit = async () => {
        const ok = await submit();
        if (!ok) return;

        router.replace("/login");
    };

    if (isAuthed) {
        return (
            <div className={registerStyles["register-form-with-button"]}>
                <div className="mt-10 flex justify-center">
                    <Spinner />
                </div>
            </div>
        );
    }

    return (
        <div className={registerStyles["register-form-with-button"]}>
            <div className={registerStyles["register-inputs"]}>
                <div className={loginStyles["login-to-your-account"]}>{t("createAccount")}</div>

                <div className={registerStyles["register-inputs-items"]}>
                    <div className={registerStyles["username-surname-items"]}>
                        <FloatingLabelInput
                            type="text"
                            label={t("register:name")}
                            onChange={(e) => update("name", e.target.value)}
                        />
                        <FloatingLabelInput
                            type="text"
                            label={t("register:surname")}
                            onChange={(e) => update("surname", e.target.value)}
                        />
                    </div>

                    <div className={`${loginStyles["login-form-inputs"]} gap-5!`}>
                        <FloatingLabelInput
                            type="text"
                            label={t("common:email")}
                            onChange={(e) => update("email", e.target.value)}
                        />
                        <FloatingLabelInput
                            type="password"
                            label={t("common:password")}
                            onChange={(e) => update("password", e.target.value)}
                        />

                        {/* Error slot like in Login */}
                        <div className={errorSlotClass} aria-live="polite">
                            <div
                                className={`${errorBoxClass} ${!error ? errorHiddenClass : ""}`}
                                role={error ? "alert" : undefined}
                            >
                                {resolveErrorText(error) || "\u00A0"}
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className={loginStyles["remember-me-clouse"]}>
                <label className="flex gap-2 cursor-pointer">
                    <Checkbox
                        className={loginStyles["checkbox"]}
                        checked={state.consent}
                        onCheckedChange={(v) => update("consent", Boolean(v))}
                    />

                    <div className={registerStyles["i-consent-to-the"]}>
                        <div className={registerStyles["clause-div"]}>
                            <div className={registerStyles["i-consent-to"]}>*</div>
                            <span className={registerStyles["i-consent"]}>{t("register:clause")}</span>
                        </div>
                    </div>
                </label>
            </div>

            <Button onClick={handleSubmit} disabled={loading} className={buttonClass}>
                <img className={loginStyles["log-out"]} src="/icons/users0.svg" alt="" />
                <div className={loginStyles["sign-in"]}>
                    {loading ? t("register:creatingAccount") : t("register:createAccount")}
                </div>
                {loading && <Spinner />}
            </Button>
        </div>
    );
}
