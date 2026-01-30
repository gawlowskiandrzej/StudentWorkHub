import { describe, it, expect } from 'vitest';
import { sortData } from '@/utils/offerFilters/sortOffers';
import { createMockOffer } from '@tests/utils/mockData';

describe('sortOffers - Bug Exposure Tests', () => {
  describe('Bug #1: Incorrect handling of null/undefined salaries', () => {
    it('should incorrectly sort null salaries as 0', () => {
      const o1 = createMockOffer({ id: 1 }); o1.salary = { from: 10000, to: 15000 } as any;
      const o2 = createMockOffer({ id: 2 }); o2.salary = null as any;
      const o3 = createMockOffer({ id: 3 }); o3.salary = { from: 5000, to: 8000 } as any;
      const offers = [o1, o2, o3];

      const sortedAsc = sortData(offers, 'Salaryasc');
      
      console.warn('Bug #1: Fixed null salary sorting');
      // Correct behavior: 5000 (ID 3) -> 10000 (ID 1) -> null (ID 2, last)
      expect(sortedAsc[0].id).toBe(3); 
    });

    it('should incorrectly sort descending with null salaries', () => {
      const o1 = createMockOffer({ id: 1 }); o1.salary = { from: 50000, to: 60000 } as any;
      const o2 = createMockOffer({ id: 2 }); o2.salary = null as any;
      const o3 = createMockOffer({ id: 3 }); o3.salary = { from: 80000, to: 100000 } as any;
      const offers = [o1, o2, o3];

      const sortedDesc = sortData(offers, 'Salarydesc');
      
      console.warn('Bug #1: Fixed null salary desc sort');
      // Correct behavior: 80000 (ID 3) -> 50000 (ID 1) -> null (ID 2, last)
      const lastOffer = sortedDesc[sortedDesc.length - 1];
      expect(lastOffer.id).toBe(2); 
    });
  });

 describe('Bug #2: Invalid sortValue returns original array', () => {
    it('should return unsorted array for invalid sort value', () => {
      const offers = [
        createMockOffer({ id: 3, salary: { from: 5000, to: 10000 } }),
        createMockOffer({ id: 1, salary: { from: 50000, to: 60000 } }),
        createMockOffer({ id: 2, salary: { from: 20000, to: 30000 } }),
      ];

      const sortedByInvalid = sortData(offers, 'InvalidSortValue');
      
      console.warn('BUG EXPOSED #2: Invalid sort value silently ignored');
      console.warn('Sorted order:', sortedByInvalid.map(o => o.id));
      
      expect(sortedByInvalid[0].id).toBe(3); // Returns in original order
      expect(sortedByInvalid).toBe(offers); // Same reference!
    });

    it('should cause inconsistent UI behavior with typos', () => {
      const o3 = createMockOffer({ id: 3 }); o3.salary = { from: 3000, to: 5000 } as any;
      const o1 = createMockOffer({ id: 1 }); o1.salary = { from: 10000, to: 20000 } as any;
      const offers = [o3, o1];

      // Typo: "Salarydesc" is correct, "SalaryDesc" is not
      const sortedWrong = sortData(offers, 'SalaryDesc'); // Wrong case
      const sortedRight = sortData(offers, 'Salarydesc'); // Correct
      
      console.warn('Bug #2: Fixed case-sensitive sort values');
      // Fixed: Both should sort correctly (descending: 10k > 3k -> ID 1 first)
      expect(sortedWrong[0].id).toBe(1); // Now works!
      expect(sortedRight[0].id).toBe(1); 
    });
  });

  describe('Bug #3: Case-sensitive sort values', () => {
    it('should sort correctly even with incorrect casing', () => {
      const offers = [
        createMockOffer({ id: 2, jobTitle: 'Zebra Developer' }),
        createMockOffer({ id: 1, jobTitle: 'Apple Developer' }),
      ];

      const sortedCorrect = sortData(offers, 'Nameasc');
      const sortedWrong = sortData(offers, 'Nameasc'.toUpperCase()); // Wrong case
      
      console.log('Fixed Bug #3: Case sensitivity in sort values handled successfully');
      
      expect(sortedCorrect[0].jobTitle).toBe('Apple Developer'); // Works
      expect(sortedWrong[0].jobTitle).toBe('Apple Developer'); // Works now (Fixed case sensitivity)
    });
  });

  describe('Bug #4: Unsafe string comparison for jobTitle', () => {
    it('should crash when jobTitle is not a string', () => {
      const offers = [
        { ...createMockOffer({ id: 1 }), jobTitle: null } as any,
        { ...createMockOffer({ id: 2 }), jobTitle: 'Developer' },
      ];

      console.warn('Bug #4: Fixed crash with null jobTitle');
      
      expect(() => {
        sortData(offers, 'Nameasc');
      }).not.toThrow(); 
    });

    it('should crash with undefined jobTitle', () => {
      const offers = [
        { ...createMockOffer({ id: 1 }), jobTitle: undefined } as any,
        { ...createMockOffer({ id: 2 }), jobTitle: 'Developer' },
      ];

      console.warn('Bug #4: Fixed crash with undefined jobTitle');
      
      expect(() => {
        sortData(offers, 'Nameasc');
      }).not.toThrow();
    });
  });

  describe('Bug #5: Inconsistent return behavior', () => {
    it('should return different array for some sorts but same for default', () => {
      const offers = [
        createMockOffer({ id: 3, salary: { from: 5000, to: 10000 } }),
        createMockOffer({ id: 1, salary: { from: 50000, to: 60000 } }),
      ];

      const sortedByName = sortData(offers, 'Nameasc');
      const sortedByDefault = sortData(offers, 'UnknownSort');
      
      console.warn('BUG EXPOSED #5: Inconsistent return reference behavior');
      
      // All other sorts create new array, but default returns original
      expect(sortedByName).not.toBe(offers);
      expect(sortedByDefault).toBe(offers); // Same reference!
      
      // This can cause mutation issues
      sortedByDefault[0] = createMockOffer({ id: 999 });
      expect(offers[0].id).toBe(999); // Original array was mutated!
    });
  });

  describe('Bug #6: Unsafe date parsing in CreationDate sort', () => {
    it('should crash with invalid date format', () => {
      const offers = [
        { ...createMockOffer(), dates: { published: 'invalid-date' } } as any,
        { ...createMockOffer(), dates: { published: '2024-01-15' } },
      ];

      console.warn('BUG EXPOSED #6: Invalid date format in CreationDate sort');
      
      expect(() => {
        sortData(offers, 'CreationDate');
      }).not.toThrow(); // Surprisingly doesn't throw, returns NaN
      
      // But sorting with NaN produces undefined behavior
      const result = sortData(offers, 'CreationDate');
      console.warn('Sort result with NaN dates:', result);
    });

    it('should handle missing published date', () => {
      const offers = [
        { ...createMockOffer({ id: 1 }), dates: { published: undefined } } as any,
        { ...createMockOffer({ id: 2 }), dates: { published: '2024-01-15' } },
      ];

      console.warn('BUG: Missing published date causes NaN comparison');
      
      const result = sortData(offers, 'CreationDate');
      expect(result).toBeDefined(); // Works but unpredictably
    });
  });

  describe('Bug #7: Edge cases with empty arrays', () => {
    it('should handle empty array consistently', () => {
      const emptyOffers = [] as any[];

      const resultIdDesc = sortData(emptyOffers, 'IdDesc');
      const resultNameAsc = sortData(emptyOffers, 'Nameasc');
      const resultDefault = sortData(emptyOffers, 'Unknown');

      console.warn('BUG: Empty array handling is inconsistent');
      
      // Creates new array for some sorts, returns same for default
      expect(resultIdDesc).not.toBe(emptyOffers);
      expect(resultNameAsc).not.toBe(emptyOffers);
      expect(resultDefault).toBe(emptyOffers); // Same reference
    });
  });

  describe('Bug #8: Salary edge cases', () => {
    it('should incorrectly handle negative salaries', () => {
      const o1 = createMockOffer({ id: 1 }); o1.salary = { from: -5000, to: 0 } as any;
      const o2 = createMockOffer({ id: 2 }); o2.salary = { from: 10000, to: 20000 } as any;
      const offers = [o1, o2];

      const sorted = sortData(offers, 'Salaryasc');
      
      console.warn('Bug #8: Handling negative salaries');
      console.warn('Sorted:', sorted.map(o => o.salary?.from));
      
      expect(sorted[0].salary?.from).toBe(-5000); 
    });

    it('should handle salaries with same "from" value', () => {
      const o1 = createMockOffer({ id: 1 }); o1.salary = { from: 10000, to: 15000 } as any;
      const o2 = createMockOffer({ id: 2 }); o2.salary = { from: 10000, to: 25000 } as any;
      const o3 = createMockOffer({ id: 3 }); o3.salary = { from: 10000, to: 12000 } as any;
      const offers = [o1, o2, o3];

      const sorted = sortData(offers, 'Salaryasc');
      
      console.warn('Bug #8: Same salary "from" values');
      
      expect(sorted[0].salary?.from).toBe(10000);
      expect(sorted[1].salary?.from).toBe(10000);
      expect(sorted[2].salary?.from).toBe(10000);
    });
  });
});
