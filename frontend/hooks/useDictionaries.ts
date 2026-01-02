// /hooks/useDictionaries.ts
import { useState, useEffect } from "react";
import { DictionaryApi } from "@/lib/api/controllers/dictionary";
import { staticDictionariesDto } from "@/types/api/dictionary";

export function useDictionaries() {
  const [dictionaries, setDictionaries] = useState<staticDictionariesDto>();
  const [loading, setLoading] = useState(false);
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
