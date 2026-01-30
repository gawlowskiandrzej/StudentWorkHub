import { describe, it, expect, beforeEach } from 'vitest';
import {
  extractFeatureVector,
  extractFeatureDifferences,
  updateWelfordStats,
  getVariance,
  getVarianceSample,
  getCoefficientOfVariation,
  getStandardDeviation,
  updateNormalizationStats,
  createInitialNormalizationStats,
  buildNormalizationStatsFromOffers,
  extractFeaturesForOffers,
} from '@/lib/ranking/features/extractor';
import { FEATURE_COUNT, WelfordStats, NormalizationStats } from '@/lib/ranking/types';
import { createMockOffer, createMockPreferences, createMockOfferBatch } from '@tests/utils/mockData';

describe('Feature Extraction', () => {
  const preferences = createMockPreferences();

  describe('extractFeatureVector', () => {
    it('should extract all 8 features from an offer', () => {
      const offer = createMockOffer();
      const features = extractFeatureVector(offer, preferences);

      expect(features.values).toHaveLength(FEATURE_COUNT);
      expect(Object.keys(features.raw)).toHaveLength(FEATURE_COUNT);
    });

    it('should return values in range [0, 1]', () => {
      const offer = createMockOffer();
      const features = extractFeatureVector(offer, preferences);

      features.values.forEach(value => {
        expect(value).toBeGreaterThanOrEqual(0);
        expect(value).toBeLessThanOrEqual(1);
      });
    });

    it('should include all required feature keys in raw object', () => {
      const offer = createMockOffer();
      const features = extractFeatureVector(offer, preferences);

      const expectedKeys = [
        'SALARY_MATCH',
        'BENEFIT_MATCH',
        'SKILLS_MATCH',
        'EMPLOYMENT_TYPE_MATCH',
        'SCHEDULE_MATCH',
        'CATEGORY_MATCH',
        'LANGUAGE_MATCH',
        'FRESHNESS',
      ];

      expectedKeys.forEach(key => {
        expect(features.raw).toHaveProperty(key);
      });
    });

    it('should handle offer with missing data gracefully', () => {
      const incompleteOffer = createMockOffer({
        salaryFrom: null,
        salaryTo: null,
        benefitNames: [],
        techStackNames: [],
      });

      const features = extractFeatureVector(incompleteOffer, preferences);

      expect(features.values).toHaveLength(FEATURE_COUNT);
      features.values.forEach(value => {
        expect(isFinite(value)).toBe(true);
      });
    });

    it('should use normalization stats when provided', () => {
      const offers = createMockOfferBatch(10);
      const { stats } = buildNormalizationStatsFromOffers(offers, preferences);

      const offer = createMockOffer();
      const featuresWithStats = extractFeatureVector(offer, preferences, stats);
      const featuresWithoutStats = extractFeatureVector(offer, preferences);

      expect(featuresWithStats.values).toBeDefined();
      expect(featuresWithoutStats.values).toBeDefined();
    });

    it('should compute lower distance for matching salary', () => {
      const preferences = createMockPreferences({
        salaryFrom: 15000,
        salaryTo: 20000,
      });

      const matchingOffer = createMockOffer({
        salaryFrom: 16000,
        salaryTo: 19000,
      });

      const nonMatchingOffer = createMockOffer({
        salaryFrom: 5000,
        salaryTo: 7000,
      });

      const matchingFeatures = extractFeatureVector(matchingOffer, preferences);
      const nonMatchingFeatures = extractFeatureVector(nonMatchingOffer, preferences);

      // SALARY_MATCH is index 0
      expect(matchingFeatures.values[0]).toBeLessThan(nonMatchingFeatures.values[0]);
    });

    it('should compute lower distance for matching category', () => {
      const preferences = createMockPreferences({
        leadingCategoryId: 10,
        leadingCategoryName: 'IT/Development',
      });

      const matchingOffer = createMockOffer({
        categoryId: 10,
        categoryName: 'IT/Development',
      });

      const nonMatchingOffer = createMockOffer({
        categoryId: 99,
        categoryName: 'Other',
      });

      const matchingFeatures = extractFeatureVector(matchingOffer, preferences);
      const nonMatchingFeatures = extractFeatureVector(nonMatchingOffer, preferences);

      // CATEGORY_MATCH is index 5
      expect(matchingFeatures.values[5]).toBeLessThan(nonMatchingFeatures.values[5]);
    });

    it('should compute lower distance for fresh offers', () => {
      const recentOffer = createMockOffer({
        dateAdded: new Date(), // Today
      });

      const oldOffer = createMockOffer({
        dateAdded: new Date(Date.now() - 30 * 24 * 60 * 60 * 1000), // 30 days ago
      });

      const recentFeatures = extractFeatureVector(recentOffer, preferences);
      const oldFeatures = extractFeatureVector(oldOffer, preferences);

      // FRESHNESS is index 7
      expect(recentFeatures.values[7]).toBeLessThan(oldFeatures.values[7]);
    });
  });

  describe('edge cases and missing data', () => {
    it('should treat future published date as freshest and clamp distance', () => {
      const futureOffer = createMockOffer({
        dateAdded: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000),
      });

      const features = extractFeatureVector(futureOffer, preferences);

      // FRESHNESS index 7 should still be within [0,1]
      expect(features.values[7]).toBeGreaterThanOrEqual(0);
      expect(features.values[7]).toBeLessThanOrEqual(1);
    });

    it('should handle missing category and languages without NaN', () => {
      const offer = createMockOffer({
        categoryName: null,
        requiredLanguages: [],
      });

      const features = extractFeatureVector(offer, preferences);

      expect(features.values.every(v => Number.isFinite(v))).toBe(true);
    });
  });

  describe('extractFeatureDifferences', () => {
    it('should return differences array with length 8', () => {
      const offer1 = createMockOffer({ offerId: 1 });
      const offer2 = createMockOffer({ offerId: 2 });

      const differences = extractFeatureDifferences(offer1, offer2);

      expect(differences).toHaveLength(FEATURE_COUNT);
    });

    it('should return all zeros for identical offers', () => {
      const offer = createMockOffer();
      const differences = extractFeatureDifferences(offer, offer);

      differences.forEach(diff => {
        expect(diff).toBeCloseTo(0, 2);
      });
    });

    it('should return differences in range [0, 1]', () => {
      const offer1 = createMockOffer({
        salaryFrom: 10000,
        categoryId: 10,
      });

      const offer2 = createMockOffer({
        salaryFrom: 20000,
        categoryId: 99,
      });

      const differences = extractFeatureDifferences(offer1, offer2);

      differences.forEach(diff => {
        expect(diff).toBeGreaterThanOrEqual(0);
        expect(diff).toBeLessThanOrEqual(1);
      });
    });

    it('should compute larger differences for very different offers', () => {
      const offer1 = createMockOffer({
        salaryFrom: 20000,
        salaryTo: 25000,
        categoryId: 10,
        categoryName: 'IT',
        workTypeNames: ['Remote'],
        techStackNames: ['TypeScript', 'React', 'Node.js'],
      });

      const offer2 = createMockOffer({
        salaryFrom: 5000,
        salaryTo: 7000,
        categoryId: 99,
        categoryName: 'Other',
        workTypeNames: ['Office'],
        techStackNames: ['Excel'],
      });

      const differences = extractFeatureDifferences(offer1, offer2);

      const hasSigDiffs = differences.some(diff => diff > 0.5);
      expect(hasSigDiffs).toBe(true);
    });

    it('should compute smaller differences for similar offers', () => {
      const offer1 = createMockOffer({
        salaryFrom: 15000,
        categoryId: 10,
        techStackNames: ['TypeScript', 'React'],
      });

      const offer2 = createMockOffer({
        salaryFrom: 16000,
        categoryId: 10, // Same category
        techStackNames: ['TypeScript', 'React', 'Node.js'], // Overlapping skills
      });

      const differences = extractFeatureDifferences(offer1, offer2);

      const avgDiff = differences.reduce((sum, d) => sum + d, 0) / differences.length;
      expect(avgDiff).toBeLessThan(0.5);
    });
  });

  describe('Welford\'s Algorithm', () => {
    describe('updateWelfordStats', () => {
      it('should update count, mean, and m2', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };

        stats = updateWelfordStats(stats, 10);
        expect(stats.count).toBe(1);
        expect(stats.mean).toBe(10);

        stats = updateWelfordStats(stats, 20);
        expect(stats.count).toBe(2);
        expect(stats.mean).toBe(15); // (10 + 20) / 2

        stats = updateWelfordStats(stats, 30);
        expect(stats.count).toBe(3);
        expect(stats.mean).toBe(20); // (10 + 20 + 30) / 3
      });

      it('should compute mean correctly for multiple values', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        const values = [1, 2, 3, 4, 5];

        for (const value of values) {
          stats = updateWelfordStats(stats, value);
        }

        expect(stats.mean).toBeCloseTo(3, 5); // (1+2+3+4+5)/5 = 3
      });

      it('should handle negative values', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };

        stats = updateWelfordStats(stats, -10);
        stats = updateWelfordStats(stats, 10);

        expect(stats.mean).toBeCloseTo(0, 5);
      });

      it('should maintain numerical stability for large datasets', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };

        // Add 1000 values around mean 0.5
        for (let i = 0; i < 1000; i++) {
          stats = updateWelfordStats(stats, 0.5 + (Math.random() - 0.5) * 0.1);
        }

        expect(stats.count).toBe(1000);
        expect(stats.mean).toBeCloseTo(0.5, 1);
      });
    });

    describe('getVariance', () => {
      it('should return 0 for empty stats', () => {
        const stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        expect(getVariance(stats)).toBe(0);
      });

      it('should compute variance correctly', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        const values = [2, 4, 4, 4, 5, 5, 7, 9]; // variance = 4

        for (const value of values) {
          stats = updateWelfordStats(stats, value);
        }

        const variance = getVariance(stats);
        expect(variance).toBeCloseTo(4, 1);
      });

      it('should return 0 for constant values', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };

        for (let i = 0; i < 10; i++) {
          stats = updateWelfordStats(stats, 5);
        }

        expect(getVariance(stats)).toBeCloseTo(0, 5);
      });
    });

    describe('getVarianceSample', () => {
      it('should return 0 for less than 2 samples', () => {
        const stats: WelfordStats = { count: 1, mean: 5, m2: 0 };
        expect(getVarianceSample(stats)).toBe(0);
      });

      it('should use Bessel correction (n-1)', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        const values = [2, 4, 4, 4, 5, 5, 7, 9];

        for (const value of values) {
          stats = updateWelfordStats(stats, value);
        }

        const populationVariance = getVariance(stats);
        const sampleVariance = getVarianceSample(stats);

        expect(sampleVariance).toBeGreaterThan(populationVariance);
      });
    });

    describe('getStandardDeviation', () => {
      it('should return square root of variance', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        const values = [2, 4, 6, 8];

        for (const value of values) {
          stats = updateWelfordStats(stats, value);
        }

        const variance = getVariance(stats);
        const stdDev = getStandardDeviation(stats);

        expect(stdDev).toBeCloseTo(Math.sqrt(variance), 5);
      });

      it('should return 0 for less than 2 samples', () => {
        const stats: WelfordStats = { count: 1, mean: 5, m2: 0 };
        expect(getStandardDeviation(stats)).toBe(0);
      });
    });

    describe('getCoefficientOfVariation', () => {
      it('should compute CV = stdDev / mean', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        const values = [10, 12, 14, 16]; // mean=13, stdDev≈2.236

        for (const value of values) {
          stats = updateWelfordStats(stats, value);
        }

        const cv = getCoefficientOfVariation(stats);
        expect(cv).toBeGreaterThan(0);
        expect(cv).toBeLessThan(0.3); // ~0.172
      });

      it('should return 0 for less than 2 samples', () => {
        const stats: WelfordStats = { count: 1, mean: 5, m2: 0 };
        expect(getCoefficientOfVariation(stats)).toBe(0);
      });

      it('should return 0 when mean is near zero', () => {
        let stats: WelfordStats = { count: 0, mean: 0, m2: 0 };
        stats = updateWelfordStats(stats, 0.0001);
        stats = updateWelfordStats(stats, -0.0001);

        expect(getCoefficientOfVariation(stats)).toBe(0);
      });
    });
  });

  describe('Normalization Stats', () => {
    describe('createInitialNormalizationStats', () => {
      it('should create stats for all features', () => {
        const stats = createInitialNormalizationStats();

        expect(stats.featureStats).toHaveLength(FEATURE_COUNT);
        expect(stats.meanDistances).toHaveLength(FEATURE_COUNT);
      });

      it('should initialize with default Welford stats', () => {
        const stats = createInitialNormalizationStats();

        stats.featureStats.forEach(featureStats => {
          expect(featureStats.count).toBe(0);
          // DEFAULT_WELFORD_STATS has mean: 0.5 as default prior
          expect(featureStats.mean).toBe(0.5);
          expect(featureStats.m2).toBe(0);
        });
      });

      it('should initialize mean distances to 0.5', () => {
        const stats = createInitialNormalizationStats();

        stats.meanDistances.forEach(meanDist => {
          expect(meanDist).toBe(0.5);
        });
      });
    });

    describe('updateNormalizationStats', () => {
      it('should update stats for each feature', () => {
        let stats = createInitialNormalizationStats();
        const offer = createMockOffer();
        const features = extractFeatureVector(offer, preferences);

        stats = updateNormalizationStats(stats, features);

        stats.featureStats.forEach(featureStats => {
          expect(featureStats.count).toBe(1);
        });
      });

      it('should accumulate stats across multiple offers', () => {
        let stats = createInitialNormalizationStats();
        const offers = createMockOfferBatch(5);

        for (const offer of offers) {
          const features = extractFeatureVector(offer, preferences);
          stats = updateNormalizationStats(stats, features);
        }

        stats.featureStats.forEach(featureStats => {
          expect(featureStats.count).toBe(5);
        });
      });

      it('should update mean distances', () => {
        let stats = createInitialNormalizationStats();
        const offer = createMockOffer();
        const features = extractFeatureVector(offer, preferences);

        stats = updateNormalizationStats(stats, features);

        expect(stats.meanDistances).toBeDefined();
        expect(stats.meanDistances).toHaveLength(FEATURE_COUNT);
      });
    });

    describe('buildNormalizationStatsFromOffers', () => {
      it('should build stats from multiple offers', () => {
        const offers = createMockOfferBatch(10);
        const { stats, features } = buildNormalizationStatsFromOffers(offers, preferences);

        expect(stats.featureStats).toHaveLength(FEATURE_COUNT);
        expect(features.size).toBe(10);
      });

      it('should create normalized feature vectors', () => {
        const offers = createMockOfferBatch(10);
        const { features } = buildNormalizationStatsFromOffers(offers, preferences);

        features.forEach(featureVector => {
          expect(featureVector.values).toHaveLength(FEATURE_COUNT);
          featureVector.values.forEach(value => {
            expect(isFinite(value)).toBe(true);
          });
        });
      });

      it('should accumulate stats from all offers', () => {
        const offers = createMockOfferBatch(20);
        const { stats } = buildNormalizationStatsFromOffers(offers, preferences);

        stats.featureStats.forEach(featureStats => {
          expect(featureStats.count).toBe(20);
        });
      });
    });

    describe('extractFeaturesForOffers', () => {
      it('should extract features for all offers', () => {
        const offers = createMockOfferBatch(5);
        const features = extractFeaturesForOffers(offers, preferences);

        expect(features.size).toBe(5);
      });

      it('should use normalization stats if provided', () => {
        const offers = createMockOfferBatch(10);
        const { stats } = buildNormalizationStatsFromOffers(offers, preferences);

        const newOffers = createMockOfferBatch(5);
        const features = extractFeaturesForOffers(newOffers, preferences, stats);

        expect(features.size).toBe(5);
      });

      it('should map offer IDs to feature vectors', () => {
        const offers = createMockOfferBatch(3);
        const features = extractFeaturesForOffers(offers, preferences);

        offers.forEach(offer => {
          expect(features.has(offer.id)).toBe(true);
          const featureVector = features.get(offer.id);
          expect(featureVector).toBeDefined();
          expect(featureVector!.values).toHaveLength(FEATURE_COUNT);
        });
      });
    });
  });
});
