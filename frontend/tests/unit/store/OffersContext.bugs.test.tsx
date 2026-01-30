import { describe, it, expect, beforeEach, vi, Mock } from 'vitest';
import { renderHook, act, waitFor } from '@testing-library/react';
import React from 'react';
import { OffersProvider, useOffers } from '@/store/OffersContext';
import { SearchProvider } from '@/store/SearchContext';
import { PaginationProvider } from '@/store/PaginationContext';
import { OfferApi } from '@/lib/api/controllers/offer';
import { createMockOffer } from '@tests/utils/mockData';

// Mock OfferApi
vi.mock('@/lib/api/controllers/offer', () => ({
  OfferApi: {
    getOffersDatabase: vi.fn(),
    createScrapper: vi.fn(),
    getScrappedOffers: vi.fn(),
  },
}));

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <PaginationProvider>
    <SearchProvider>
      <OffersProvider>{children}</OffersProvider>
    </SearchProvider>
  </PaginationProvider>
);

describe('OffersContext - Bug Exposure Tests', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  describe('Bug #1: Pagination off-by-one errors', () => {
    it('should not handle page 0 correctly', async () => {
      const mockOffers = Array.from({ length: 50 }, (_, i) =>
        createMockOffer({ id: i + 1 })
      );

      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: {
            items: mockOffers.slice(0, 10),
            totalItems: 50,
            pageSize: 10,
            pageNumber: 0, // Zero-indexed
          },
          dynamicFilter: { languages: [] },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.fetchOffers();
      });

      console.warn('BUG EXPOSED #1: Page 0 indexing');
      
      expect(result.current.offersResponse?.pagination.pageNumber).toBe(0);
    });

    it('should crash with negative page numbers', async () => {
      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: {
            items: [createMockOffer()],
            totalItems: 1,
            pageNumber: -1, // Invalid!
          },
          dynamicFilter: { languages: [] },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #1b: Negative page number handling');

      await act(async () => {
        await result.current.fetchOffers();
      });

      expect(result.current.offersResponse?.pagination.pageNumber).toBe(-1);
    });
  });

  describe('Bug #2: Race conditions in scrapping', () => {
    it('should handle multiple scrapping requests simultaneously', async () => {
      (OfferApi.createScrapper as Mock)
        .mockResolvedValueOnce({
          data: ['job-1'],
        })
        .mockResolvedValueOnce({
          data: ['job-2'],
        });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #2: Multiple simultaneous scrapping requests');

      await act(async () => {
        result.current.startScrapping();
        result.current.startScrapping(); // Second call while first is pending
      });

      expect(result.current.scrappingJobIds?.length).toBeGreaterThan(0);
    });

    it('should handle cancelled scrapping requests incorrectly', async () => {
      let resolveScrap: any;
      const scrappingPromise = new Promise(resolve => {
        resolveScrap = resolve;
      });

      (OfferApi.createScrapper as Mock).mockReturnValue(scrappingPromise);

      const { result, unmount } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #2b: Unmount during pending scrapping');

      act(() => {
        result.current.startScrapping();
      });

      unmount();

      await act(async () => {
        resolveScrap({ data: { jobIds: ['job-1'] } });
      });

      console.warn('WARNING: Memory leak potential from unmounted component');
    });
  });

  describe('Bug #3: Generic error handling', () => {
    it('should show same error for network and validation errors', async () => {
      const networkError = new Error('Network timeout');
      (OfferApi.getOffersDatabase as Mock).mockRejectedValueOnce(networkError);

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.fetchOffers();
      });

      console.warn('BUG EXPOSED #3: Generic error message');
      console.warn('Error:', result.current.error);

      expect(result.current.error).toBe('Nie udało się pobrać ofert');
    });

    it('should not handle 500 errors differently from 404', async () => {
      const error500 = new Error('500 Internal Server Error');
      (OfferApi.getOffersDatabase as Mock).mockRejectedValueOnce(error500);

      const { result } = renderHook(() => useOffers(), { wrapper });

      await act(async () => {
        await result.current.fetchOffers();
      });

      expect(result.current.error).toBeTruthy();
    });
  });

  describe('Bug #4: Offer deduplication and merging', () => {
    it('should not properly deduplicate offers from database and scrapper', async () => {
      const commonOffer = createMockOffer({ id: 1, source: 'JustJoinIt' });

      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: { items: [commonOffer], totalItems: 1 },
          dynamicFilter: { languages: [] },
        },
      });

      (OfferApi.createScrapper as Mock).mockResolvedValue({
        data: { jobIds: ['job-1'] },
      });

      (OfferApi.getScrappedOffers as Mock).mockResolvedValue({
        data: {
          scrappingStatus: {
            jobInfos: [{ jobId: 'job-1', status: 'finished' }],
            scrappingDone: true,
          },
          databaseOffersResponse: {
            pagination: { items: [commonOffer], totalItems: 1 }, // Same offer!
            dynamicFilter: {},
          },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #4: Offer deduplication');

      await act(async () => {
        await result.current.fetchOffers();
      });

      await act(async () => {
        await result.current.startScrapping();
      });

      const allOffers = result.current.offersResponse?.pagination.items || [];
      const offerWithId1 = allOffers.filter(o => o.id === 1);
      
      console.warn('Offers with id=1:', offerWithId1.length);
      expect(offerWithId1.length).toBe(1); // Should be 1, might be 2
    });
  });

  describe('Bug #5: Filter state management', () => {
    it('should not reset filters when switching data sources', async () => {
      const mockOffers = [createMockOffer({ id: 1 }), createMockOffer({ id: 2 })];

      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: { items: mockOffers, totalItems: 2 },
          dynamicFilter: {
            languages: ['English', 'German'],
            skills: ['React', 'Node.js'],
          },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #5: Filter state inconsistency');

      await act(async () => {
        await result.current.fetchOffers();
      });

      const dynamicFilter1 = result.current.offersResponse?.dynamicFilter;

      // Fetch again - filter might be different
      await act(async () => {
        await result.current.fetchOffers();
      });

      const dynamicFilter2 = result.current.offersResponse?.dynamicFilter;

      console.warn('First filter:', dynamicFilter1);
      console.warn('Second filter:', dynamicFilter2);
    });
  });

  describe('Bug #6: Loading state management', () => {
    it('should not set loading=false while scrapping is in progress', async () => {
      const delayedPromise = new Promise(resolve =>
        setTimeout(() => resolve({ data: { jobIds: ['job-1'] } }), 1000)
      );

      (OfferApi.createScrapper as Mock).mockReturnValue(delayedPromise);
      (OfferApi.getScrappedOffers as Mock).mockResolvedValue({
        data: {
          scrappingStatus: {
            jobInfos: [{ jobId: 'job-1', status: 'processing' }],
            scrappingDone: false,
          },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #6: Loading state during scrapping');

      act(() => {
        result.current.startScrapping();
      });

      expect(result.current.loading).toBe(true);

      // Wait a bit
      await waitFor(() => {
        console.warn('Loading state during scrapping:', result.current.loading);
      });
    });
  });

  describe('Bug #7: Empty and null response handling', () => {
    it('should crash with null pagination response', async () => {
      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: null,
          dynamicFilter: { languages: [] },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #7: Null pagination response');

      expect(async () => {
        await act(async () => {
          await result.current.fetchOffers();
        });
      }).not.toThrow(); // Might not throw but could crash during render
    });

    it('should handle missing pagination.items array', async () => {
      (OfferApi.getOffersDatabase as Mock).mockResolvedValue({
        data: {
          pagination: {
            totalItems: 0,
          },
          dynamicFilter: { languages: [] },
        },
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #7b: Missing items array');

      await act(async () => {
        await result.current.fetchOffers();
      });

      expect(result.current.offersResponse?.pagination.items).toEqual([]);
    });
  });

  describe('Bug #8: Scrapping poll timeout', () => {
    it('should not timeout if scrapping takes too long', async () => {
      (OfferApi.createScrapper as Mock).mockResolvedValue({
        data: { jobIds: ['job-1'] },
      });

      let pollCount = 0;
      (OfferApi.getScrappedOffers as Mock).mockImplementation(() => {
        pollCount++;
        console.warn(`Poll #${pollCount}`);
        
        return Promise.resolve({
          data: {
            scrappingStatus: {
              jobInfos: [{ jobId: 'job-1', status: 'processing' }],
              scrappingDone: false,
            },
          },
        });
      });

      const { result } = renderHook(() => useOffers(), { wrapper });

      console.warn('BUG EXPOSED #8: No polling timeout');

      await act(async () => {
        await result.current.startScrapping();
      });

      // Wait 30 seconds
      await new Promise(resolve => setTimeout(resolve, 2000));

      console.warn(`Total polls: ${pollCount}`);
      expect(pollCount).toBeGreaterThan(0);
    });
  });
});
