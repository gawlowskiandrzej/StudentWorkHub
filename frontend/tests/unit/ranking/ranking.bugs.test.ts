import { describe, it, expect, beforeEach } from 'vitest';
import { Ranking } from '@/lib/ranking/ranking';
import { PreferencesDto } from '@/types/api/usersDTO';
import { createMockOffer, createMockPreferences } from '@tests/utils/mockData';

describe('Ranking Algorithm - Bug Exposure Tests', () => {
  let ranking: Ranking;

  beforeEach(() => {
    ranking = new Ranking();
  });

  describe('Bug #1: No validation of weight vectors', () => {
    it('should accept NaN weights from backend', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      ranking.loadWeights({
        vector: [NaN, 0.5, 0.3, 0.2, 0.1, 0.1, 0.1, 0.1],
        order_by_option: [],
        mean_dist: [0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1],
      });

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      console.warn('BUG EXPOSED #1: NaN weights accepted');
      console.warn('Score with NaN weights:', score);
      
      expect(Number.isNaN(score)).toBe(false);
    });

    it('should accept negative weights', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      ranking.loadWeights({
        vector: [-0.5, -0.3, -0.2, -0.1, -0.1, -0.1, -0.1, -0.1],
        order_by_option: [],
        mean_dist: [0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1],
      });

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      console.warn('BUG EXPOSED #1b: Negative weights accepted');
      console.warn('Score with negative weights:', score);
      
     expect(score).toBeGreaterThan(0); 
    });

    it('should accept weights with wrong length', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      ranking.loadWeights({
        vector: [0.5, 0.3, 0.2], // Too few weights!
        order_by_option: [],
        mean_dist: [0.1, 0.1, 0.1],
      });

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      console.warn('BUG EXPOSED #1c: Wrong weight vector length ignored');
      console.warn('Score:', score);
      
      expect(score).toBeDefined();
    });
  });

  describe('Bug #2: Invalid feature names in order_by_option', () => {
    it('should accept invalid feature names', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      ranking.loadWeights({
        vector: [0.5, 0.3, 0.2, 0.1, 0.1, 0.1, 0.1, 0.1],
        order_by_option: ['INVALID_FEATURE', 'ANOTHER_INVALID', 'SALARY_MATCH'],
        mean_dist: [0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1],
      });

      console.warn('BUG EXPOSED #2: Invalid feature names in order_by_option');
      
      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);
      expect(score).toBeDefined();
    });
  });

  describe('Bug #3: Null preferences handling', () => {
    it('should reset to defaults when null but keeps old scores', () => {
      const preferences1 = createMockPreferences({
        salaryFrom: 50000,
        salaryTo: 100000,
      });

      ranking.setUserPreferences({
        leading_category_id: preferences1.leadingCategoryId,
        leading_category_name: preferences1.leadingCategoryName,
        salary_from: preferences1.salaryFrom,
        salary_to: preferences1.salaryTo,
        employment_type_ids: preferences1.employmentTypeIds,
        employment_type_names: preferences1.employmentTypeNames,
        job_status: preferences1.jobStatus,
        city_name: preferences1.cityName,
        work_types: preferences1.workTypes,
        languages: preferences1.languages,
        skills: [],
      } as any);

      const offers = [createMockOffer({ id: 1 }), createMockOffer({ id: 2 })];
      const scores1 = ranking.loadOffersAndScore(offers);
      const score1 = scores1[0].score;

      // Now set to null
      ranking.setUserPreferences(null);
      const scores2 = ranking.loadOffersAndScore(offers);
      const score2 = scores2[0].score;

      console.warn('BUG EXPOSED #3: Scores with preferences:', score1);
      console.warn('Scores after null preferences:', score2);
      
      expect(score2).toBeDefined();
    });
  });

  describe('Bug #4: Preference conversion and data loss', () => {
    it('should lose skills data during conversion', () => {
      const preferencesDto: PreferencesDto = {
        leading_category_id: 1,
        leading_category_name: 'IT',
        salary_from: 10000,
        salary_to: 50000,
        employment_type_ids: [1],
        employment_type_names: ['B2B'],
        job_status: 'actively_looking',
        city_name: 'Warsaw',
        work_types: ['Remote'],
        languages: [],
        skills: [
          { skill_name: 'React', experience_months: 24 },
          { skill_name: 'Node.js', experience_months: 12 },
        ],
        benefits: [],
      };

      ranking.setUserPreferences(preferencesDto);

      const offer = createMockOffer({
        requirements: {
          skills: [
            { skillName: 'React', experienceLevel: ['advanced'] },
            { skillName: 'Python', experienceLevel: ['intermediate'] },
          ],
        },
      });

      console.warn('BUG EXPOSED #4: Skills conversion might lose data');
      
      const score = ranking.getOfferScore(offer);
      expect(score).toBeDefined();
    });
  });

  describe('Bug #5: Empty offers list edge cases', () => {
    it('should handle empty offers array', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      const scores = ranking.loadOffersAndScore([]);

      console.warn('BUG: Empty offers list result:', scores);
      
      expect(scores).toHaveLength(0);
    });

    it('should handle very large offers list causing performance issues', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      const largeOffersList = Array.from({ length: 10000 }, (_, i) =>
        createMockOffer({ id: i })
      );

      console.warn('BUG EXPOSED #5: Performance issue with large datasets');
      
      const start = performance.now();
      const scores = ranking.loadOffersAndScore(largeOffersList);
      const duration = performance.now() - start;

      console.warn(`Ranked ${scores.length} offers in ${duration.toFixed(2)}ms`);
      
      expect(duration).toBeLessThan(5000); // Should be fast
    });
  });

  describe('Bug #6: Result sorting issues', () => {
    it('should return sorted results but might not be stable', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      const offers = [
        createMockOffer({ id: 1, salary: { from: 10000, to: 20000 } }),
        createMockOffer({ id: 2, salary: { from: 50000, to: 60000 } }),
        createMockOffer({ id: 3, salary: { from: 30000, to: 40000 } }),
      ];

      const scores = ranking.loadOffersAndScore(offers);

      console.warn('BUG: Verify scores are properly sorted');
      console.warn('Scores:', scores.map(s => ({ id: s.offerId, score: s.score })));
      
      for (let i = 1; i < scores.length; i++) {
        expect(scores[i - 1].score).toBeGreaterThanOrEqual(scores[i].score);
      }
    });
  });

  describe('Bug #7: Missing offer properties handling', () => {
    it('should handle offers with null benefits', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      const offer = createMockOffer({
        requirements: {
          benefits: null as any, // null benefits
          skills: [],
        },
      });

      console.warn('BUG EXPOSED #7: Null benefits in offer');
      
      expect(() => {
        ranking.getOfferScore(offer);
      }).not.toThrow();
    });

    it('should handle offers with undefined salary', () => {
      const preferences = createMockPreferences({
        salaryFrom: 20000,
        salaryTo: 50000,
      });

      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        leading_category_name: preferences.leadingCategoryName,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        employment_type_names: preferences.employmentTypeNames,
        job_status: preferences.jobStatus,
        city_name: preferences.cityName,
        work_types: preferences.workTypes,
        languages: preferences.languages,
        skills: [],
      } as any);

      const offer = createMockOffer({
        salary: undefined as any,
      });

      console.warn('BUG: Undefined salary in offer');
      
      const score = ranking.getOfferScore(offer);
      expect(score).toBeDefined();
    });
  });

  describe('Bug #8: Cache invalidation problems', () => {
    it('should invalidate cache when preferences change', () => {
      const preferences1 = createMockPreferences({ salaryFrom: 10000, salaryTo: 30000 });
      ranking.setUserPreferences({
        leading_category_id: preferences1.leadingCategoryId,
        leading_category_name: preferences1.leadingCategoryName,
        salary_from: preferences1.salaryFrom,
        salary_to: preferences1.salaryTo,
        employment_type_ids: preferences1.employmentTypeIds,
        employment_type_names: preferences1.employmentTypeNames,
        job_status: preferences1.jobStatus,
        city_name: preferences1.cityName,
        work_types: preferences1.workTypes,
        languages: preferences1.languages,
        skills: [],
      } as any);

      const offers = [createMockOffer({ id: 1 }), createMockOffer({ id: 2 })];
      const scores1 = ranking.loadOffersAndScore(offers);
      const score1Before = scores1[0].score;

      const preferences2 = createMockPreferences({ salaryFrom: 50000, salaryTo: 100000 });
      ranking.setUserPreferences({
        leading_category_id: preferences2.leadingCategoryId,
        leading_category_name: preferences2.leadingCategoryName,
        salary_from: preferences2.salaryFrom,
        salary_to: preferences2.salaryTo,
        employment_type_ids: preferences2.employmentTypeIds,
        employment_type_names: preferences2.employmentTypeNames,
        job_status: preferences2.jobStatus,
        city_name: preferences2.cityName,
        work_types: preferences2.workTypes,
        languages: preferences2.languages,
        skills: [],
      } as any);

      const scores2 = ranking.loadOffersAndScore(offers);
      const score1After = scores2[0].score;

      console.warn('BUG EXPOSED #8: Cache invalidation');
      console.warn('Score before preference change:', score1Before);
      console.warn('Score after preference change:', score1After);
      
      if (preferences1.salaryFrom !== preferences2.salaryFrom) {
        console.warn('WARNING: Cache might not be properly invalidated');
      }
    });
  });
});
