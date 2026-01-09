import { staticDictionariesDto, FullDictionariesDto } from "@/types/api/dictionary";
import { apiClient } from "../apiClient";

export const DictionaryApi = {
  getStaticDictionaries: async () => {
    return apiClient.get<staticDictionariesDto>(
      `/dictionary/searchview-dictionaries`
    );
  },

  /* 
    get all dictionaries for user profile creation 
  */
  getAllDictionaries: async () => {
    return apiClient.get<FullDictionariesDto>(
      `/dictionary/all-dictionaries`
    );
  },
};
