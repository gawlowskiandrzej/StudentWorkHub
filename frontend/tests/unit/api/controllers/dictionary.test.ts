import { describe, it, expect, vi, beforeEach, afterEach, Mock } from 'vitest';
import { DictionaryApi } from '@/lib/api/controllers/dictionary';
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

describe('DictionaryApi', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  afterEach(() => {
    vi.restoreAllMocks();
  });

  describe('getStaticDictionaries', () => {
    const mockStaticDictionaries = {
      employmentType: ['full-time', 'part-time', 'contract', 'internship'],
      employmentSchedules: ['morning', 'afternoon', 'flexible', 'shift'],
      salaryPeriods: ['monthly', 'hourly', 'yearly', 'daily'],
    };

    it('should call correct endpoint', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockStaticDictionaries,
        error: null,
      });

      await DictionaryApi.getStaticDictionaries();

      expect(apiClient.get).toHaveBeenCalledWith(
        '/dictionary/searchview-dictionaries'
      );
    });

    it('should return employment types array', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockStaticDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.employmentType).toContain('full-time');
      expect(result.data?.employmentType).toContain('part-time');
    });

    it('should return employment schedules array', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockStaticDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.employmentSchedules).toContain('morning');
      expect(result.data?.employmentSchedules).toContain('flexible');
    });

    it('should return salary periods array', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockStaticDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.salaryPeriods).toContain('monthly');
      expect(result.data?.salaryPeriods).toContain('hourly');
    });

    it('should handle empty dictionaries', async () => {
      const emptyDictionaries = {
        employmentType: [],
        employmentSchedules: [],
        salaryPeriods: [],
      };

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: emptyDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.employmentType).toHaveLength(0);
      expect(result.data?.employmentSchedules).toHaveLength(0);
      expect(result.data?.salaryPeriods).toHaveLength(0);
    });

    it('should handle API error', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 500 Internal Server Error',
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data).toBeNull();
      expect(result.error).toContain('500');
    });

    it('should handle network timeout', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Request timeout',
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.error).toBe('Request timeout');
    });

    it('should return dictionaries with Polish values', async () => {
      const polishDictionaries = {
        employmentType: ['pełny etat', 'częściowy etat', 'umowa zlecenie'],
        employmentSchedules: ['praca zmianowa', 'elastyczny'],
        salaryPeriods: ['miesięcznie', 'godzinowo'],
      };

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: polishDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.employmentType).toContain('pełny etat');
    });
  });

  describe('getAllDictionaries', () => {
    const mockFullDictionaries = {
      leading_categories: [
        { id: 1, name: 'IT' },
        { id: 2, name: 'Marketing' },
        { id: 3, name: 'Finance' },
        { id: 4, name: 'HR' },
      ],
      employment_types: [
        { id: 1, name: 'full-time' },
        { id: 2, name: 'part-time' },
        { id: 3, name: 'contract' },
      ],
      languages: [
        { id: 1, name: 'English' },
        { id: 2, name: 'Polish' },
        { id: 3, name: 'German' },
        { id: 4, name: 'French' },
      ],
      language_levels: [
        { id: 1, name: 'A1' },
        { id: 2, name: 'A2' },
        { id: 3, name: 'B1' },
        { id: 4, name: 'B2' },
        { id: 5, name: 'C1' },
        { id: 6, name: 'C2' },
        { id: 7, name: 'Native' },
      ],
    };

    it('should call correct endpoint', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      await DictionaryApi.getAllDictionaries();

      expect(apiClient.get).toHaveBeenCalledWith(
        '/dictionary/all-dictionaries'
      );
    });

    it('should return leading categories with id and name', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data?.leading_categories).toHaveLength(4);
      expect(result.data?.leading_categories[0]).toHaveProperty('id');
      expect(result.data?.leading_categories[0]).toHaveProperty('name');
      expect(result.data?.leading_categories[0].name).toBe('IT');
    });

    it('should return employment types with id and name', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data?.employment_types).toHaveLength(3);
      expect(result.data?.employment_types.find(e => e.name === 'full-time')).toBeDefined();
    });

    it('should return languages with id and name', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data?.languages).toHaveLength(4);
      expect(result.data?.languages.map(l => l.name)).toContain('English');
      expect(result.data?.languages.map(l => l.name)).toContain('Polish');
    });

    it('should return language levels with id and name', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data?.language_levels).toHaveLength(7);
      expect(result.data?.language_levels.map(l => l.name)).toContain('B2');
      expect(result.data?.language_levels.map(l => l.name)).toContain('Native');
    });

    it('should handle empty dictionaries', async () => {
      const emptyFullDictionaries = {
        leading_categories: [],
        employment_types: [],
        languages: [],
        language_levels: [],
      };

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: emptyFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data?.leading_categories).toHaveLength(0);
      expect(result.data?.employment_types).toHaveLength(0);
      expect(result.data?.languages).toHaveLength(0);
      expect(result.data?.language_levels).toHaveLength(0);
    });

    it('should handle API error', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 503 Service Unavailable',
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data).toBeNull();
      expect(result.error).toContain('503');
    });

    it('should return dictionaries for profile creation', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data).toHaveProperty('leading_categories');
      expect(result.data).toHaveProperty('employment_types');
      expect(result.data).toHaveProperty('languages');
      expect(result.data).toHaveProperty('language_levels');
    });

    it('should return items with numeric IDs', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      result.data?.leading_categories.forEach(item => {
        expect(typeof item.id).toBe('number');
      });
      result.data?.employment_types.forEach(item => {
        expect(typeof item.id).toBe('number');
      });
    });

    it('should return items with string names', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: mockFullDictionaries,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      result.data?.languages.forEach(item => {
        expect(typeof item.name).toBe('string');
        expect(item.name.length).toBeGreaterThan(0);
      });
    });
  });

  describe('Integration Scenarios', () => {
    it('should support loading search view with static dictionaries', async () => {
      const staticDicts = {
        employmentType: ['full-time', 'part-time'],
        employmentSchedules: ['morning', 'flexible'],
        salaryPeriods: ['monthly', 'hourly'],
      };

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: staticDicts,
        error: null,
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data?.employmentType.length).toBeGreaterThan(0);
      expect(result.data?.salaryPeriods.includes('monthly')).toBe(true);
    });

    it('should support loading profile creation form with all dictionaries', async () => {
      const fullDicts = {
        leading_categories: [{ id: 1, name: 'IT' }],
        employment_types: [{ id: 1, name: 'full-time' }],
        languages: [{ id: 1, name: 'English' }],
        language_levels: [{ id: 1, name: 'B2' }],
      };

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: fullDicts,
        error: null,
      });

      const result = await DictionaryApi.getAllDictionaries();

      const categoryOptions = result.data?.leading_categories.map(c => ({
        value: c.id,
        label: c.name,
      }));
      expect(categoryOptions).toEqual([{ value: 1, label: 'IT' }]);
    });

    it('should handle sequential dictionary loading', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: { employmentType: ['full-time'], employmentSchedules: [], salaryPeriods: [] },
        error: null,
      });

      const staticResult = await DictionaryApi.getStaticDictionaries();
      expect(staticResult.data?.employmentType).toContain('full-time');

      (apiClient.get as Mock).mockResolvedValueOnce({
        data: { 
          leading_categories: [{ id: 1, name: 'IT' }],
          employment_types: [],
          languages: [],
          language_levels: [],
        },
        error: null,
      });

      const fullResult = await DictionaryApi.getAllDictionaries();
      expect(fullResult.data?.leading_categories).toHaveLength(1);

      expect(apiClient.get).toHaveBeenCalledTimes(2);
    });
  });

  describe('Error Handling', () => {
    it('should handle network failure for static dictionaries', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Network error',
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.data).toBeNull();
      expect(result.error).toBe('Network error');
    });

    it('should handle network failure for all dictionaries', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Network error',
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.data).toBeNull();
      expect(result.error).toBe('Network error');
    });

    it('should handle timeout for static dictionaries', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Request timeout',
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.error).toBe('Request timeout');
    });

    it('should handle timeout for all dictionaries', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Request timeout',
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.error).toBe('Request timeout');
    });

    it('should handle 404 not found', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 404 Not Found',
      });

      const result = await DictionaryApi.getStaticDictionaries();

      expect(result.error).toContain('404');
    });

    it('should handle 401 unauthorized', async () => {
      (apiClient.get as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 401 Unauthorized',
      });

      const result = await DictionaryApi.getAllDictionaries();

      expect(result.error).toContain('401');
    });
  });

  describe('Caching Behavior', () => {
    it('should call API each time getStaticDictionaries is called', async () => {
      const mockData = {
        employmentType: ['full-time'],
        employmentSchedules: [],
        salaryPeriods: [],
      };

      (apiClient.get as Mock).mockResolvedValue({
        data: mockData,
        error: null,
      });

      await DictionaryApi.getStaticDictionaries();
      await DictionaryApi.getStaticDictionaries();
      await DictionaryApi.getStaticDictionaries();

      expect(apiClient.get).toHaveBeenCalledTimes(3);
    });

    it('should call API each time getAllDictionaries is called', async () => {
      const mockData = {
        leading_categories: [],
        employment_types: [],
        languages: [],
        language_levels: [],
      };

      (apiClient.get as Mock).mockResolvedValue({
        data: mockData,
        error: null,
      });

      await DictionaryApi.getAllDictionaries();
      await DictionaryApi.getAllDictionaries();

      expect(apiClient.get).toHaveBeenCalledTimes(2);
    });
  });
});
