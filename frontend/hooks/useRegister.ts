"use client";

import { useState } from "react";
import { useUser } from "@/store/userContext";
import type { StandardRegisterRequestDto } from "@/types/api/usersDTO";
import type { RegisterState } from "@/types/register/registerState";

const initialState: RegisterState = {
  name: "",
  surname: "",
  email: "",
  password: "",
  consent: false,
};

const CONSENT_REQUIRED_ERROR = "CONSENT_REQUIRED";

export function useRegisterState() {
  const [state, setState] = useState<RegisterState>(initialState);
  const [hasSubmitted, setHasSubmitted] = useState(false);
  const [clientError, setClientError] = useState<string | null>(null);

  const { standardRegister, loading, error: serverError } = useUser();

  const update = <K extends keyof RegisterState>(key: K, value: RegisterState[K]) => {
    setHasSubmitted(false);
    setClientError(null);
    setState((prev) => ({ ...prev, [key]: value }));
  };

  const submit = async (): Promise<boolean> => {
    if (loading) return false;

    setHasSubmitted(true);
    setClientError(null);

    // Consent gate
    if (!state.consent) {
      setClientError(CONSENT_REQUIRED_ERROR);
      return false;
    }

    const dto: StandardRegisterRequestDto = {
      email: state.email.trim(),
      password: state.password,
      firstName: state.name.trim(),
      lastName: state.surname.trim(),
    };

    return standardRegister(dto);
  };

  return {
    state,
    update,
    submit,
    loading,
    error: hasSubmitted ? (clientError ?? serverError) : null,
    rawError: serverError,
  };
}
