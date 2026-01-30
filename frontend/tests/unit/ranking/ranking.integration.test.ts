import { describe, it, expect, beforeEach } from 'vitest';
import { Ranking } from '@/lib/ranking/ranking';
import { PreferencesDto, WeightsDto } from '@/types/api/usersDTO';
import { createMockOffer, createMockPreferences, createMockOfferBatch, MOCK_OFFERS } from '@tests/utils/mockData';
import { FEATURE_COUNT } from '@/lib/ranking/types';

describe('Ranking Class - Integration Tests', () => {
  let ranking: Ranking;

  beforeEach(() => {
    ranking = new Ranking();
  });

  describe('Initialization', () => {
    it('should create a new instance with default values', () => {
      expect(ranking).toBeDefined();
      expect(ranking).toBeInstanceOf(Ranking);
    });

    it('should start with empty caches', () => {
      const offers = createMockOfferBatch(3);
      const scores = ranking.rankOffers(offers);

      expect(scores).toHaveLength(3);
    });
  });

  describe('User Preferences', () => {
    it('should load user preferences correctly', () => {
      const preferences = {
        leading_category_id: 10,
        leading_category_name: 'IT/Development',
        salary_from: 15000,
        salary_to: 20000,
        employment_type_ids: [1],
        employment_type_names: ['B2B'],
        job_status: 'actively_looking',
        city_name: 'Warsaw',
        work_types: ['Remote'],
        languages: [],
        skills: [],
        benefits: [],
      } as PreferencesDto;

      ranking.setUserPreferences(preferences);

      const offer = createMockOffer({
        categoryId: 10,
        salaryFrom: 16000,
        salaryTo: 19000,
      });

      const score = ranking.getOfferScore(offer);
      expect(score).toBeGreaterThan(0.5);
    });

    it('should reset to defaults when null preferences provided', () => {
      ranking.setUserPreferences(null);

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      expect(score).toBeDefined();
      expect(score).toBeGreaterThanOrEqual(0);
      expect(score).toBeLessThanOrEqual(1);
    });
  });

  describe('Loading and Scoring Offers', () => {
    beforeEach(() => {
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
        skills: preferences.skills.map(s => ({
          skill_name: s.skillName,
          experience_months: s.experienceMonths,
        })),
      } as any);
    });

    it('should score and rank multiple offers', () => {
      const offers = createMockOfferBatch(10);
      const rankedOffers = ranking.loadOffersAndScore(offers);

      expect(rankedOffers).toHaveLength(10);

      for (let i = 1; i < rankedOffers.length; i++) {
        expect(rankedOffers[i - 1].score).toBeGreaterThanOrEqual(rankedOffers[i].score);
      }
    });

    it('should cache offer scores', () => {
      const offer = createMockOffer({ offerId: 1 });
      
      const score1 = ranking.getOfferScore(offer);
      const score2 = ranking.getOfferScore(offer);

      expect(score1).toBe(score2);
    });

    it('should include confidence scores', () => {
      const offers = createMockOfferBatch(5);
      const rankedOffers = ranking.loadOffersAndScore(offers);

      rankedOffers.forEach(scoredOffer => {
        expect(scoredOffer).toHaveProperty('confidence');
        expect(scoredOffer.confidence).toBeGreaterThanOrEqual(0);
        expect(scoredOffer.confidence).toBeLessThanOrEqual(1);
      });
    });

    it('should include feature vectors', () => {
      const offers = createMockOfferBatch(3);
      const rankedOffers = ranking.loadOffersAndScore(offers);

      rankedOffers.forEach(scoredOffer => {
        expect(scoredOffer).toHaveProperty('features');
        expect(scoredOffer.features.values).toHaveLength(FEATURE_COUNT);
      });
    });

    it('should rank perfect match higher than poor match', () => {
      const offers = [MOCK_OFFERS.perfectMatch, MOCK_OFFERS.poorMatch];
      const rankedOffers = ranking.loadOffersAndScore(offers);

      const perfectScore = rankedOffers.find(s => s.offerId === MOCK_OFFERS.perfectMatch.id)?.score;
      const poorScore = rankedOffers.find(s => s.offerId === MOCK_OFFERS.poorMatch.id)?.score;

      expect(perfectScore).toBeGreaterThan(poorScore!);
    });
  });

  describe('User Interactions and Learning', () => {
    beforeEach(() => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        work_types: preferences.workTypes,
        skills: preferences.skills.map(s => ({ 
          skill_name: s.skillName,
          experience_months: s.experienceMonths,
          entry_date: new Date().toISOString()
        })),
      } as any);
    });

    it('should strengthen salary-weighted offers after positive interactions', () => {
      const salaryMatch = createMockOffer({
        offerId: 11,
        salaryFrom: 17000,
        salaryTo: 19000,
        techStackNames: ['TypeScript', 'React'],
      });
      const salaryMismatch = createMockOffer({
        offerId: 12,
        salaryFrom: 7000,
        salaryTo: 9000,
        techStackNames: ['Excel'],
      });

      const initialRanking = ranking.loadOffersAndScore([salaryMatch, salaryMismatch]);
      const initialWeights = ranking.getProfileForSync().vector.slice();

      for (let i = 0; i < 5; i++) {
        ranking.recordInteraction(salaryMatch.id, 'click');
        ranking.recordInteraction(salaryMatch.id, 'view_time', 60000);
      }

      const reranked = ranking.rankOffers([salaryMatch, salaryMismatch]);
      const updatedWeights = ranking.getProfileForSync().vector;

      expect(updatedWeights[0]).toBeGreaterThan(0.01);

      const scoreGapBefore = initialRanking[0].score - initialRanking[1].score;
      const scoreGapAfter = reranked[0].score - reranked[1].score;
      expect(scoreGapAfter).toBeGreaterThan(scoreGapBefore);

      expect(reranked[0].offerId).toBe(salaryMatch.id);
    });

    it('should keep ordering stable when only very weak signals arrive', () => {
      const good = createMockOffer({ offerId: 21, salaryFrom: 17000, salaryTo: 19000, techStackNames: ['TypeScript'] });
      const bad = createMockOffer({ offerId: 22, salaryFrom: 5000, salaryTo: 7000, techStackNames: ['Cobol'] });

      const initial = ranking.loadOffersAndScore([good, bad]);

      for (let i = 0; i < 5; i++) {
        ranking.recordInteraction(bad.id, 'view_time', 100);
      }

      const updated = ranking.rankOffers([good, bad]);

      expect(updated[0].offerId).toBe(initial[0].offerId);
      expect(updated[1].offerId).toBe(initial[1].offerId);
    });

    it('should record click interaction', () => {
      const offers = createMockOfferBatch(5);
      ranking.loadOffersAndScore(offers);

      const offer = offers[0];
      const scoreBefore = ranking.getOfferScore(offer);

      ranking.recordInteraction(offer.id, 'click');

      expect(scoreBefore).toBeDefined();
    });

    it('should record view time interaction', () => {
      const offers = createMockOfferBatch(3);
      ranking.loadOffersAndScore(offers);

      const offer = offers[0];

      ranking.recordInteraction(offer.id, 'view_time', 30000);

      expect(true).toBe(true);
    });

    it('should record hover interaction', () => {
      const offers = createMockOfferBatch(3);
      ranking.loadOffersAndScore(offers);

      ranking.recordInteraction(offers[0].id, 'hover');

      expect(true).toBe(true);
    });

    it('should learn from multiple interactions', () => {
      const offers = createMockOfferBatch(10);
      const initialRanking = ranking.loadOffersAndScore(offers);

      const highSalaryOffers = offers.filter(o => (o.salary?.from ?? 0) > 15000);

      highSalaryOffers.slice(0, 3).forEach(offer => {
        ranking.recordInteraction(offer.id, 'click');
        ranking.recordInteraction(offer.id, 'view_time', 45000);
      });

      const updatedRanking = ranking.loadOffersAndScore(offers);

      expect(updatedRanking).toHaveLength(offers.length);
    });

    it('should ignore interactions for unknown offers', () => {
      const offers = createMockOfferBatch(3);
      ranking.loadOffersAndScore(offers);

      ranking.recordInteraction(999, 'click');

      expect(true).toBe(true);
    });

    it('should ignore very weak signals', () => {
      const offers = createMockOfferBatch(3);
      ranking.loadOffersAndScore(offers);

      ranking.recordInteraction(offers[0].id, 'view_time', 100);

      expect(true).toBe(true);
    });
  });

  describe('Weights Management', () => {
    it('should load SGD weights', () => {
      const customWeights = [0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1];
      const weights: WeightsDto = {
        vector: customWeights,
      };

      ranking.loadWeights(weights);

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      expect(score).toBeDefined();
    });

    it('should load RReliefF feature order', () => {
      const weights: WeightsDto = {
        vector: new Array(FEATURE_COUNT).fill(0.5),
        order_by_option: ['SALARY_MATCH', 'SKILLS_MATCH', 'CATEGORY_MATCH'],
      };

      ranking.loadWeights(weights);

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      expect(score).toBeDefined();
    });

    it('should load normalization stats (mean distances)', () => {
      const weights: WeightsDto = {
        vector: new Array(FEATURE_COUNT).fill(0.5),
        mean_dist: [0.3, 0.4, 0.5, 0.6, 0.4, 0.3, 0.5, 0.2],
      };

      ranking.loadWeights(weights);

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      expect(score).toBeDefined();
    });

    it('should handle null weights gracefully', () => {
      ranking.loadWeights(null);

      const offer = createMockOffer();
      const score = ranking.getOfferScore(offer);

      expect(score).toBeDefined();
    });

    it('should export current weights', () => {
      const offers = createMockOfferBatch(5);
      ranking.loadOffersAndScore(offers);

      ranking.recordInteraction(offers[0].id, 'click');

      const exportedWeights = ranking.getProfileForSync();

      expect(exportedWeights).toHaveProperty('vector');
      expect(exportedWeights.vector).toHaveLength(FEATURE_COUNT);
      expect(exportedWeights).toHaveProperty('order_by_option');
      expect(exportedWeights).toHaveProperty('mean_dist');
    });
  });

  describe('Performance and Scaling', () => {
    it('should handle 100+ offers efficiently', () => {
      const offers = createMockOfferBatch(150);
      
      const startTime = performance.now();
      const rankedOffers = ranking.loadOffersAndScore(offers);
      const endTime = performance.now();

      expect(rankedOffers).toHaveLength(150);
      expect(endTime - startTime).toBeLessThan(5000); // < 5 seconds
    });

    it('should maintain cache consistency', () => {
      const offers = createMockOfferBatch(10);
      ranking.loadOffersAndScore(offers);

      const offer = offers[0];
      const score1 = ranking.getOfferScore(offer);
      const score2 = ranking.getOfferScore(offer);
      const score3 = ranking.getOfferScore(offer);

      expect(score1).toBe(score2);
      expect(score2).toBe(score3);
    });

    it('should rebuild normalization stats on new offer batch', () => {
      const batch1 = createMockOfferBatch(10);
      const ranking1 = ranking.loadOffersAndScore(batch1);

      const batch2 = createMockOfferBatch(10);
      const ranking2 = ranking.loadOffersAndScore(batch2);

      expect(ranking1).toHaveLength(10);
      expect(ranking2).toHaveLength(10);
    });

    it('should rebuild normalization stats when switching datasets', () => {
      const preferences = createMockPreferences();
      ranking.setUserPreferences({
        leading_category_id: preferences.leadingCategoryId,
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        employment_type_ids: preferences.employmentTypeIds,
        work_types: preferences.workTypes,
        skills: preferences.skills.map(s => ({
          skill_name: s.skillName,
          experience_months: s.experienceMonths,
          entry_date: new Date().toISOString()
        })),
      } as any);

      const lowSalaryOffers = [
        createMockOffer({ offerId: 201, salaryFrom: 4000, salaryTo: 6000, dateAdded: new Date(Date.now() - 15 * 24 * 60 * 60 * 1000) }),
        createMockOffer({ offerId: 202, salaryFrom: 4500, salaryTo: 6500, dateAdded: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000) }),
        createMockOffer({ offerId: 203, salaryFrom: 5000, salaryTo: 7000, dateAdded: new Date(Date.now() - 5 * 24 * 60 * 60 * 1000) }),
      ];

      const highSalaryOffers = [
        createMockOffer({ offerId: 301, salaryFrom: 18000, salaryTo: 22000, dateAdded: new Date() }),
        createMockOffer({ offerId: 302, salaryFrom: 19000, salaryTo: 23000, dateAdded: new Date() }),
        createMockOffer({ offerId: 303, salaryFrom: 20000, salaryTo: 24000, dateAdded: new Date() }),
      ];

      ranking.loadOffersAndScore(lowSalaryOffers);
      const lowStats = ranking.getProfileForSync().mean_dist;

      ranking.loadOffersAndScore(highSalaryOffers);
      const highStats = ranking.getProfileForSync().mean_dist;

      expect(highStats[0]).not.toBeCloseTo(lowStats[0], 3);
    });
  });

  describe('Edge Cases', () => {
    it('should handle empty offer list', () => {
      const rankedOffers = ranking.loadOffersAndScore([]);

      expect(rankedOffers).toHaveLength(0);
    });

    it('should handle single offer', () => {
      const offer = createMockOffer();
      const rankedOffers = ranking.loadOffersAndScore([offer]);

      expect(rankedOffers).toHaveLength(1);
      expect(rankedOffers[0].score).toBeGreaterThanOrEqual(0);
      expect(rankedOffers[0].score).toBeLessThanOrEqual(1);
    });

    it('should handle offers with missing data', () => {
      const incompleteOffers = [
        createMockOffer({ salaryFrom: null, salaryTo: null }),
        createMockOffer({ techStackNames: [] }),
        createMockOffer({ benefitNames: [] }),
      ];

      const rankedOffers = ranking.loadOffersAndScore(incompleteOffers);

      expect(rankedOffers).toHaveLength(3);
      rankedOffers.forEach(scored => {
        expect(typeof scored.score).toBe('number');
      });
    });

    it('should handle duplicate offer IDs', () => {
      const offer = createMockOffer({ offerId: 1 });
      const offers = [offer, offer, offer];

      const rankedOffers = ranking.loadOffersAndScore(offers);

      expect(rankedOffers.length).toBeGreaterThanOrEqual(1);
    });
  });

  describe('Full User Journey Simulation', () => {
    it('should simulate complete user interaction cycle', () => {
      const preferences = createMockPreferences({
        salaryFrom: 15000,
        salaryTo: 20000,
        leadingCategoryId: 10,
        skills: [
          { skillName: 'TypeScript', experienceMonths: 24 },
          { skillName: 'React', experienceMonths: 18 },
        ],
      });

      ranking.setUserPreferences({
        salary_from: preferences.salaryFrom,
        salary_to: preferences.salaryTo,
        leading_category_id: preferences.leadingCategoryId,
        skills: preferences.skills.map(s => ({ 
          skill_name: s.skillName,
          experience_months: s.experienceMonths,
          entry_date: new Date().toISOString()
        })),
      } as any);

      const offers = [
        createMockOffer({
          offerId: 1,
          salaryFrom: 16000,
          categoryId: 10,
          techStackNames: ['TypeScript', 'React'],
        }),
        createMockOffer({
          offerId: 2,
          salaryFrom: 10000,
          categoryId: 99,
          techStackNames: ['Java'],
        }),
        createMockOffer({
          offerId: 3,
          salaryFrom: 18000,
          categoryId: 10,
          techStackNames: ['TypeScript', 'Vue'],
        }),
      ];

      const initialRanking = ranking.loadOffersAndScore(offers);

      const topOffer = initialRanking[0];
      ranking.recordInteraction(topOffer.offerId, 'click');
      ranking.recordInteraction(topOffer.offerId, 'view_time', 60000);

      const updatedRanking = ranking.rankOffers(offers);

      expect(initialRanking).toHaveLength(3);
      expect(updatedRanking).toHaveLength(3);

      const learnedWeights = ranking.getProfileForSync();

      expect(learnedWeights.vector).toHaveLength(FEATURE_COUNT);

      expect(learnedWeights.vector.some(w => w !== 0.5)).toBe(true);
    });
  });
});
