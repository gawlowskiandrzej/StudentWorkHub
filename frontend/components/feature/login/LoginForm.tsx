"use client";

import { useEffect, useMemo, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";

import loginStyles from "@/styles/LoginStyles.module.css";
import buttonStyles from "@/styles/ButtonStyle.module.css";
import { Checkbox } from "@/components/ui/checkbox";
import { FloatingLabelInput } from "@/components/ui/floatingInput";
import { useTranslation } from "react-i18next";
import { useLoginState } from "@/hooks/useLogin";
import { Button } from "@/components/ui/button";
import { Spinner } from "@/components/ui/spinner";
import { useUser } from "@/store/userContext";

// Redirect code to use on other sites: router.replace(`/login?next=${encodeURIComponent(window.location.pathname + window.location.search + window.location.hash)}`);
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

function safeSameOriginPath(input: string): string | null {
    try {
        const url = new URL(input, window.location.origin);
        if (url.origin !== window.location.origin) return null;

        const path = `${url.pathname}${url.search}${url.hash}`;
        if (path === "/login" || path.startsWith("/login?")) return null;

        return path;
    } catch {
        return null;
    }
}

function resolveRedirectTarget(nextParam: string | null): string {
    if (nextParam) {
        const target = safeSameOriginPath(nextParam);
        if (target) return target;
    }

    if (document.referrer) {
        const target = safeSameOriginPath(document.referrer);
        if (target) return target;
    }

    return MAIN_PATH;
}

export function LoginForm() {
    const router = useRouter();
    const isAuthed = useMemo(() => hasValidJwtInLocalStorage(), []);
    const { t } = useTranslation(["common", "loginView"]);
    const searchParams = useSearchParams();
    const { state, update, submit, loading, error } = useLoginState();
    const { fetchPreferences, preferences } = useUser();
    
    const [isCheckingPreferences, setIsCheckingPreferences] = useState(false);

    useEffect(() => {
        if (isAuthed) {
            router.replace(MAIN_PATH);
        }
    }, [router, isAuthed]);

    const columnClass = loginStyles["login-form-column"];
    const formClass = loginStyles["login-form"];
    const titleClass = loginStyles["login-to-your-account"];
    const inputsClass = loginStyles["login-form-inputs"];
    const rememberWrapClass = `${loginStyles["remember-me-clouse"]} w-full!`;
    const checkboxClass = loginStyles["checkbox"];
    const rememberTextClass = loginStyles["remember-me"];
    const iconClass = loginStyles["log-out"];
    const signInTextClass = loginStyles["sign-in"];

    const errorSlotClass = loginStyles["login-error-slot"];
    const errorBoxClass = loginStyles["login-error-box"];
    const errorHiddenClass = loginStyles["login-error-hidden"];

    const buttonClass =
        `transition-[width] duration-300 gap-4 ease-in-out py-5.5 px-4 ` +
        `inline-flex items-center cursor-pointer justify-center overflow-hidden ` +
        `${buttonStyles["big-scale"]}`;

    // Effect to handle redirect after preferences are fetched
    useEffect(() => {
        if (!isCheckingPreferences) return;

        const performRedirect = async () => {
            const nextParam = searchParams.get("next");
            const target = resolveRedirectTarget(nextParam);

            // Check if preferences are incomplete (missing leadingCategory)
            // If leadingCategory is missing, redirect to profile creation
            if (!preferences?.leading_category_id) {
                router.replace("/profile-creation");
            } else {
                router.replace(target);
            }

            setIsCheckingPreferences(false);
        };

        performRedirect();
    }, [isCheckingPreferences, preferences, searchParams, router]);

    const handleSubmit = async () => {
        const ok = await submit();
        if (!ok) return;

        // Set flag to fetch preferences and handle redirect
        setIsCheckingPreferences(true);
        
        // Fetch preferences to check if leadingCategory is set
        await fetchPreferences();
    };

    if (isAuthed) {
        return (
            <div className={loginStyles["login-form-column"]}>
                <div className="mt-10 flex justify-center">
                    <Spinner />
                </div>
            </div>
        );
    }

    return (
        <div className={columnClass}>
            <div className={formClass}>
                <div className={titleClass}>{t("loginView:loginToYourAccount")}</div>

                <div className={inputsClass}>
                    <FloatingLabelInput
                        type="text"
                        label={t("common:email")}
                        onChange={(e) => update("email", e.target.value)}
                    />
                    <FloatingLabelInput
                        type="password"
                        label={t("password")}
                        onChange={(e) => update("password", e.target.value)}
                    />

                    { }
                    {/* <div className={errorSlotClass} aria-live="polite">
                        <div
                            className={`${errorBoxClass} ${!error ? errorHiddenClass : ""}`}
                            role={error ? "alert" : undefined}
                        >
                            {error || "\u00A0"}
                        </div>
                    </div> */}
                </div>
            </div>

            <div className={rememberWrapClass}>
                <label className="flex gap-2 cursor-pointer">
                    <Checkbox
                        className={checkboxClass}
                        checked={state.rememberMe}
                        onCheckedChange={(v) => update("rememberMe", Boolean(v))}
                    />
                    <div className={rememberTextClass}>{t("common:rememberMe")}</div>
                </label>

                <a href="/register" className="text-sm">
                    {t("loginView:registerCta")}
                </a>
            </div>

            <div className="mt-5">
                <Button onClick={handleSubmit} disabled={loading} className={buttonClass}>
                    <img className={iconClass} src="/icons/log-in0.svg" alt="" />

                    <div className={signInTextClass}>
                        {loading ? t("loginView:signInLoading") : t("common:signIn")}
                    </div>

                    {loading && <Spinner />}
                </Button>
            </div>
        </div>
    );
}
