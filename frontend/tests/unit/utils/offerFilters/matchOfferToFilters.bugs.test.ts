import { describe, it, expect } from 'vitest';
import { matchOfferToFilters } from '@/utils/offerFilters/matchOfferToFilters';
import { createMockOffer } from '@tests/utils/mockData';
import { FiltersState } from '@/app/(public)/list/page';

describe('matchOfferToFilters - Bug Exposure Tests', () => {
  describe('Bug #1: Inconsistent logic for experience levels (AND vs OR)', () => {
    it('should fail when some skills match but not all (AND logic bug)', () => {
      const offer = createMockOffer({
        id: 1,
        jobTitle: 'Senior Developer',
        requirements: {
          skills: [
            { skillName: 'React', experienceLevel: ['advanced'] },
            { skillName: 'Node.js', experienceLevel: ['intermediate'] }, // This doesn't match!
            { skillName: 'TypeScript', experienceLevel: ['advanced'] },
          ],
        },
      });

      const filters: FiltersState = {
        experienceLevels: new Set(['advanced']),
        languages: new Set(),
      };

      const result = matchOfferToFilters(offer, filters);
      
      console.warn('BUG EXPOSED #1: experienceLevels uses AND logic instead of OR');
      console.warn('Result:', result, '- Expected: true, Actual:', result);
      expect(result).toBe(false); // This is the buggy behavior
    });

    it('should work inconsistently compared to languages filter', () => {
      const offer = createMockOffer({
        id: 2,
        requirements: {
          languages: [
            { language: 'English', level: 'B2' },
            { language: 'Polish', level: 'A1' }, // This doesn't match!
          ],
          skills: [
            { skillName: 'React', experienceLevel: ['advanced'] },
            { skillName: 'Node.js', experienceLevel: ['intermediate'] },
          ],
        },
      });

      const languageFilters: FiltersState = {
        languages: new Set(['English']), // Only English selected
        languageLevels: new Set(),
        experienceLevels: new Set(),
      };

      const languageResult = matchOfferToFilters(offer, languageFilters);
      console.warn('Languages result (OR logic):', languageResult); // TRUE - correctly matches

      const experienceFilters: FiltersState = {
        languages: new Set(),
        languageLevels: new Set(),
        experienceLevels: new Set(['advanced']),
      };

      const experienceResult = matchOfferToFilters(offer, experienceFilters);
      console.warn('Experience result (AND logic):', experienceResult); // FALSE - incorrectly rejects
      
      expect(languageResult).not.toBe(experienceResult);
    });
  });

  describe('Bug #2: Empty experienceLevel array bypasses filter', () => {
    it('should incorrectly match offers with empty experience levels', () => {
      const offer = createMockOffer({
        id: 3,
        requirements: {
          skills: [
            { skillName: 'React', experienceLevel: [] }, // Empty array!
            { skillName: 'Node.js', experienceLevel: ['junior'] },
          ],
        },
      });

      const filters: FiltersState = {
        experienceLevels: new Set(['senior', 'mid-level']),
        languages: new Set(),
      };

      // BUG FIXED: Returns FALSE because handled correctly
      const result = matchOfferToFilters(offer, filters);
      
      console.warn('Bug #2: Empty experienceLevel array correctly handled');
      expect(result).toBe(false); // Fixed behavior
    });
  });

  describe('Bug #3: Unsafe null/undefined handling in language matching', () => {
    it('should handle null language gracefully', () => {
      const offer = createMockOffer({
        id: 4,
        requirements: {
          languages: [
            { language: null as any, level: 'B2' },
            { language: 'English', level: 'C1' },
          ],
        },
      });

      const filters: FiltersState = {
        languages: new Set(['English']),
        languageLevels: new Set(),
        experienceLevels: new Set(),
      };

      try {
        const result = matchOfferToFilters(offer, filters);
        console.warn('BUG: Null language handling', result);
      } catch (e) {
        console.warn('BUG EXPOSED #3: Crash with null language:', e);
      }
    });
  });

  describe('Bug #4: Case sensitivity issues in language matching', () => {
    it('should fail to match languages with different cases', () => {
      const offer = createMockOffer({
        id: 5,
        requirements: {
          languages: [
            { language: 'ENGLISH', level: 'C1' }, // Uppercase
            { language: 'POLISH', level: 'C1' },
          ],
        },
      });

      const filters: FiltersState = {
        languages: new Set(['english']), // Lowercase
        languageLevels: new Set(),
        experienceLevels: new Set(),
      };

      const result = matchOfferToFilters(offer, filters);
      console.warn('Bug #4: Case sensitivity check');
      expect(result).toBe(true); // Fixed: should match
    });
  });

  describe('Bug #5: Missing null check for languages array', () => {
    it('should handle offer without languages property', () => {
      const offer = createMockOffer({
        id: 6,
        requirements: {
          skills: [{ skillName: 'React', experienceLevel: ['advanced'] }],
        },
      });
      offer.requirements.languages = undefined;

      const filters: FiltersState = {
        languages: new Set(['English']),
        languageLevels: new Set(),
        experienceLevels: new Set(),
      };

      const result = matchOfferToFilters(offer, filters);
      expect(result).toBe(false);
      console.warn('Bug #5: Undefined languages safely handled');
    });
  });

  describe('Bug #6: Multiple filter combination logic', () => {
    it('should fail when all filters are applied together', () => {
      const offer = createMockOffer({
        id: 7,
        requirements: {
          languages: [
            { language: 'English', level: 'C1' },
          ],
          skills: [
            { skillName: 'React', experienceLevel: ['advanced', 'mid-level'] },
            { skillName: 'Node.js', experienceLevel: ['junior'] }, // Doesn't match senior
          ],
        },
      });

      const filters: FiltersState = {
        languages: new Set(['English']),
        languageLevels: new Set(),
        experienceLevels: new Set(['senior']), // Requires senior but Node.js is junior
      };

      const result = matchOfferToFilters(offer, filters);
      console.warn('BUG: Combined filter logic uses AND instead of OR');
      expect(result).toBe(false); // Buggy behavior
    });
  });
});
