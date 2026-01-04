import { apiClient } from "../apiClient";
import { offerListResponse, scrappedOffersResponse } from "@/types/api/offer";
import { search } from "@/types/search/search";
import { ExtraFiltersState } from "@/store/SearchContext";
import { cleanObject } from "@/utils/others/apiObjectCleaner";
import { jobIds } from '../../../types/api/jobIds';

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
  getScrappedOffers: async (
    jobIds: jobIds,
  ) => {
    const cleanedBody = cleanObject(jobIds);
    const params = new URLSearchParams();
    params.set("usedAi", "true");
    params.set("addToDatabase", "true");
    return apiClient.post<scrappedOffersResponse>(`/offers/offers-scrapped?${params.toString()}`, cleanedBody);
  },
  createScrapper: async (
    search: search,
    filters: ExtraFiltersState
  ) => {
    const body: SearchOfferRequestDto = {
      ...search,
      ...filters
    };
    const cleanedBody = cleanObject(body);
    return apiClient.post<jobIds>(`/offers/create-scrappers`, cleanedBody);
  },
};