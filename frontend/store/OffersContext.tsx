"use client";
import { createContext, useContext, useState, useEffect } from "react";
import { OfferApi } from "@/lib/api/controllers/offer";
import { useSearch } from "@/store/SearchContext";
import { offerListResponse } from "@/types/api/offer";

interface OffersContextType {
  offersResponse: offerListResponse | null;
  loading: boolean;
  error: string | null;
  fetchOffers: () => void;
}

// Tworzymy context
const OffersContext = createContext<OffersContextType | undefined>(undefined);

// Provider
export const OffersProvider = ({ children }: { children: React.ReactNode }) => {
  const { search, extraFilters } = useSearch();

  const [offersResponse, setOffers] = useState<offerListResponse | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchOffers = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await OfferApi.getOffersDatabase(search, extraFilters);

      if (response.data) {
        setOffers({
          pagination: {
            ...response.data.pagination,
            items: response.data.pagination.items || [],
          },
          dynamicFilters: response.data.dynamicFilters || {},
        });
      }
    } catch (e) {
      setError("Nie udało się pobrać ofert");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOffers();
  }, [search, extraFilters]);

  return (
    <OffersContext.Provider value={{ offersResponse, loading, error, fetchOffers }}>
      {children}
    </OffersContext.Provider>
  );
};

// Hook do użycia w komponentach
export const useOffers = () => {
  const context = useContext(OffersContext);
  if (!context) {
    throw new Error("useOffers must be used within an OffersProvider");
  }
  return context;
};
