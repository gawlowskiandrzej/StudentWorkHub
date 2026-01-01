import { useMemo } from "react";
import offersJson from "@/store/data/DummyOffers.json";
import { matchOfferToFilters } from "@/utils/offerFilters/matchOfferToFilters";
import { sortData } from "@/utils/offerFilters/sortOffers";
import { FiltersState } from "@/app/(public)/list/page";
import { searchFilterKeywords } from '../types/list/searchFilterKeywords';
import { matchSearchFilterKeywords } from "@/utils/offerFilters/matchOfferToKeywords";

export function useOfferList(
  filters: FiltersState,
  sort: string,
  offset: number,
  limit: number,
  searchFilterKeywords: searchFilterKeywords
) {

  const allOffers = useMemo(() => {
  const filtered = offersJson.pagination.items.filter((offer) =>
    matchOfferToFilters(offer, filters) &&
    matchSearchFilterKeywords(offer, searchFilterKeywords)
  );

  return sortData(filtered, sort);
}, [filters, sort, searchFilterKeywords]);

  const paginatedOffers = useMemo(
    () => allOffers.slice(offset, offset + limit),
    [allOffers, offset, limit]
  );

  return {
    total: allOffers.length,
    offers: paginatedOffers,
  };
}
