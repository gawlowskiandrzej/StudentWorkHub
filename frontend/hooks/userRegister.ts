"use client";
import { RegisterState } from "@/types/register/registerState";
import { useState } from "react";

const initialState: RegisterState = {
  name: "",
  surname: "",
  email: "",
  password: "",
  consent: false,
};

export function useRegisterState() {
  const [state, setState] = useState<RegisterState>(initialState);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const update = <K extends keyof RegisterState>(
    key: K,
    value: RegisterState[K]
  ) => {
    setState((prev) => ({ ...prev, [key]: value }));
  };

  const submit = async () => {
    setLoading(true);
    setError(null);

    try {
      console.log("REGISTER DATA:", state);
    } catch (e) {
      setError("REGISTER_FAILED");
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
