"use client";

import React, { createContext, useContext, useEffect, useMemo, useState } from "react";
import { UserApi } from "@/lib/api/controllers/user";

import type {
    StandardRegisterRequestDto,
    StandardRegisterResponseDto,
    StandardLoginRequestDto,
    StandardLoginResponseDto,
    TokenLoginResponseDto,
    UpdateDataResponseDto,
    UpdateWeightsResponseDto,
    ChangePasswordResponseDto,
    LogoutResponseDto,
    DeleteUserResponseDto,
    RefreshJwtResponseDto,
    CheckJwtResponseDto,
    CheckPermissionResponseDto,
    InsertSearchHistoryResponseDto,
    GetSearchHistoryResponseDto,
    GetLastSearchesResponseDto,
    GetWeightsResponseDto,
    GetDataResponseDto,
    UpdatePreferencesResponseDto,
    GetPreferencesResponseDto,
    UserDataDto,
    PreferencesDto,
    WeightsDto,
    SearchHistoryEntryDto,
    LastSearchesEntryDto,
} from "@/types/api/usersDTO";

type UserContextType = {
    jwt: string | null;
    rememberMeToken: string | null;
    isAuthenticated: boolean;

    // JWT meta stored in localStorage
    jwtSavedAtMs: number | null;
    jwtExpiresAtMs: number | null;

    // Derived info for refresh logic
    isJwtExpired: boolean;
    isJwtExpiringSoon: boolean;

    loading: boolean;
    error: string | null;

    userData: UserDataDto | null;
    weights: WeightsDto | null;
    preferences: PreferencesDto | null;
    preferencesIncomplete: boolean;

    searchHistory: SearchHistoryEntryDto[];
    lastSearches: LastSearchesEntryDto[];

    standardRegister: (dto: StandardRegisterRequestDto) => Promise<boolean>;
    standardLogin: (dto: StandardLoginRequestDto) => Promise<boolean>;
    tokenLogin: () => Promise<boolean>;
    logout: () => Promise<boolean>;
    deleteUser: () => Promise<boolean>;

    refreshJwt: () => Promise<boolean>;
    checkJwt: () => Promise<boolean>;

    // Refresh only when needed (based on expiresAt if available)
    maybeRefreshJwt: () => Promise<boolean>;

    fetchUserData: () => Promise<void>;
    fetchWeights: () => Promise<void>;
    fetchPreferences: () => Promise<void>;

    updateData: (dto: Omit<Parameters<typeof UserApi.updateData>[0], "jwt">) => Promise<boolean>;
    updateWeights: (dto: Omit<Parameters<typeof UserApi.updateWeights>[0], "jwt">) => Promise<boolean>;
    updatePreferences: (dto: Omit<Parameters<typeof UserApi.updatePreferences>[0], "jwt">) => Promise<boolean>;

    changePassword: (newPassword: string) => Promise<boolean>;
    checkPermission: (permissionName: string) => Promise<boolean>;

    insertSearchHistory: (
        dto: Omit<Parameters<typeof UserApi.insertSearchHistory>[0], "jwt">
    ) => Promise<boolean>;

    getSearchHistory: (limit: number) => Promise<void>;
    getLastSearches: (limit: number) => Promise<void>;
};

const UserContext = createContext<UserContextType | undefined>(undefined);

const LS_JWT_KEY = "auth.jwt";
const LS_REMEMBER_KEY = "auth.rememberMeToken";
const LS_JWT_SAVED_AT_MS_KEY = "auth.jwtSavedAtMs";
const LS_JWT_EXPIRES_AT_MS_KEY = "auth.jwtExpiresAtMs";

// If JWT has exp, refresh a bit before expiration
const JWT_REFRESH_SAFETY_WINDOW_MS = 2 * 60 * 1000; // 2 minutes
const JWT_FALLBACK_TTL_MS = 3 * 60 * 60 * 1000; // 3 hours

function tryParseJsonFromApiError<T>(apiError: string | null): T | null {
    if (!apiError) return null;

    const jsonStartIndex = apiError.indexOf("{");
    if (jsonStartIndex < 0) return null;

    const jsonPart = apiError.slice(jsonStartIndex);
    try {
        return JSON.parse(jsonPart) as T;
    } catch {
        return null;
    }
}

function extractErrorMessage(apiError: string | null, fallback: string): string {
    if (!apiError) return fallback;

    const parsed = tryParseJsonFromApiError<{ errorMessage?: string }>(apiError);
    if (parsed?.errorMessage) return parsed.errorMessage;

    return apiError;
}

function safeNumber(value: string | null): number | null {
    if (!value) return null;
    const n = Number(value);
    return Number.isFinite(n) ? n : null;
}

function decodeBase64UrlToJson<T>(base64Url: string): T | null {
    try {
        const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
        const padded = base64.padEnd(base64.length + ((4 - (base64.length % 4)) % 4), "=");
        const json = atob(padded);
        return JSON.parse(json) as T;
    } catch {
        return null;
    }
}

function getJwtExpiresAtMs(jwt: string): number | null {
    // JWT format: header.payload.signature
    const parts = jwt.split(".");
    if (parts.length < 2) return null;

    const payload = decodeBase64UrlToJson<{ exp?: number }>(parts[1]);
    if (!payload?.exp || typeof payload.exp !== "number") return null;

    return payload.exp * 1000;
}

export const UserProvider = ({ children }: { children: React.ReactNode }) => {
    const [jwt, setJwt] = useState<string | null>(null);
    const [rememberMeToken, setRememberMeToken] = useState<string | null>(null);

    const [jwtSavedAtMs, setJwtSavedAtMs] = useState<number | null>(null);
    const [jwtExpiresAtMs, setJwtExpiresAtMs] = useState<number | null>(null);

    const [userData, setUserData] = useState<UserDataDto | null>(null);
    const [weights, setWeights] = useState<WeightsDto | null>(null);
    const [preferences, setPreferences] = useState<PreferencesDto | null>(null);
    const [preferencesIncomplete, setPreferencesIncomplete] = useState(false);

    const [searchHistory, setSearchHistory] = useState<SearchHistoryEntryDto[]>([]);
    const [lastSearches, setLastSearches] = useState<LastSearchesEntryDto[]>([]);

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const effectiveJwtExpiresAtMs = useMemo(() => {
        if (!jwt) return null;

        // Prefer JWT "exp" (parsed to ms) if available
        if (jwtExpiresAtMs) return jwtExpiresAtMs;

        // Fallback: 3 hours from when we saved it locally
        if (jwtSavedAtMs) return jwtSavedAtMs + JWT_FALLBACK_TTL_MS;

        return null;
    }, [jwt, jwtExpiresAtMs, jwtSavedAtMs]);

    const isJwtExpired = useMemo(() => {
        if (!jwt) return true;
        if (!effectiveJwtExpiresAtMs) return false; // unknown -> assume not expired here
        return Date.now() >= effectiveJwtExpiresAtMs;
    }, [jwt, effectiveJwtExpiresAtMs]);

    const isAuthenticated = useMemo(() => {
        return Boolean(jwt) && !isJwtExpired;
    }, [jwt, isJwtExpired]);

    const isJwtExpiringSoon = useMemo(() => {
        if (!jwt) return false;
        if (!effectiveJwtExpiresAtMs) return false;
        return effectiveJwtExpiresAtMs - Date.now() <= JWT_REFRESH_SAFETY_WINDOW_MS;
    }, [jwt, effectiveJwtExpiresAtMs]);

    const persistTokens = (nextJwt: string | null, nextRememberMeToken: string | null) => {
        setJwt(nextJwt);
        setRememberMeToken(nextRememberMeToken);

        try {
            if (nextJwt) {
                localStorage.setItem(LS_JWT_KEY, nextJwt);

                const nowMs = Date.now();
                localStorage.setItem(LS_JWT_SAVED_AT_MS_KEY, String(nowMs));
                setJwtSavedAtMs(nowMs);

                const expiresAtMs = getJwtExpiresAtMs(nextJwt);
                if (expiresAtMs) {
                    localStorage.setItem(LS_JWT_EXPIRES_AT_MS_KEY, String(expiresAtMs));
                    setJwtExpiresAtMs(expiresAtMs);
                } else {
                    localStorage.removeItem(LS_JWT_EXPIRES_AT_MS_KEY);
                    setJwtExpiresAtMs(null);
                }
            } else {
                localStorage.removeItem(LS_JWT_KEY);
                localStorage.removeItem(LS_JWT_SAVED_AT_MS_KEY);
                localStorage.removeItem(LS_JWT_EXPIRES_AT_MS_KEY);

                setJwtSavedAtMs(null);
                setJwtExpiresAtMs(null);
            }

            if (nextRememberMeToken) localStorage.setItem(LS_REMEMBER_KEY, nextRememberMeToken);
            else localStorage.removeItem(LS_REMEMBER_KEY);
        } catch {
            // Ignore storage errors
        }
    };

    const requireJwt = (): string | null => {
        if (!jwt) {
            setError("Brak zalogowanego użytkownika.");
            return null;
        }
        return jwt;
    };

    const standardRegister = async (dto: StandardRegisterRequestDto): Promise<boolean> => {
        setLoading(true);
        setError(null);

        const res = await UserApi.standardRegister(dto);

        setLoading(false);

        if (res.error) {
            setError(extractErrorMessage(res.error, "Rejestracja nie powiodła się."));
            return false;
        }

        const data: StandardRegisterResponseDto | null = res.data;
        if (!data) {
            setError("Rejestracja nie powiodła się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const standardLogin = async (dto: StandardLoginRequestDto): Promise<boolean> => {
        setLoading(true);
        setError(null);

        const res = await UserApi.standardLogin(dto);

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<StandardLoginResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Logowanie nie powiodło się."));
            return false;
        }

        const data: StandardLoginResponseDto | null = res.data;
        if (!data) {
            setError("Logowanie nie powiodło się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        // (1) Save JWT after successful login
        // (2) Save rememberMeToken after successful login
        persistTokens(data.jwt, data.rememberMeToken || null);
        return true;
    };

    const tokenLogin = async (): Promise<boolean> => {
        if (!rememberMeToken) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.tokenLogin({ token: rememberMeToken });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<TokenLoginResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Logowanie tokenem nie powiodło się."));
            return false;
        }

        const data: TokenLoginResponseDto | null = res.data;
        if (!data) {
            setError("Logowanie tokenem nie powiodło się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        // (3) Replace rememberMeToken/JWT after token login
        persistTokens(data.jwt, data.rememberMeToken || null);
        return true;
    };

    const refreshJwt = async (): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.refreshJwt({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<RefreshJwtResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Odświeżenie JWT nie powiodło się."));
            return false;
        }

        const data: RefreshJwtResponseDto | null = res.data;
        if (!data) {
            setError("Odświeżenie JWT nie powiodło się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        // (4) Replace JWT after refresh-jwt (rememberMeToken stays as-is)
        persistTokens(data.jwt, rememberMeToken);
        return true;
    };

    const maybeRefreshJwt = async (): Promise<boolean> => {
        if (!jwt) return false;
        if (!jwtExpiresAtMs) return false; // no exp info -> do not auto decide
        if (isJwtExpired) return false;

        if (isJwtExpiringSoon) {
            return refreshJwt();
        }

        return true;
    };

    const checkJwt = async (): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        const res = await UserApi.checkJwt({ jwt: currentJwt });

        if (res.data) {
            const data: CheckJwtResponseDto = res.data;
            return Boolean(data.result);
        }

        const parsed = tryParseJsonFromApiError<CheckJwtResponseDto>(res.error);
        if (parsed) return Boolean(parsed.result);

        return false;
    };

    const logout = async (): Promise<boolean> => {
        setLoading(true);
        setError(null);

        const currentJwt = jwt;

        // (5) Remove JWT/rememberMeToken on logout (always locally)
        persistTokens(null, null);

        setUserData(null);
        setWeights(null);
        setPreferences(null);
        setPreferencesIncomplete(false);
        setSearchHistory([]);
        setLastSearches([]);

        if (!currentJwt) {
            setLoading(false);
            return true;
        }

        const res = await UserApi.logout({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            setError(extractErrorMessage(res.error, "Wylogowano lokalnie, ale backend zwrócił błąd."));
            return false;
        }

        const data: LogoutResponseDto | null = res.data;
        if (data?.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const deleteUser = async (): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.deleteUser({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            setError(extractErrorMessage(res.error, "Usunięcie konta nie powiodło się."));
            return false;
        }

        const data: DeleteUserResponseDto | null = res.data;
        if (!data) {
            setError("Usunięcie konta nie powiodło się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        persistTokens(null, null);

        setUserData(null);
        setWeights(null);
        setPreferences(null);
        setPreferencesIncomplete(false);
        setSearchHistory([]);
        setLastSearches([]);

        return true;
    };

    const fetchUserData = async (): Promise<void> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return;

        setLoading(true);
        setError(null);

        const res = await UserApi.getData({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<GetDataResponseDto>(res.error);
            if (parsed?.userData && typeof parsed.userData === "object") {
                setUserData(parsed.userData as UserDataDto);
            }
            setError(extractErrorMessage(res.error, "Nie udało się pobrać danych użytkownika."));
            return;
        }

        const data: GetDataResponseDto | null = res.data;
        if (!data) {
            setError("Nie udało się pobrać danych użytkownika (brak odpowiedzi).");
            return;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return;
        }

        setUserData(data.userData as UserDataDto);
    };

    const fetchWeights = async (): Promise<void> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return;

        setLoading(true);
        setError(null);

        const res = await UserApi.getWeights({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<GetWeightsResponseDto>(res.error);
            if (parsed?.weights && typeof parsed.weights === "object") {
                setWeights(parsed.weights as WeightsDto);
            }
            setError(extractErrorMessage(res.error, "Nie udało się pobrać wag."));
            return;
        }

        const data: GetWeightsResponseDto | null = res.data;
        if (!data) {
            setError("Nie udało się pobrać wag (brak odpowiedzi).");
            return;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return;
        }

        setWeights(data.weights as WeightsDto);
    };

    const fetchPreferences = async (): Promise<void> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return;

        setLoading(true);
        setError(null);
        setPreferencesIncomplete(false);

        const res = await UserApi.getPreferences({ jwt: currentJwt });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<GetPreferencesResponseDto>(res.error);
            if (parsed?.preferences && typeof parsed.preferences === "object") {
                setPreferences(parsed.preferences as PreferencesDto);
                setPreferencesIncomplete(parsed.errorMessage === "Preferences are incomplete");
                if (parsed.errorMessage) setError(parsed.errorMessage);
                return;
            }

            setError(extractErrorMessage(res.error, "Nie udało się pobrać preferencji."));
            return;
        }

        const data: GetPreferencesResponseDto | null = res.data;
        if (!data) {
            setError("Nie udało się pobrać preferencji (brak odpowiedzi).");
            return;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
        }

        setPreferences(data.preferences as PreferencesDto);
        setPreferencesIncomplete(data.errorMessage === "Preferences are incomplete");
    };

    const updateData = async (
        dto: Omit<Parameters<typeof UserApi.updateData>[0], "jwt">
    ): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.updateData({ jwt: currentJwt, ...dto });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<UpdateDataResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Aktualizacja danych nie powiodła się."));
            return false;
        }

        const data: UpdateDataResponseDto | null = res.data;
        if (!data) {
            setError("Aktualizacja danych nie powiodła się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const updateWeights = async (
        dto: Omit<Parameters<typeof UserApi.updateWeights>[0], "jwt">
    ): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.updateWeights({ jwt: currentJwt, ...dto });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<UpdateWeightsResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Aktualizacja wag nie powiodła się."));
            return false;
        }

        const data: UpdateWeightsResponseDto | null = res.data;
        if (!data) {
            setError("Aktualizacja wag nie powiodła się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const updatePreferences = async (
        dto: Omit<Parameters<typeof UserApi.updatePreferences>[0], "jwt">
    ): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.updatePreferences({ jwt: currentJwt, ...dto });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<UpdatePreferencesResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Aktualizacja preferencji nie powiodła się."));
            return false;
        }

        const data: UpdatePreferencesResponseDto | null = res.data;
        if (!data) {
            setError("Aktualizacja preferencji nie powiodła się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const changePassword = async (newPassword: string): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.changePassword({ jwt: currentJwt, newPassword });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<ChangePasswordResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Zmiana hasła nie powiodła się."));
            return false;
        }

        const data: ChangePasswordResponseDto | null = res.data;
        if (!data) {
            setError("Zmiana hasła nie powiodła się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const checkPermission = async (permissionName: string): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        const res = await UserApi.checkPermission({ jwt: currentJwt, permissionName });

        if (res.data) {
            const data: CheckPermissionResponseDto = res.data;
            return data.errorMessage === "";
        }

        const parsed = tryParseJsonFromApiError<CheckPermissionResponseDto>(res.error);
        if (parsed) return parsed.errorMessage === "";

        return false;
    };

    const insertSearchHistory = async (
        dto: Omit<Parameters<typeof UserApi.insertSearchHistory>[0], "jwt">
    ): Promise<boolean> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return false;

        setLoading(true);
        setError(null);

        const res = await UserApi.insertSearchHistory({ jwt: currentJwt, ...dto });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<InsertSearchHistoryResponseDto>(res.error);
            if (parsed?.errorMessage) setError(parsed.errorMessage);
            else setError(extractErrorMessage(res.error, "Zapis historii wyszukiwania nie powiódł się."));
            return false;
        }

        const data: InsertSearchHistoryResponseDto | null = res.data;
        if (!data) {
            setError("Zapis historii wyszukiwania nie powiódł się (brak odpowiedzi).");
            return false;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            return false;
        }

        return true;
    };

    const getSearchHistory = async (limit: number): Promise<void> => {
        const currentJwt = requireJwt();
        if (!currentJwt) return;

        setLoading(true);
        setError(null);

        const res = await UserApi.getSearchHistory({ jwt: currentJwt, limit });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<GetSearchHistoryResponseDto>(res.error);
            if (parsed?.searchHistory) setSearchHistory(parsed.searchHistory);
            setError(extractErrorMessage(res.error, "Nie udało się pobrać historii wyszukiwań."));
            return;
        }

        const data: GetSearchHistoryResponseDto | null = res.data;
        if (!data) {
            setError("Nie udało się pobrać historii wyszukiwań (brak odpowiedzi).");
            return;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            setSearchHistory([]);
            return;
        }

        setSearchHistory(data.searchHistory);
    };

    const getLastSearches = async (limit: number): Promise<void> => {
        setLoading(true);
        setError(null);

        const res = await UserApi.getLastSearches({ limit });

        setLoading(false);

        if (res.error) {
            const parsed = tryParseJsonFromApiError<GetLastSearchesResponseDto>(res.error);
            if (parsed?.lastSearches) setLastSearches(parsed.lastSearches);
            setError(extractErrorMessage(res.error, "Nie udało się pobrać ostatnich wyszukiwań."));
            return;
        }

        const data: GetLastSearchesResponseDto | null = res.data;
        if (!data) {
            setError("Nie udało się pobrać ostatnich wyszukiwań (brak odpowiedzi).");
            return;
        }

        if (data.errorMessage) {
            setError(data.errorMessage);
            setLastSearches([]);
            return;
        }

        setLastSearches(data.lastSearches);
    };

    // Hydrate from localStorage on mount
    useEffect(() => {
        try {
            const storedJwt = localStorage.getItem(LS_JWT_KEY);
            const storedRememberMeToken = localStorage.getItem(LS_REMEMBER_KEY);

            const storedJwtSavedAtMs = safeNumber(localStorage.getItem(LS_JWT_SAVED_AT_MS_KEY));
            const storedJwtExpiresAtMs = safeNumber(localStorage.getItem(LS_JWT_EXPIRES_AT_MS_KEY));

            if (storedJwt) setJwt(storedJwt);
            if (storedRememberMeToken) setRememberMeToken(storedRememberMeToken);

            setJwtSavedAtMs(storedJwtSavedAtMs);
            setJwtExpiresAtMs(storedJwtExpiresAtMs ?? (storedJwt ? getJwtExpiresAtMs(storedJwt) : null));
        } catch {
            // ignore
        }
    }, []);

    // If we have rememberMeToken but no jwt, attempt silent token login
    useEffect(() => {
        if (jwt || !rememberMeToken) return;
        void tokenLogin();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [rememberMeToken, jwt]);

    const value: UserContextType = {
        jwt,
        rememberMeToken,
        isAuthenticated,

        jwtSavedAtMs,
        jwtExpiresAtMs,

        isJwtExpired,
        isJwtExpiringSoon,

        loading,
        error,

        userData,
        weights,
        preferences,
        preferencesIncomplete,

        searchHistory,
        lastSearches,

        standardRegister,
        standardLogin,
        tokenLogin,
        logout,
        deleteUser,

        refreshJwt,
        checkJwt,
        maybeRefreshJwt,

        fetchUserData,
        fetchWeights,
        fetchPreferences,

        updateData,
        updateWeights,
        updatePreferences,

        changePassword,
        checkPermission,

        insertSearchHistory,
        getSearchHistory,
        getLastSearches,
    };

    return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
};

export const useUser = (): UserContextType => {
    const context = useContext(UserContext);
    if (!context) throw new Error("useUser must be used within a UserProvider");
    return context;
};
