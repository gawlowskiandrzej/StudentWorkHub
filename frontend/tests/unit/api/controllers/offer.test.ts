import { describe, it, expect, vi, beforeEach, afterEach, Mock } from 'vitest';
import { OfferApi } from '@/lib/api/controllers/offer';
import { apiClient } from '@/lib/api/apiClient';

// mock
vi.mock('@/lib/api/apiClient', () => ({
  apiClient: {
    get: vi.fn(),
    post: vi.fn(),
    put: vi.fn(),
    delete: vi.fn(),
  },
}));

describe('OfferApi', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  afterEach(() => {
    vi.restoreAllMocks();
  });

  describe('getOffersDatabase', () => {
    const mockSearchParams = {
      keywords: 'developer',
      localization: 'Warsaw',
      category: 'IT',
    };

    const mockFilters = {
      salaryFrom: 5000,
      salaryTo: 15000,
      employmentType: 'full-time',
    };

    const mockPaginationResponse = {
      pagination: {
        items: [
          { id: 1, title: 'Senior Developer', salary: { from: 10000, to: 15000 } },
          { id: 2, title: 'Junior Developer', salary: { from: 5000, to: 8000 } },
        ],
        page: 0,
        pageSize: -1,
        totalItems: 2,
        totalPages: 1,
        isLastPage: true,
      },
      dynamicFilter: {
        languages: [],
        experienceLevels: ['junior', 'senior'],
        educationNames: [],
      },
    };

    it('should call correct endpoint with search params and filters', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      await OfferApi.getOffersDatabase(mockSearchParams as any, mockFilters as any);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.stringContaining('/offers/offers-database'),
        expect.objectContaining({
          keywords: 'developer',
          localization: 'Warsaw',
          salaryFrom: 5000,
          salaryTo: 15000,
        })
      );
    });

    it('should include perPage query parameter', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.stringContaining('perPage=-1'),
        expect.any(Object)
      );
    });

    it('should return paginated offers on success', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.data?.pagination.items).toHaveLength(2);
      expect(result.data?.pagination.totalItems).toBe(2);
    });

    it('should return dynamic filters with offers', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.data?.dynamicFilter).toBeDefined();
      expect(result.data?.dynamicFilter.experienceLevels).toContain('junior');
    });

    it('should handle empty search results', async () => {
      const emptyResponse = {
        pagination: {
          items: [],
          page: 0,
          pageSize: -1,
          totalItems: 0,
          totalPages: 0,
          isLastPage: true,
        },
        dynamicFilter: {
          languages: [],
          experienceLevels: [],
          educationNames: [],
        },
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: emptyResponse,
        error: null,
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.data?.pagination.items).toHaveLength(0);
      expect(result.data?.pagination.totalItems).toBe(0);
    });

    it('should handle API error', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 500 Internal Server Error',
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.data).toBeNull();
      expect(result.error).toContain('500');
    });

    it('should clean empty values from request body', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      const searchWithEmpty = {
        keywords: 'test',
        category: '',
        localization: null,
      };

      await OfferApi.getOffersDatabase(searchWithEmpty as any, {} as any);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          keywords: 'test',
        })
      );
    });

    it('should combine search and filter objects', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockPaginationResponse,
        error: null,
      });

      await OfferApi.getOffersDatabase(
        { keywords: 'python' } as any,
        { salaryFrom: 8000, isRemote: true } as any
      );

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          keywords: 'python',
          salaryFrom: 8000,
          isRemote: true,
        })
      );
    });

    it('should surface 401 Unauthorized error', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 401 Unauthorized',
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.data).toBeNull();
      expect(result.error).toContain('401');
    });

    it('should surface 422 validation errors', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 422 Unprocessable Entity',
      });

      const result = await OfferApi.getOffersDatabase({ salaryFrom: -1 } as any, {} as any);

      expect(result.data).toBeNull();
      expect(result.error).toContain('422');
    });
  });

  describe('getScrappedOffers', () => {
    const mockJobIds = {
      jobIds: ['job-1', 'job-2', 'job-3'],
    };

    const mockScrappedResponse = {
      databaseOffersResponse: {
        pagination: {
          items: [{ id: 1, title: 'New Offer' }],
          page: 0,
          pageSize: -1,
          totalItems: 1,
          totalPages: 1,
          isLastPage: true,
        },
        dynamicFilter: {
          languages: [],
          experienceLevels: [],
          educationNames: [],
        },
      },
      scrappingStatus: {
        jobInfos: [
          { jobId: 'job-1', status: 'completed' },
          { jobId: 'job-2', status: 'completed' },
        ],
        scrappingDone: true,
        scrappingDuration: '5.23',
      },
    };

    it('should call correct endpoint with job IDs', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockScrappedResponse,
        error: null,
      });

      await OfferApi.getScrappedOffers(mockJobIds);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.stringContaining('/offers/offers-scrapped'),
        expect.objectContaining({
          jobIds: ['job-1', 'job-2', 'job-3'],
        })
      );
    });

    it('should include usedAi and addToDatabase query params', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockScrappedResponse,
        error: null,
      });

      await OfferApi.getScrappedOffers(mockJobIds);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.stringContaining('usedAi=true'),
        expect.any(Object)
      );
      expect(apiClient.post).toHaveBeenCalledWith(
        expect.stringContaining('addToDatabase=true'),
        expect.any(Object)
      );
    });

    it('should return scrapped offers and status on success', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockScrappedResponse,
        error: null,
      });

      const result = await OfferApi.getScrappedOffers(mockJobIds);

      expect(result.data?.databaseOffersResponse.pagination.items).toHaveLength(1);
      expect(result.data?.scrappingStatus.scrappingDone).toBe(true);
    });

    it('should return scrapping duration', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockScrappedResponse,
        error: null,
      });

      const result = await OfferApi.getScrappedOffers(mockJobIds);

      expect(result.data?.scrappingStatus.scrappingDuration).toBe('5.23');
    });

    it('should handle pending jobs status', async () => {
      const pendingResponse = {
        scrappingStatus: {
          jobInfos: [
            { jobId: 'job-1', status: 'pending' },
            { jobId: 'job-2', status: 'processing' },
          ],
          scrappingDone: false,
        },
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: pendingResponse,
        error: null,
      });

      const result = await OfferApi.getScrappedOffers(mockJobIds);

      expect(result.data?.scrappingStatus.scrappingDone).toBe(false);
    });

    it('should handle not found jobs', async () => {
      const notFoundResponse = {
        scrappingStatus: {
          jobInfos: [
            { jobId: 'job-unknown', status: 'notfound' },
          ],
          scrappingDone: false,
        },
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: notFoundResponse,
        error: null,
      });

      const result = await OfferApi.getScrappedOffers({ jobIds: ['job-unknown'] });

      const jobInfo = result.data?.scrappingStatus.jobInfos[0];
      expect(jobInfo?.status).toBe('notfound');
    });

    it('should handle empty job IDs array', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { scrappingStatus: { jobInfos: [], scrappingDone: true } },
        error: null,
      });

      const result = await OfferApi.getScrappedOffers({ jobIds: [] });

      expect(result.data?.scrappingStatus.jobInfos).toHaveLength(0);
    });

    it('should handle API error during scrapping', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 400 Bad Request',
      });

      const result = await OfferApi.getScrappedOffers(mockJobIds);

      expect(result.data).toBeNull();
      expect(result.error).toContain('400');
    });
  });

  describe('createScrapper', () => {
    const mockSearch = {
      keywords: 'react developer',
      localization: 'Krakow',
    };

    const mockFilters = {
      salaryFrom: 10000,
      employmentType: 'full-time',
    };

    const mockJobIdsResponse = {
      jobIds: ['job-abc-123', 'job-def-456'],
    };

    it('should call correct endpoint with search and filters', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockJobIdsResponse,
        error: null,
      });

      await OfferApi.createScrapper(mockSearch as any, mockFilters as any);

      expect(apiClient.post).toHaveBeenCalledWith(
        '/offers/create-scrappers',
        expect.objectContaining({
          keywords: 'react developer',
          localization: 'Krakow',
          salaryFrom: 10000,
          employmentType: 'full-time',
        })
      );
    });

    it('should return job IDs on success', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockJobIdsResponse,
        error: null,
      });

      const result = await OfferApi.createScrapper({} as any, {} as any);

      expect(result.data?.jobIds).toHaveLength(2);
      expect(result.data?.jobIds).toContain('job-abc-123');
    });

    it('should handle multiple job IDs for different portals', async () => {
      const multiPortalResponse = {
        jobIds: ['job-portal1-1', 'job-portal2-1', 'job-portal3-1'],
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: multiPortalResponse,
        error: null,
      });

      const result = await OfferApi.createScrapper({} as any, {} as any);

      expect(result.data?.jobIds).toHaveLength(3);
    });

    it('should clean empty values from request body', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockJobIdsResponse,
        error: null,
      });

      await OfferApi.createScrapper(
        { keywords: 'test', localization: '' } as any,
        { salaryFrom: undefined, isRemote: true } as any
      );

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          keywords: 'test',
          isRemote: true,
        })
      );
    });

    it('should handle API error during scrapper creation', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 503 Service Unavailable',
      });

      const result = await OfferApi.createScrapper({} as any, {} as any);

      expect(result.data).toBeNull();
      expect(result.error).toContain('503');
    });

    it('should handle empty search parameters', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: mockJobIdsResponse,
        error: null,
      });

      const result = await OfferApi.createScrapper({} as any, {} as any);

      expect(result.data).toBeDefined();
      expect(apiClient.post).toHaveBeenCalled();
    });
  });


  describe('Integration Scenarios', () => {
    it('should support full scraping workflow: create -> poll -> get results', async () => {
      const jobIds = { jobIds: ['job-1', 'job-2'] };
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: jobIds,
        error: null,
      });

      const createResult = await OfferApi.createScrapper({} as any, {} as any);
      expect(createResult.data?.jobIds).toHaveLength(2);

      const pendingStatus = {
        scrappingStatus: {
          jobInfos: [{ jobId: 'job-1', status: 'processing' }],
          scrappingDone: false,
        },
      };
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: pendingStatus,
        error: null,
      });

      const pollResult = await OfferApi.getScrappedOffers(jobIds);
      expect(pollResult.data?.scrappingStatus.scrappingDone).toBe(false);

      const completedResults = {
        databaseOffersResponse: {
          pagination: { items: [{ id: 1 }], totalItems: 1 },
          dynamicFilter: {},
        },
        scrappingStatus: { scrappingDone: true, scrappingDuration: '10.5' },
      };
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: completedResults,
        error: null,
      });

      const finalResult = await OfferApi.getScrappedOffers(jobIds);
      expect(finalResult.data?.scrappingStatus.scrappingDone).toBe(true);
    });

    it('should handle search with all filter types', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { pagination: { items: [] }, dynamicFilter: {} },
        error: null,
      });

      const fullSearch = {
        keywords: 'senior developer',
        category: 'IT',
        localization: 'Remote',
      };

      const fullFilters = {
        salaryFrom: 15000,
        salaryTo: 25000,
        employmentType: 'B2B',
        isRemote: true,
        isHybrid: false,
      };

      await OfferApi.getOffersDatabase(fullSearch as any, fullFilters as any);

      expect(apiClient.post).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          keywords: 'senior developer',
          salaryFrom: 15000,
          salaryTo: 25000,
          isRemote: true,
        })
      );
    });
  });

  describe('Error Handling', () => {
    it('should handle network timeout', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Request timeout',
      });

      const result = await OfferApi.getOffersDatabase({} as any, {} as any);

      expect(result.error).toBe('Request timeout');
    });

    it('should handle network failure', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Network error',
      });

      const result = await OfferApi.createScrapper({} as any, {} as any);

      expect(result.error).toBe('Network error');
    });

    it('should handle malformed response', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Invalid JSON',
      });

      const result = await OfferApi.getScrappedOffers({ jobIds: ['test'] });

      expect(result.error).toBe('Invalid JSON');
    });
  });
});
