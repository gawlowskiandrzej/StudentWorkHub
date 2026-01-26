"use client";
import { createContext, useContext, useState, useEffect } from "react";
import { OfferApi } from "@/lib/api/controllers/offer";
import { useSearch } from "@/store/SearchContext";
import { offerListResponse, scrappingStatus } from "@/types/api/offer";
import { jobIds } from "@/types/api/jobIds";
import { offer } from "@/types/list/Offer/offer";

interface OffersContextType {
  offersResponse: offerListResponse | null;
  loading: boolean;
  error: string | null;
  activeJobsCount: number;
  fetchOffers: () => void;

  startScrapping: () => Promise<void>;
  scrapping: boolean;
}

// Tworzymy context
const OffersContext = createContext<OffersContextType | undefined>(undefined);

// Provider
export const OffersProvider = ({ children }: { children: React.ReactNode }) => {
  const { search, extraFilters, hasSearched } = useSearch();
  const [scrapping, setScrapping] = useState(false);
  const [scrapStatus, setScrapStatus] = useState<scrappingStatus | null>(null);
  const [jobIds, setJobIds] = useState<jobIds | null>(null);
  const [activeJobsCount, setActiveJobsCount] = useState<number>(4);
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
          dynamicFilter: response.data.dynamicFilter || {},
        });
      }
    } catch (e) {
      setError("Nie udało się pobrać ofert");
    } finally {
      setLoading(false);
    }
  };


  const startScrapping = async () => {
    if (scrapping) return;

    try {
      setScrapping(true);
      setError(null);

      const res = await OfferApi.createScrapper(search, extraFilters);
      setJobIds(res.data);
    } catch {
      setError("Błąd podczas scrapowania");
      setScrapping(false);
    }
  };

  useEffect(() => {
    if (!scrapping || !jobIds) return;

    let isCancelled = false;

    const fetchScrapStatus = async () => {
      try {
        const res = await OfferApi.getScrappedOffers(jobIds);
        if (!res.data || isCancelled) return;

        const { scrappingStatus, databaseOffersResponse } = res.data;
        setScrapStatus(scrappingStatus);
        const jobCount = scrappingStatus?.jobInfos?.length ?? 4;
        setActiveJobsCount(jobCount);

        if (scrappingStatus.scrappingDone) {
          setOffers(prev => {
            if (!prev) return databaseOffersResponse;

            const mergedItems = mergeOffers(
              prev.pagination.items,
              databaseOffersResponse.pagination.items
            );

            return {
              pagination: {
                ...prev.pagination,
                items: mergedItems,
                totalItems: mergedItems.length,
              },
              dynamicFilter: {
                ...prev.dynamicFilter,
                ...databaseOffersResponse.dynamicFilter,
              },
            };
          });

          setScrapping(false);
        } else {
          setTimeout(fetchScrapStatus, 2000);
        }
      } catch {
        if (!isCancelled) {
          setScrapping(false);
          setError("Błąd podczas pobierania statusu scrapowania");
        }
      }
    };

    fetchScrapStatus();

    return () => {
      isCancelled = true;
    };
  }, [scrapping, jobIds]);

  const mergeOffers = (oldOffers: offer[], newOffers: offer[]) => {
    const map = new Map<string, offer>();

    [...oldOffers, ...newOffers].forEach(offer => {
      map.set(offer.id.toString(), offer);
    });

    return Array.from(map.values());
  };

  useEffect(() => {
    if (!hasSearched) return;
    fetchOffers();
  }, [search, extraFilters, hasSearched]);

  return (
    <OffersContext.Provider value={{ offersResponse, loading, activeJobsCount, error, scrapping, fetchOffers, startScrapping }}>
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
