import { useMemo } from "react";
import { matchOfferToFilters } from "@/utils/offerFilters/matchOfferToFilters";
import { sortData } from "@/utils/offerFilters/sortOffers";
import { FiltersState } from "@/app/(public)/list/page";
import { searchFilterKeywords } from '../types/list/searchFilterKeywords';
import { matchSearchFilterKeywords } from "@/utils/offerFilters/matchOfferToKeywords";
import { offer } from "@/types/list/Offer/offer";

export function useOfferList(
  filters: FiltersState,
  sort: string,
  offset: number,
  limit: number,
  searchFilterKeywords: searchFilterKeywords,
  offers?: offer[],
) {

  const allOffers = useMemo(() => {
  const filtered = offers?.filter((offer) =>
    matchOfferToFilters(offer, filters) &&
    matchSearchFilterKeywords(offer, searchFilterKeywords)
  );

  return sortData(filtered || [], sort);
}, [filters, sort, searchFilterKeywords, offers]);

  const paginatedOffers = useMemo(
    () => allOffers.slice(offset, offset + limit),
    [allOffers, offset, limit]
  );

  return {
    total: allOffers.length,
    filteredOffers: paginatedOffers,
  };
}
