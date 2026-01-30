import { describe, it, expect } from 'vitest';
import {
  SalaryDistanceCalculator,
  ScheduleDistanceCalculator,
} from '@/lib/ranking/distances/numerical';
import {
  BenefitDistanceCalculator,
  SkillsDistanceCalculator,
  EmploymentTypeDistanceCalculator,
  CategoryDistanceCalculator,
  LanguageDistanceCalculator,
} from '@/lib/ranking/distances/jaccard';
import { FreshnessDistanceCalculator } from '@/lib/ranking/distances/freshness';
import { languageLevelToNumeric } from '@/lib/ranking/utils';
import { createMockOffer, createMockPreferences } from '@tests/utils/mockData';

describe('Distance Calculators', () => {
  describe('SalaryDistanceCalculator', () => {
    const calculator = new SalaryDistanceCalculator();
    const preferences = createMockPreferences({
      salaryFrom: 15000,
      salaryTo: 20000,
    });

    it('should return low distance for matching salary range', () => {
      // Offer 16000-19000 overlaps with user 15000-20000
      // overlap = min(19000,20000) - max(16000,15000) = 3000
      // userRange = 5000, overlapRatio = 0.6, distance = 1 - 0.6 = 0.4
      const offer = createMockOffer({
        salaryFrom: 16000,
        salaryTo: 19000,
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeLessThan(0.5); // Good match should be < 0.5
    });

    it('should return perfect match (0) for exact salary overlap', () => {
      // Offer exactly matches user preferences - full overlap
      const offer = createMockOffer({
        salaryFrom: 15000,
        salaryTo: 20000,
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0);
    });

    it('should return high distance for much lower salary', () => {
      const offer = createMockOffer({
        salaryFrom: 5000,
        salaryTo: 7000,
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeGreaterThan(0.5);
    });

    it('should return high distance for much higher salary', () => {
      const offer = createMockOffer({
        salaryFrom: 30000,
        salaryTo: 40000,
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeGreaterThan(0.3);
    });

    it('should return 0.5 when both offer and user salary are missing', () => {
      const noSalaryPrefs = createMockPreferences({
        salaryFrom: null,
        salaryTo: null,
      });
      const offer = createMockOffer({
        salaryFrom: null,
        salaryTo: null,
      });

      const distance = calculator.sgdDistance(offer, noSalaryPrefs);

      expect(distance).toBe(0.5);
    });

    it('should compute difference between two offers', () => {
      const offer1 = createMockOffer({ salaryFrom: 10000, salaryTo: 15000 });
      const offer2 = createMockOffer({ salaryFrom: 20000, salaryTo: 25000 });

      const difference = calculator.rrelieffDifference(offer1, offer2);

      expect(difference).toBeGreaterThan(0);
      expect(difference).toBeLessThanOrEqual(1);
    });

    it('should return 0 difference for identical salaries', () => {
      const offer = createMockOffer({ salaryFrom: 15000, salaryTo: 20000 });

      const difference = calculator.rrelieffDifference(offer, offer);

      expect(difference).toBeCloseTo(0, 1);
    });
  });

  describe('BenefitDistanceCalculator', () => {
    const calculator = new BenefitDistanceCalculator();
    const preferences = createMockPreferences({
      benefits: ['Medical care', 'Sports card', 'Remote work'],
    });

    it('should return low distance for offers with many benefits', () => {
      const offer = createMockOffer({
        benefitNames: ['Medical care', 'Sports card', 'Training budget', 'Free coffee'],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeLessThan(0.7);
    });

    it('should return 0.5 when offer has no benefits', () => {
      const offer = createMockOffer({
        benefitNames: [],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0.5);
    });

    it('should compute difference between offer benefits', () => {
      const offer1 = createMockOffer({
        benefitNames: ['Medical care', 'Sports card'],
      });
      const offer2 = createMockOffer({
        benefitNames: ['Training budget', 'Free coffee'],
      });

      const difference = calculator.rrelieffDifference(offer1, offer2);

      expect(difference).toBeGreaterThan(0.5); // No overlap
    });

    it('should return low difference for similar benefits', () => {
      const offer1 = createMockOffer({
        benefitNames: ['Medical care', 'Sports card'],
      });
      const offer2 = createMockOffer({
        benefitNames: ['Medical care', 'Sports card', 'Coffee'],
      });

      const difference = calculator.rrelieffDifference(offer1, offer2);

      expect(difference).toBeLessThan(0.5);
    });
  });

  describe('SkillsDistanceCalculator', () => {
    const calculator = new SkillsDistanceCalculator();
    const preferences = createMockPreferences({
      skills: [
        { skillName: 'TypeScript', experienceMonths: 24 },
        { skillName: 'React', experienceMonths: 18 },
        { skillName: 'Node.js', experienceMonths: 12 },
      ],
    });

    it('should return low distance for matching skills', () => {
      const prefs = createMockPreferences({
        skills: [
          { skillName: 'TypeScript', experienceMonths: 36 },
          { skillName: 'React', experienceMonths: 24 },
          { skillName: 'Node.js', experienceMonths: 18 },
        ],
      });
      
      const offer = createMockOffer({
        techStackNames: ['TypeScript', 'React', 'Node.js'],
      });

      const distance = calculator.sgdDistance(offer, prefs);

      expect(distance).toBeLessThanOrEqual(0.5);
    });

    it('should return high distance for non-matching skills', () => {
      const prefs = createMockPreferences({
        skills: [
          { skillName: 'TypeScript', experienceMonths: 24 },
          { skillName: 'React', experienceMonths: 18 },
        ],
      });
      
      const offer = createMockOffer({
        techStackNames: ['Java', 'Spring', 'Hibernate'],
      });

      const distance = calculator.sgdDistance(offer, prefs);

      expect(distance).toBeGreaterThanOrEqual(0.5);
    });

    it('should return 0.5 when offer has no skill requirements', () => {
      const offer = createMockOffer({
        techStackNames: [],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0.5);
    });

    it('should compute difference in skill sets', () => {
      const offer1 = createMockOffer({
        techStackNames: ['TypeScript', 'React'],
      });
      const offer2 = createMockOffer({
        techStackNames: ['Java', 'Spring'],
      });

      const difference = calculator.rrelieffDifference(offer1, offer2);

      expect(difference).toBeGreaterThan(0.5);
    });
  });

  describe('EmploymentTypeDistanceCalculator', () => {
    const calculator = new EmploymentTypeDistanceCalculator();
    const preferences = createMockPreferences({
      employmentTypeIds: [1],
      employmentTypeNames: ['B2B'],
    });

    it('should return 0 for matching employment type', () => {
      const offer = createMockOffer({
        employmentTypeIds: [1],
        employmentTypeNames: ['B2B'],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0);
    });

    it('should return 1 for non-matching employment type', () => {
      const offer = createMockOffer({
        employmentTypeIds: [2],
        employmentTypeNames: ['UoP'],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(1);
    });

    it('should handle multiple employment types', () => {
      const preferences = createMockPreferences({
        employmentTypeIds: [1, 2],
        employmentTypeNames: ['B2B', 'UoP'],
      });

      const offer = createMockOffer({
        employmentTypeIds: [1],
        employmentTypeNames: ['B2B'],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0);
    });
  });

  describe('CategoryDistanceCalculator', () => {
    const calculator = new CategoryDistanceCalculator();
    const preferences = createMockPreferences({
      leadingCategoryId: 10,
      leadingCategoryName: 'IT/Development',
    });

    it('should return 0 for matching category', () => {
      const offer = createMockOffer({
        leadingCategory: 'IT/Development',
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0);
    });

    it('should return 1 for non-matching category', () => {
      const offer = createMockOffer({
        leadingCategory: 'Other',
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(1);
    });

    it('should return 0.5 when category is missing', () => {
      const offer = createMockOffer();
      offer.category.leadingCategory = null;

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0.5);
    });
  });

  describe('LanguageDistanceCalculator', () => {
    const calculator = new LanguageDistanceCalculator();
    const preferences = createMockPreferences({
      languages: [
        {
          languageId: 1,
          languageName: 'English',
          languageLevelId: 5,
          languageLevelName: 'B2',
        },
      ],
    });

    it('should return low distance for matching language', () => {
      const offer = createMockOffer({
        requiredLanguages: [
          {
            languageId: 1,
            languageName: 'English',
            languageLevelId: 5,
            languageLevelName: 'B2',
          },
        ],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeLessThan(0.5);
    });

    it('should return 0.1 when no language requirements (considered neutral/good)', () => {
      const offer = createMockOffer({
        requiredLanguages: [],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0.1);
    });

    it('should handle language level differences', () => {
      const preferences = createMockPreferences({
        languages: [
          {
            languageId: 1,
            languageName: 'English',
            languageLevelId: 7,
            languageLevelName: 'C2',
          },
        ],
      });

      const offer = createMockOffer({
        requiredLanguages: [
          {
            languageId: 1,
            languageName: 'English',
            languageLevelId: 3,
            languageLevelName: 'A2',
          },
        ],
      });

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBeLessThan(0.3);
    });
  });

  describe('languageLevelToNumeric utility', () => {
    it('should map language levels consistently (case-insensitive)', () => {
      const levels = ['A1', 'a2', 'B1', 'b2', 'C1', 'c2', 'native', 'NATYWNY'];
      const numeric = levels.map(languageLevelToNumeric);

      numeric.forEach(value => {
        expect(value).toBeGreaterThanOrEqual(0);
        expect(value).toBeLessThanOrEqual(1);
      });

      expect(numeric[0]).toBeLessThan(numeric[1]);
      expect(numeric[1]).toBeLessThan(numeric[2]);
      expect(numeric[2]).toBeLessThan(numeric[3]);
      expect(numeric[3]).toBeLessThan(numeric[4]);
      expect(numeric[4]).toBeLessThanOrEqual(numeric[5]);
      expect(numeric[5]).toBeLessThanOrEqual(numeric[6]);
      expect(numeric[6]).toBe(numeric[7]);
    });
  });

  describe('ScheduleDistanceCalculator', () => {
    const calculator = new ScheduleDistanceCalculator();
    const preferences = createMockPreferences();

    it('should prefer full-time positions', () => {
      const fullTime = createMockOffer({
        // todo
      });

      const distance = calculator.sgdDistance(fullTime, preferences);

      expect(distance).toBeDefined();
      expect(distance).toBeGreaterThanOrEqual(0);
      expect(distance).toBeLessThanOrEqual(1);
    });

    it('should return 0.5 for missing schedule', () => {
      const offer = createMockOffer({});
      offer.employment.schedules = [];

      const distance = calculator.sgdDistance(offer, preferences);

      expect(distance).toBe(0.5);
    });
  });

  describe('FreshnessDistanceCalculator', () => {
    const calculator = new FreshnessDistanceCalculator();
    const preferences = createMockPreferences();

    it('should return low distance for fresh offers (posted today)', () => {
      const freshOffer = createMockOffer({
        dateAdded: new Date(),
      });

      const distance = calculator.sgdDistance(freshOffer, preferences);

      expect(distance).toBeLessThan(0.2);
    });

    it('should return moderate distance for old offers (30+ days)', () => {
      const oldOffer = createMockOffer({
        dateAdded: new Date(Date.now() - 35 * 24 * 60 * 60 * 1000),
      });

      const distance = calculator.sgdDistance(oldOffer, preferences);

      expect(distance).toBeGreaterThan(0.3);
      expect(distance).toBeLessThan(0.7);
    });

    it('should have monotonically increasing distance with age', () => {
      const ages = [0, 7, 14, 21, 30];
      const distances = ages.map(days => {
        const offer = createMockOffer({
          dateAdded: new Date(Date.now() - days * 24 * 60 * 60 * 1000),
        });
        return calculator.sgdDistance(offer, preferences);
      });

      for (let i = 1; i < distances.length; i++) {
        expect(distances[i]).toBeGreaterThanOrEqual(distances[i - 1]);
      }
    });

    it('should compute difference based on date difference', () => {
      const offer1 = createMockOffer({
        dateAdded: new Date(),
      });

      const offer2 = createMockOffer({
        dateAdded: new Date(Date.now() - 20 * 24 * 60 * 60 * 1000),
      });

      const difference = calculator.rrelieffDifference(offer1, offer2);

      expect(difference).toBeGreaterThan(0);
      expect(difference).toBeLessThanOrEqual(1);
    });
  });

  describe('Distance Calculator Integration', () => {
    it('should all return values in [0, 1] range', () => {
      const calculators = [
        new SalaryDistanceCalculator(),
        new BenefitDistanceCalculator(),
        new SkillsDistanceCalculator(),
        new EmploymentTypeDistanceCalculator(),
        new CategoryDistanceCalculator(),
        new LanguageDistanceCalculator(),
        new ScheduleDistanceCalculator(),
        new FreshnessDistanceCalculator(),
      ];

      const offer = createMockOffer();
      const preferences = createMockPreferences();

      calculators.forEach(calculator => {
        const distance = calculator.sgdDistance(offer, preferences);
        expect(distance).toBeGreaterThanOrEqual(0);
        expect(distance).toBeLessThanOrEqual(1);
      });
    });

    it('should all compute valid RReliefF differences', () => {
      const calculators = [
        new SalaryDistanceCalculator(),
        new BenefitDistanceCalculator(),
        new SkillsDistanceCalculator(),
        new EmploymentTypeDistanceCalculator(),
        new CategoryDistanceCalculator(),
        new LanguageDistanceCalculator(),
        new ScheduleDistanceCalculator(),
        new FreshnessDistanceCalculator(),
      ];

      const offer1 = createMockOffer({ offerId: 1 });
      const offer2 = createMockOffer({ offerId: 2 });

      calculators.forEach(calculator => {
        const difference = calculator.rrelieffDifference(offer1, offer2);
        expect(difference).toBeGreaterThanOrEqual(0);
        expect(difference).toBeLessThanOrEqual(1);
        expect(isFinite(difference)).toBe(true);
      });
    });
  });
});
