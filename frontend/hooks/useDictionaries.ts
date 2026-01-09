// /hooks/useDictionaries.ts
import { useState, useEffect } from "react";
import { DictionaryApi } from "@/lib/api/controllers/dictionary";
import { FullDictionariesDto, staticDictionariesDto } from "@/types/api/dictionary";

// Hook dla profile-creation (languages, language_levels, etc.)
export function useProfileCreationDictionaries() {
  const [fullDictionaries, setFullDictionaries] = useState<FullDictionariesDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function fetchDictionaries() {
      setLoading(true);
      setError(null);

      const { data, error } = await DictionaryApi.getAllDictionaries();

      if (error) setError(error);
      if (data) setFullDictionaries(data);

      setLoading(false);
    }

    fetchDictionaries();
  }, []);

  return { fullDictionaries, loading, error };
}

// Hook dla search page (employment types, schedules, etc.)
export function useSimpleDictionaries() {
  const [dictionaries, setDictionaries] = useState<staticDictionariesDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function fetchDictionaries() {
      setLoading(true);
      setError(null);

      const { data, error } = await DictionaryApi.getStaticDictionaries();

      if (error) setError(error);
      if (data) setDictionaries(data);

      setLoading(false);
    }

    fetchDictionaries();
  }, []);

  return { dictionaries, loading, error };
}
