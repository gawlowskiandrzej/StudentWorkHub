"use client";
import { LoginState } from "@/types/login/loginState";
import { useState } from "react";
import { useUser } from "@/store/userContext";
import type { StandardLoginRequestDto } from "@/types/api/usersDTO";

export const initialState: LoginState = {
    email: "",
    password: "",
    rememberMe: false,
};

const MAIN_PATH_DEFAULT = "/";

function safeSameOriginPathFromNextParam(nextParam: string | null): string | null {
    if (!nextParam) return null;

    try {
        const url = new URL(nextParam, window.location.origin);
        if (url.origin !== window.location.origin) return null;

        const path = `${url.pathname}${url.search}${url.hash}`;
        if (path === "/login" || path.startsWith("/login?")) return null;

        return path;
    } catch {
        return null;
    }
}

function safeSameOriginPathFromReferrer(): string | null {
    const ref = document.referrer;
    if (!ref) return null;

    try {
        const url = new URL(ref);
        if (url.origin !== window.location.origin) return null;

        const path = `${url.pathname}${url.search}${url.hash}`;
        if (path === "/login" || path.startsWith("/login?")) return null;

        return path;
    } catch {
        return null;
    }
}

export function resolvePostLoginRedirect(nextParam: string | null, mainPath: string): string {
    return (
        safeSameOriginPathFromNextParam(nextParam) ??
        safeSameOriginPathFromReferrer() ??
        mainPath
    );
}

export function useLoginState() {
    const [state, setState] = useState<LoginState>(initialState);
    const [hasSubmitted, setHasSubmitted] = useState(false);

    const { standardLogin, loading, error } = useUser();

    const update = <K extends keyof LoginState>(key: K, value: LoginState[K]) => {
        setHasSubmitted(false);
        setState((prev) => ({ ...prev, [key]: value }));
    };

    const submit = async (): Promise<boolean> => {
        if (loading) return false;

        setHasSubmitted(true);

        const dto: StandardLoginRequestDto = {
            login: state.email.trim(),
            password: state.password,
            rememberMe: state.rememberMe,
        };

        return standardLogin(dto);
    };

    const submitAndResolveRedirect = async (
        nextParam: string | null,
        mainPath: string = MAIN_PATH_DEFAULT
    ): Promise<{ ok: boolean; target: string }> => {
        const ok = await submit();
        const target = ok ? resolvePostLoginRedirect(nextParam, mainPath) : mainPath;
        return { ok, target };
    };

    return {
        state,
        update,
        submit,
        submitAndResolveRedirect,
        loading,

        error: hasSubmitted ? error : null,

        rawError: error,
    };
}
