import { staticDictionariesDto } from "@/types/api/dictionary";
import { apiClient } from "../apiClient";

export const DictionaryApi = {
  getStaticDictionaries: async () => {
    return apiClient.get<staticDictionariesDto>(
      `/dictionary/searchview-dictionaries`
    );
  },
};
