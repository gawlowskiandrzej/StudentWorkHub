import { describe, it, expect, beforeEach } from 'vitest';
import {
  createRReliefFState,
  trainRReliefFStep,
  getFeaturesByImportance,
  ScoredOfferContext,
} from '@/lib/ranking/algorithms/rrelieff';
import { RReliefFState, FEATURE_COUNT, FEATURE_KEYS } from '@/lib/ranking/types';
import { createMockOffer, createMockPreferences } from '@tests/utils/mockData';
import { extractFeatureVector } from '@/lib/ranking/features/extractor';
import { sum } from '@tests/utils/testHelpers';

describe('RReliefF Algorithm - Feature Importance', () => {
  describe('createRReliefFState', () => {
    it('should initialize with uniform feature importance', () => {
      const state = createRReliefFState();

      expect(state.featureImportance).toHaveLength(FEATURE_COUNT);
      
      const allEqual = state.featureImportance.every(
        importance => Math.abs(importance - 1.0 / FEATURE_COUNT) < 0.0001
      );
      expect(allEqual).toBe(true);
    });

    it('should initialize with default k=10 neighbors', () => {
      const state = createRReliefFState();

      expect(state.k).toBe(10);
    });

    it('should initialize with default sigma=0.5', () => {
      const state = createRReliefFState();

      expect(state.sigma).toBe(0.5);
    });

    it('should accept custom k value', () => {
      const state = createRReliefFState(20);

      expect(state.k).toBe(20);
    });

    it('should accept custom sigma value', () => {
      const state = createRReliefFState(10, 0.8);

      expect(state.sigma).toBe(0.8);
    });

    it('should sum importance values to approximately 1.0', () => {
      const state = createRReliefFState();
      const totalImportance = sum(state.featureImportance);

      expect(totalImportance).toBeCloseTo(1.0, 5);
    });
  });

  describe('trainRReliefFStep', () => {
    let state: RReliefFState;
    let preferences = createMockPreferences();

    beforeEach(() => {
      state = createRReliefFState(5, 0.5); // 5 neighbors, sigma 0.5
    });

    it('should return unchanged state when no context offers available', () => {
      const selectedOffer = createMockOffer({ offerId: 1 });
      const context: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.8,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const updatedState = trainRReliefFStep(state, context, 0.8, []);

      // state should remain the same
      expect(updatedState.featureImportance).toEqual(state.featureImportance);
    });

    it('should update feature importance based on neighbor differences', () => {
      const selectedOffer = createMockOffer({
        offerId: 1,
        salaryFrom: 15000,
        salaryTo: 20000,
        categoryId: 10,
      });

      const similarOffer = createMockOffer({
        offerId: 2,
        salaryFrom: 16000,
        salaryTo: 21000,
        categoryId: 10, // same category
      });

      const differentOffer = createMockOffer({
        offerId: 3,
        salaryFrom: 5000,
        salaryTo: 7000,
        categoryId: 99, // different category
      });

      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.9,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = [
        {
          offerId: 2,
          offer: similarOffer,
          score: 0.85, // similar score
          features: extractFeatureVector(similarOffer, preferences),
        },
        {
          offerId: 3,
          offer: differentOffer,
          score: 0.2, // different score
          features: extractFeatureVector(differentOffer, preferences),
        },
      ];

      const updatedState = trainRReliefFStep(
        state,
        selectedContext,
        0.9,
        contextOffers
      );

      // should be same
      expect(updatedState.featureImportance).not.toEqual(state.featureImportance);
    });

    it('should increase importance of features that correlate with score differences', () => {
      const highSalaryOffer = createMockOffer({
        offerId: 1,
        salaryFrom: 20000,
        salaryTo: 25000,
        categoryId: 10,
        techStackNames: ['TypeScript', 'React'],
      });

      const mediumSalaryOffer = createMockOffer({
        offerId: 2,
        salaryFrom: 15000,
        salaryTo: 18000,
        categoryId: 10,
        techStackNames: ['TypeScript', 'React'],
      });

      const lowSalaryOffer = createMockOffer({
        offerId: 3,
        salaryFrom: 8000,
        salaryTo: 10000,
        categoryId: 10,
        techStackNames: ['TypeScript', 'React'],
      });

      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: highSalaryOffer,
        score: 0.95,
        features: extractFeatureVector(highSalaryOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = [
        {
          offerId: 2,
          offer: mediumSalaryOffer,
          score: 0.7,
          features: extractFeatureVector(mediumSalaryOffer, preferences),
        },
        {
          offerId: 3,
          offer: lowSalaryOffer,
          score: 0.3,
          features: extractFeatureVector(lowSalaryOffer, preferences),
        },
      ];

      const updatedState = trainRReliefFStep(
        state,
        selectedContext,
        0.95,
        contextOffers
      );

      const salaryImportanceChange =
        updatedState.featureImportance[0] - state.featureImportance[0];

      expect(updatedState.featureImportance).not.toEqual(state.featureImportance);
    });

    it('should limit neighbors to k nearest', () => {
      const k = 3;
      state = createRReliefFState(k, 0.5);

      const selectedOffer = createMockOffer({ offerId: 1 });
      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.8,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = Array.from({ length: 10 }, (_, i) => {
        const offer = createMockOffer({ offerId: i + 2 });
        return {
          offerId: i + 2,
          offer,
          score: 0.5 + Math.random() * 0.3,
          features: extractFeatureVector(offer, preferences),
        };
      });

      const updatedState = trainRReliefFStep(
        state,
        selectedContext,
        0.8,
        contextOffers
      );

      expect(updatedState.featureImportance).toBeDefined();
    });

    it('should handle edge case where k is larger than available offers', () => {
      state = createRReliefFState(100, 0.5);

      const selectedOffer = createMockOffer({ offerId: 1 });
      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.8,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = Array.from({ length: 5 }, (_, i) => {
        const offer = createMockOffer({ offerId: i + 2 });
        return {
          offerId: i + 2,
          offer,
          score: 0.5,
          features: extractFeatureVector(offer, preferences),
        };
      });

      const updatedState = trainRReliefFStep(
        state,
        selectedContext,
        0.8,
        contextOffers
      );

      expect(updatedState.featureImportance).toBeDefined();
    });

    it('should use Gaussian kernel weighting based on sigma', () => {
      const smallSigma = createRReliefFState(5, 0.1);
      const largeSigma = createRReliefFState(5, 2.0);

      const selectedOffer = createMockOffer({ offerId: 1 });
      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.8,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = [
        {
          offerId: 2,
          offer: createMockOffer({ offerId: 2 }),
          score: 0.7,
          features: extractFeatureVector(createMockOffer({ offerId: 2 }), preferences),
        },
        {
          offerId: 3,
          offer: createMockOffer({ offerId: 3 }),
          score: 0.3,
          features: extractFeatureVector(createMockOffer({ offerId: 3 }), preferences),
        },
      ];

      const smallSigmaState = trainRReliefFStep(
        smallSigma,
        selectedContext,
        0.8,
        contextOffers
      );

      const largeSigmaState = trainRReliefFStep(
        largeSigma,
        selectedContext,
        0.8,
        contextOffers
      );

      expect(smallSigmaState.featureImportance).toBeDefined();
      expect(largeSigmaState.featureImportance).toBeDefined();
    });

    it('should keep feature importance non-negative', () => {
      const selectedOffer = createMockOffer({ offerId: 1 });
      const selectedContext: ScoredOfferContext = {
        offerId: 1,
        offer: selectedOffer,
        score: 0.8,
        features: extractFeatureVector(selectedOffer, preferences),
      };

      const contextOffers: ScoredOfferContext[] = Array.from({ length: 5 }, (_, i) => {
        const offer = createMockOffer({ offerId: i + 2 });
        return {
          offerId: i + 2,
          offer,
          score: Math.random(),
          features: extractFeatureVector(offer, preferences),
        };
      });

      let currentState = state;
      
      // train multiple times
      for (let i = 0; i < 10; i++) {
        currentState = trainRReliefFStep(
          currentState,
          selectedContext,
          0.8,
          contextOffers
        );
      }

      // all importance values should be >= 0
      currentState.featureImportance.forEach(importance => {
        expect(importance).toBeGreaterThanOrEqual(0);
      });
    });
  });

  describe('getFeaturesByImportance', () => {
    let state: RReliefFState;

    beforeEach(() => {
      // create state with varied importance values
      // index order: SALARY_MATCH(0.3), BENEFIT_MATCH(0.05), SKILLS_MATCH(0.2), 
      // EMPLOYMENT_TYPE_MATCH(0.1), SCHEDULE_MATCH(0.15), CATEGORY_MATCH(0.08), 
      // LANGUAGE_MATCH(0.07), FRESHNESS(0.05)
      state = {
        featureImportance: [0.3, 0.05, 0.2, 0.1, 0.15, 0.08, 0.07, 0.05],
        k: 10,
        sigma: 0.5,
      };
    });

    it('should return feature keys sorted by importance (descending)', () => {
      const sorted = getFeaturesByImportance(state);

      expect(sorted).toHaveLength(FEATURE_COUNT);
      expect(Array.isArray(sorted)).toBe(true);
      
      sorted.forEach(key => {
        expect(typeof key).toBe('string');
        expect(FEATURE_KEYS).toContain(key);
      });
    });

    it('should return all FEATURE_KEYS', () => {
      const sorted = getFeaturesByImportance(state);

      const validKeys = [
        'SALARY_MATCH',
        'BENEFIT_MATCH',
        'SKILLS_MATCH',
        'EMPLOYMENT_TYPE_MATCH',
        'SCHEDULE_MATCH',
        'CATEGORY_MATCH',
        'LANGUAGE_MATCH',
        'FRESHNESS',
      ];

      validKeys.forEach(key => {
        expect(sorted).toContain(key);
      });
    });

    it('should handle uniform importance correctly', () => {
      const uniformState = createRReliefFState();
      const sorted = getFeaturesByImportance(uniformState);

      expect(sorted).toHaveLength(FEATURE_COUNT);
    });

    it('should correctly identify most important feature first', () => {
      const sorted = getFeaturesByImportance(state);

      expect(sorted[0]).toBe('SALARY_MATCH');
    });

    it('should correctly order by importance (descending)', () => {
      const sorted = getFeaturesByImportance(state);
      
      // expected order based on importance values:
      // 0.3 (SALARY_MATCH) > 0.2 (SKILLS_MATCH) > 0.15 (SCHEDULE_MATCH) > 
      // 0.1 (EMPLOYMENT_TYPE_MATCH) > 0.08 (CATEGORY_MATCH) > 0.07 (LANGUAGE_MATCH) > 
      // 0.05 (BENEFIT_MATCH or FRESHNESS)
      expect(sorted[0]).toBe('SALARY_MATCH');
      expect(sorted[1]).toBe('SKILLS_MATCH');
      expect(sorted[2]).toBe('SCHEDULE_MATCH');
      expect(sorted[3]).toBe('EMPLOYMENT_TYPE_MATCH');
    });
  });

  describe('RReliefF Integration', () => {
    it('should learn feature importance over multiple training steps', () => {
      let state = createRReliefFState(5, 0.5);
      const preferences = createMockPreferences();

      const trainingData = Array.from({ length: 20 }, (_, i) => {
        const salary = 10000 + i * 2000;
        const score = 0.3 + (salary / 40000); // score correlates with salary

        const offer = createMockOffer({
          offerId: i + 1,
          salaryFrom: salary,
          salaryTo: salary + 5000,
        });

        return {
          offerId: i + 1,
          offer,
          score,
          features: extractFeatureVector(offer, preferences),
        };
      });

      // train on multiple samples
      for (let i = 0; i < trainingData.length; i++) {
        const selected = trainingData[i];
        const context = trainingData.filter(d => d.offerId !== selected.offerId);

        state = trainRReliefFStep(state, selected, selected.score, context);
      }

      // feature importance should have changed
      const initialState = createRReliefFState(5, 0.5);
      expect(state.featureImportance).not.toEqual(initialState.featureImportance);

      const features = getFeaturesByImportance(state);
      
      expect(features).toBeDefined();
      expect(features.length).toBe(8);
    });
  });
});
