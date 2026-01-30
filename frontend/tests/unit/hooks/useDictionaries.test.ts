import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import { renderHook, waitFor } from '@testing-library/react';
import { useProfileCreationDictionaries, useSimpleDictionaries } from '@/hooks/useDictionaries';

vi.mock('@/lib/api/controllers/dictionary', () => ({
  DictionaryApi: {
    getAllDictionaries: vi.fn(),
    getStaticDictionaries: vi.fn(),
  },
}));

import { DictionaryApi } from '@/lib/api/controllers/dictionary';

describe('useDictionaries Hooks', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  afterEach(() => {
    vi.resetAllMocks();
  });

  describe('useProfileCreationDictionaries', () => {
    it('should start with loading state', () => {
      vi.mocked(DictionaryApi.getAllDictionaries).mockImplementation(
        () => new Promise(() => {})
      );

      const { result } = renderHook(() => useProfileCreationDictionaries());

      expect(result.current.loading).toBe(true);
      expect(result.current.fullDictionaries).toBeNull();
      expect(result.current.error).toBeNull();
    });

    it('should fetch and return dictionaries on success', async () => {
      const mockData = {
        languages: [
          { id: 1, name: 'English' },
          { id: 2, name: 'Polish' },
        ],
        languageLevels: [
          { id: 1, name: 'A1' },
          { id: 2, name: 'B2' },
        ],
        skills: [
          { id: 1, name: 'TypeScript' },
          { id: 2, name: 'React' },
        ],
      };

      vi.mocked(DictionaryApi.getAllDictionaries).mockResolvedValue({
        data: mockData,
        error: null,
      });

      const { result } = renderHook(() => useProfileCreationDictionaries());

      await waitFor(() => {
        expect(result.current.loading).toBe(false);
      });

      expect(result.current.fullDictionaries).toEqual(mockData);
      expect(result.current.error).toBeNull();
    });

    it('should handle API error', async () => {
      vi.mocked(DictionaryApi.getAllDictionaries).mockResolvedValue({
        data: null,
        error: 'Failed to fetch dictionaries',
      });

      const { result } = renderHook(() => useProfileCreationDictionaries());

      await waitFor(() => {
        expect(result.current.loading).toBe(false);
      });

      expect(result.current.fullDictionaries).toBeNull();
      expect(result.current.error).toBe('Failed to fetch dictionaries');
    });

    it('should call API only once on mount', async () => {
      vi.mocked(DictionaryApi.getAllDictionaries).mockResolvedValue({
        data: { languages: [] },
        error: null,
      });

      const { rerender } = renderHook(() => useProfileCreationDictionaries());

      await waitFor(() => {
        expect(DictionaryApi.getAllDictionaries).toHaveBeenCalledTimes(1);
      });

      rerender();

      expect(DictionaryApi.getAllDictionaries).toHaveBeenCalledTimes(1);
    });
  });

  describe('useSimpleDictionaries', () => {
    it('should start with loading state', () => {
      vi.mocked(DictionaryApi.getStaticDictionaries).mockImplementation(
        () => new Promise(() => {})
      );

      const { result } = renderHook(() => useSimpleDictionaries());

      expect(result.current.loading).toBe(true);
      expect(result.current.dictionaries).toBeNull();
      expect(result.current.error).toBeNull();
    });

    it('should fetch and return static dictionaries on success', async () => {
      const mockData = {
        employmentTypes: [
          { id: 1, name: 'B2B' },
          { id: 2, name: 'UoP' },
        ],
        schedules: [
          { id: 1, name: 'Full-time' },
          { id: 2, name: 'Part-time' },
        ],
      };

      vi.mocked(DictionaryApi.getStaticDictionaries).mockResolvedValue({
        data: mockData,
        error: null,
      });

      const { result } = renderHook(() => useSimpleDictionaries());

      await waitFor(() => {
        expect(result.current.loading).toBe(false);
      });

      expect(result.current.dictionaries).toEqual(mockData);
      expect(result.current.error).toBeNull();
    });

    it('should handle API error', async () => {
      vi.mocked(DictionaryApi.getStaticDictionaries).mockResolvedValue({
        data: null,
        error: 'Network error',
      });

      const { result } = renderHook(() => useSimpleDictionaries());

      await waitFor(() => {
        expect(result.current.loading).toBe(false);
      });

      expect(result.current.dictionaries).toBeNull();
      expect(result.current.error).toBe('Network error');
    });

    it('should call API only once on mount', async () => {
      vi.mocked(DictionaryApi.getStaticDictionaries).mockResolvedValue({
        data: { employmentTypes: [] },
        error: null,
      });

      const { rerender } = renderHook(() => useSimpleDictionaries());

      await waitFor(() => {
        expect(DictionaryApi.getStaticDictionaries).toHaveBeenCalledTimes(1);
      });

      rerender();

      expect(DictionaryApi.getStaticDictionaries).toHaveBeenCalledTimes(1);
    });
  });

  describe('Hook Independence', () => {
    it('should fetch independently when both hooks are used', async () => {
      vi.mocked(DictionaryApi.getAllDictionaries).mockResolvedValue({
        data: { languages: [] },
        error: null,
      });
      vi.mocked(DictionaryApi.getStaticDictionaries).mockResolvedValue({
        data: { employmentTypes: [] },
        error: null,
      });

      const { result: result1 } = renderHook(() => useProfileCreationDictionaries());
      const { result: result2 } = renderHook(() => useSimpleDictionaries());

      await waitFor(() => {
        expect(result1.current.loading).toBe(false);
        expect(result2.current.loading).toBe(false);
      });

      expect(DictionaryApi.getAllDictionaries).toHaveBeenCalledTimes(1);
      expect(DictionaryApi.getStaticDictionaries).toHaveBeenCalledTimes(1);
    });
  });
});
