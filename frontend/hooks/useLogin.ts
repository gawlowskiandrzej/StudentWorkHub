"use client";
import { LoginState } from "@/types/login/loginState";
import { useState } from "react";

export const initialState: LoginState = {
  email: "",
  password: "",
  rememberMe: false,
};

export function useLoginState() {
  const [state, setState] = useState<LoginState>(initialState);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const update = <K extends keyof LoginState>(
    key: K,
    value: LoginState[K]
  ) => {
    setState((prev) => ({ ...prev, [key]: value }));
  };

  const submit = async () => {
    setLoading(true);
    setError(null);

    try {
      console.log("LOGIN DATA:", state);
    } catch (e) {
      setError("LOGIN_FAILED");
    } finally {
      setLoading(false);
    }
  };

  return {
    state,
    update,
    submit,
    loading,
    error,
  };
}