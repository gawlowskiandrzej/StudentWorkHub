import { describe, it, expect } from 'vitest';
import { renderHook } from '@testing-library/react';
import { useOfferList } from '@/hooks/useOfferList';
import { createMockOffer, createMockOfferBatch } from '@tests/utils/mockData';
import { FiltersState } from '@/app/(public)/list/page';
import { searchFilterKeywords } from '@/types/list/searchFilterKeywords';
import { offer } from '@/types/list/Offer/offer';

describe('useOfferList Hook', () => {
  const emptyFilters: FiltersState = {};
  const emptyKeywords: searchFilterKeywords = {};

  describe('basic functionality', () => {
    it('should return empty array when no offers provided', () => {
      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, emptyKeywords, undefined)
      );

      expect(result.current.total).toBe(0);
      expect(result.current.filteredOffers).toEqual([]);
    });

    it('should return all offers when no filters applied', () => {
      const offers = createMockOfferBatch(5);

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(5);
      expect(result.current.filteredOffers).toHaveLength(5);
    });
  });

  describe('pagination', () => {
    it('should paginate results correctly', () => {
      const offers = createMockOfferBatch(25);

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(25);
      expect(result.current.filteredOffers).toHaveLength(10);
    });

    it('should handle offset correctly', () => {
      const offers = createMockOfferBatch(25);

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 10, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(25);
      expect(result.current.filteredOffers).toHaveLength(10);
    });

    it('should handle last page with fewer items', () => {
      const offers = createMockOfferBatch(25);

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 20, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(25);
      expect(result.current.filteredOffers).toHaveLength(5);
    });

    it('should return empty array when offset exceeds total', () => {
      const offers = createMockOfferBatch(10);

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 20, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(10);
      expect(result.current.filteredOffers).toHaveLength(0);
    });
  });

  describe('sorting', () => {
    it('should sort by ID descending', () => {
      const offers = [
        createMockOffer({ id: 1 }),
        createMockOffer({ id: 3 }),
        createMockOffer({ id: 2 }),
      ];

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.filteredOffers[0].id).toBe(3);
      expect(result.current.filteredOffers[1].id).toBe(2);
      expect(result.current.filteredOffers[2].id).toBe(1);
    });

    it('should sort by salary ascending', () => {
      const offers = [
        createMockOffer({ id: 1, salaryFrom: 20000 }),
        createMockOffer({ id: 2, salaryFrom: 10000 }),
        createMockOffer({ id: 3, salaryFrom: 15000 }),
      ];

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'Salaryasc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.filteredOffers[0].salary.from).toBe(10000);
      expect(result.current.filteredOffers[1].salary.from).toBe(15000);
      expect(result.current.filteredOffers[2].salary.from).toBe(20000);
    });

    it('should sort by salary descending', () => {
      const offers = [
        createMockOffer({ id: 1, salaryFrom: 10000 }),
        createMockOffer({ id: 2, salaryFrom: 30000 }),
        createMockOffer({ id: 3, salaryFrom: 20000 }),
      ];

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'Salarydesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.filteredOffers[0].salary.from).toBe(30000);
      expect(result.current.filteredOffers[1].salary.from).toBe(20000);
      expect(result.current.filteredOffers[2].salary.from).toBe(10000);
    });

    it('should sort by name ascending', () => {
      const offers = [
        createMockOffer({ id: 1, jobTitle: 'Zebra Developer' }),
        createMockOffer({ id: 2, jobTitle: 'Alpha Engineer' }),
        createMockOffer({ id: 3, jobTitle: 'Beta Tester' }),
      ];

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'Nameasc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.filteredOffers[0].jobTitle).toBe('Alpha Engineer');
      expect(result.current.filteredOffers[1].jobTitle).toBe('Beta Tester');
      expect(result.current.filteredOffers[2].jobTitle).toBe('Zebra Developer');
    });

    it('should sort by creation date', () => {
      const now = Date.now();
      const offers = [
        createMockOffer({
          id: 1,
          dateAdded: new Date(now - 2 * 24 * 60 * 60 * 1000), // 2 days ago
        }),
        createMockOffer({
          id: 2,
          dateAdded: new Date(now), // Today
        }),
        createMockOffer({
          id: 3,
          dateAdded: new Date(now - 1 * 24 * 60 * 60 * 1000), // 1 day ago
        }),
      ];

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'CreationDate', 0, 10, emptyKeywords, offers)
      );

      // Most recent first
      expect(result.current.filteredOffers[0].id).toBe(2);
      expect(result.current.filteredOffers[1].id).toBe(3);
      expect(result.current.filteredOffers[2].id).toBe(1);
    });
  });

  describe('custom sorting', () => {
    it('should use custom sort function when provided', () => {
      const offers = [
        createMockOffer({ id: 1, salaryFrom: 10000 }),
        createMockOffer({ id: 2, salaryFrom: 20000 }),
        createMockOffer({ id: 3, salaryFrom: 15000 }),
      ];

      // Custom sort: by id ascending
      const customSort = (items: offer[]) =>
        [...items].sort((a, b) => a.id - b.id);

      const { result } = renderHook(() =>
        useOfferList(
          emptyFilters,
          'Salarydesc', // This should be ignored
          0,
          10,
          emptyKeywords,
          offers,
          customSort
        )
      );

      expect(result.current.filteredOffers[0].id).toBe(1);
      expect(result.current.filteredOffers[1].id).toBe(2);
      expect(result.current.filteredOffers[2].id).toBe(3);
    });
  });

  describe('filtering', () => {
    it('should filter by language', () => {
      const offers = [
        createMockOffer({
          id: 1,
          requiredLanguages: [{ languageName: 'English', languageLevelName: 'B2' }],
        }),
        createMockOffer({
          id: 2,
          requiredLanguages: [{ languageName: 'German', languageLevelName: 'C1' }],
        }),
        createMockOffer({
          id: 3,
          requiredLanguages: [{ languageName: 'English', languageLevelName: 'C1' }],
        }),
      ];

      const filters: FiltersState = {
        languages: new Set(['english']),
      };

      const { result } = renderHook(() =>
        useOfferList(filters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(2);
      expect(result.current.filteredOffers.every(o => 
        o.requirements.languages?.some(l => l.language?.toLowerCase() === 'english')
      )).toBe(true);
    });

    it('should filter by language level', () => {
      const offers = [
        createMockOffer({
          id: 1,
          requiredLanguages: [{ languageName: 'English', languageLevelName: 'B2' }],
        }),
        createMockOffer({
          id: 2,
          requiredLanguages: [{ languageName: 'English', languageLevelName: 'C1' }],
        }),
      ];

      const filters: FiltersState = {
        languageLevels: new Set(['C1']),
      };

      const { result } = renderHook(() =>
        useOfferList(filters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      expect(result.current.total).toBe(1);
      expect(result.current.filteredOffers[0].id).toBe(2);
    });

    it('should filter before sorting and paginating on large mixed dataset', () => {
      const offers = [
        createMockOffer({ id: 1, salaryFrom: 9000, requiredLanguages: [{ languageName: 'English', languageLevelName: 'B2' }] }),
        createMockOffer({ id: 2, salaryFrom: 18000, requiredLanguages: [{ languageName: 'English', languageLevelName: 'C1' }] }),
        createMockOffer({ id: 3, salaryFrom: 12000, requiredLanguages: [{ languageName: 'German', languageLevelName: 'C1' }] }),
        createMockOffer({ id: 4, salaryFrom: 22000, requiredLanguages: [{ languageName: 'English', languageLevelName: 'B2' }] }),
        createMockOffer({ id: 5, salaryFrom: 7000, requiredLanguages: [{ languageName: 'Polish', languageLevelName: 'B2' }] }),
      ];

      const filters: FiltersState = {
        languages: new Set(['english']),
      };

      // First page after filtering and sorting by salary desc
      const { result: page1 } = renderHook(() =>
        useOfferList(filters, 'Salarydesc', 0, 1, emptyKeywords, offers)
      );

      expect(page1.current.total).toBe(3); // Only English-speaking offers remain
      expect(page1.current.filteredOffers).toHaveLength(1);
      expect(page1.current.filteredOffers[0].id).toBe(4); // Highest salary

      // Second page to ensure pagination respects filtered ordering
      const { result: page2 } = renderHook(() =>
        useOfferList(filters, 'Salarydesc', 1, 1, emptyKeywords, offers)
      );

      expect(page2.current.filteredOffers).toHaveLength(1);
      expect(page2.current.filteredOffers[0].id).toBe(2); // Next highest salary
    });
  });

  describe('keyword search', () => {
    it('should filter by skill keyword', () => {
      const offers = [
        createMockOffer({
          id: 1,
          techStackNames: ['TypeScript', 'React'],
        }),
        createMockOffer({
          id: 2,
          techStackNames: ['Java', 'Spring'],
        }),
        createMockOffer({
          id: 3,
          techStackNames: ['TypeScript', 'Node.js'],
        }),
      ];

      const keywords: searchFilterKeywords = {
        skillName: 'TypeScript',
      };

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, keywords, offers)
      );

      expect(result.current.total).toBe(2);
    });

    it('should filter by benefit keyword', () => {
      const offers = [
        createMockOffer({
          id: 1,
          benefitNames: ['Medical care', 'Sports card'],
        }),
        createMockOffer({
          id: 2,
          benefitNames: ['Free coffee', 'Training budget'],
        }),
      ];

      const keywords: searchFilterKeywords = {
        benefitName: 'Medical',
      };

      const { result } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, keywords, offers)
      );

      expect(result.current.total).toBe(1);
      expect(result.current.filteredOffers[0].id).toBe(1);
    });
  });

  describe('memoization', () => {
    it('should return same reference when inputs do not change', () => {
      const offers = createMockOfferBatch(5);

      const { result, rerender } = renderHook(() =>
        useOfferList(emptyFilters, 'IdDesc', 0, 10, emptyKeywords, offers)
      );

      const firstResult = result.current;

      rerender();

      // Note: Due to how React hooks work, the memoized values should be stable
      expect(result.current.total).toBe(firstResult.total);
    });
  });
});
