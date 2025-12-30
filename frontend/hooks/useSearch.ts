import { useState, useMemo } from "react";
import { offer } from "../types/list/Offer/offer";
import { filterOffers } from "../utils/offerFilters/searchByKeyword";

export function useOfferSearch(offers: offer[]) {
  const [keyword, setKeyword] = useState("");

  const filteredOffers = useMemo(() => {
    if (!keyword) return offers;
    return filterOffers(offers, keyword);
  }, [offers, keyword]);

  return { keyword, setKeyword, filteredOffers };
}