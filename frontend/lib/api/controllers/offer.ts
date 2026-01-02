import { apiClient } from "../apiClient";
import { offerListResponse } from "@/types/api/offer";
import { search } from "@/types/search/search";
import { ExtraFiltersState } from "@/store/SearchContext";
import { cleanObject } from "@/utils/others/apiObjectCleaner";

const PER_PAGE = -1

export type SearchOfferRequestDto = search & ExtraFiltersState;

export const OfferApi = {
  getOffersDatabase: async (
    search: search,
    filters: ExtraFiltersState
  ) => {
    const params = new URLSearchParams();
    params.set("perPage", PER_PAGE.toString());

    const body: SearchOfferRequestDto = {
      ...search,
      ...filters
    };

    const cleanedBody = cleanObject(body);
    return apiClient.post<offerListResponse>(`/offers/offers-database?${params.toString()}`, cleanedBody);
  },
};