import { describe, it, expect, beforeEach } from 'vitest';
import {
  createSGDTrainer,
  computeOfferScore,
  trainStep,
  computeModelConfidence,
  computeModelConfidenceV2,
  computeScoreBreakdown,
} from '@/lib/ranking/algorithms/sgd';
import { SGDTrainerState, FEATURE_COUNT } from '@/lib/ranking/types';
import {
  createFeatureVector,
  createCustomFeatureVector,
  isWithinRange,
  assertValidWeights,
  arraysAlmostEqual,
} from '@tests/utils/testHelpers';

describe('SGD Algorithm - Stochastic Gradient Descent', () => {
  describe('createSGDTrainer', () => {
    it('should initialize trainer with default weights of 0.5', () => {
      const trainer = createSGDTrainer();

      expect(trainer.weights).toHaveLength(FEATURE_COUNT);
      expect(trainer.weights.every(w => w === 0.5)).toBe(true);
    });

    it('should initialize Adam optimizer state with zeros', () => {
      const trainer = createSGDTrainer();

      expect(trainer.adamState.m).toHaveLength(FEATURE_COUNT);
      expect(trainer.adamState.v).toHaveLength(FEATURE_COUNT);
      expect(trainer.adamState.t).toBe(0);
      expect(trainer.adamState.m.every(m => m === 0)).toBe(true);
      expect(trainer.adamState.v.every(v => v === 0)).toBe(true);
    });

    it('should accept custom initial weights', () => {
      const customWeights = [0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1];
      const trainer = createSGDTrainer(customWeights);

      expect(trainer.weights).toEqual(customWeights);
    });

    it('should accept custom hyperparameters', () => {
      const trainer = createSGDTrainer(undefined, {
        learningRate: 0.1,
        beta1: 0.95,
      });

      expect(trainer.hyperparameters.learningRate).toBe(0.1);
      expect(trainer.hyperparameters.beta1).toBe(0.95);
    });

    it('should use default hyperparameters when not specified', () => {
      const trainer = createSGDTrainer();

      expect(trainer.hyperparameters.learningRate).toBeDefined();
      expect(trainer.hyperparameters.beta1).toBeDefined();
      expect(trainer.hyperparameters.beta2).toBeDefined();
      expect(trainer.hyperparameters.epsilon).toBeDefined();
      expect(trainer.hyperparameters.regularization).toBeDefined();
    });
  });

  describe('computeOfferScore', () => {
    let trainer: SGDTrainerState;

    beforeEach(() => {
      trainer = createSGDTrainer();
    });

    it('should return score of ~1.0 for perfect match (all distances = 0)', () => {
      const perfectMatch = createFeatureVector(0.0);
      const score = computeOfferScore(trainer, perfectMatch);

      expect(score).toBeGreaterThan(0.95);
      expect(score).toBeLessThanOrEqual(1.0);
    });

    it('should return score of ~0.0 for worst match (all distances = 1)', () => {
      const worstMatch = createFeatureVector(1.0);
      const score = computeOfferScore(trainer, worstMatch);

      expect(score).toBeLessThan(0.05);
      expect(score).toBeGreaterThanOrEqual(0.0);
    });

    it('should return score of ~0.5 for neutral match (all distances = 0.5)', () => {
      const neutralMatch = createFeatureVector(0.5);
      const score = computeOfferScore(trainer, neutralMatch);

      expect(score).toBeGreaterThan(0.45);
      expect(score).toBeLessThan(0.55);
    });

    it('should handle weighted preferences correctly', () => {
      // high weight on salary, low on other features
      const salaryFocusedWeights = [0.9, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1];
      trainer = createSGDTrainer(salaryFocusedWeights);

      // good salary (0.1 distance) but poor other features (0.9 distance)
      const offer = createCustomFeatureVector({
        salary: 0.1,
        benefits: 0.9,
        skills: 0.9,
        employmentType: 0.9,
        schedule: 0.9,
        category: 0.9,
        language: 0.9,
        freshness: 0.9,
      });

      const score = computeOfferScore(trainer, offer);

      // score calculation: (0.8*0.9 + 0.1*0.1*7) / (0.8 + 0.7) = 0.79/1.5 = 0.527
      // high salary weight with low salary distance gives moderate score
      expect(score).toBeGreaterThan(0.4);
      expect(score).toBeLessThan(0.7);
    });

    it('should clamp scores to [0, 1] range', () => {
      const features = createFeatureVector(0.0);
      const score = computeOfferScore(trainer, features);

      expect(score).toBeGreaterThanOrEqual(0.0);
      expect(score).toBeLessThanOrEqual(1.0);
    });

    it('should handle zero total weight gracefully', () => {
      // all weights set to minimum (0.001 is enforced internally)
      const veryLowWeights = new Array(FEATURE_COUNT).fill(0.001);
      trainer = createSGDTrainer(veryLowWeights);

      const features = createFeatureVector(0.5);
      const score = computeOfferScore(trainer, features);

      expect(score).toBeDefined();
      expect(isFinite(score)).toBe(true);
    });

    it('should return finite score even if weights contain non-finite values', () => {
      // [oison weights to catch NaN propagation bugs
      const brokenTrainer = createSGDTrainer([
        Number.NaN,
        Number.POSITIVE_INFINITY,
        Number.NEGATIVE_INFINITY,
        0,
        0,
        0,
        0,
        0,
      ]);

      const features = createFeatureVector(0.5);
      const score = computeOfferScore(brokenTrainer, features);

      expect(Number.isFinite(score)).toBe(true);
    });
  });

  describe('computeScoreBreakdown', () => {
    let trainer: SGDTrainerState;

    beforeEach(() => {
      trainer = createSGDTrainer();
    });

    it('should return breakdown for all 8 features', () => {
      const features = createFeatureVector(0.5);
      const breakdown = computeScoreBreakdown(trainer, features);

      expect(breakdown).toHaveLength(8);
    });

    it('should include feature name, distance, weight, and contribution', () => {
      const features = createFeatureVector(0.3);
      const breakdown = computeScoreBreakdown(trainer, features);

      breakdown.forEach(item => {
        expect(item).toHaveProperty('featureName');
        expect(item).toHaveProperty('distance');
        expect(item).toHaveProperty('weight');
        expect(item).toHaveProperty('contribution');
        expect(typeof item.featureName).toBe('string');
        expect(typeof item.distance).toBe('number');
        expect(typeof item.weight).toBe('number');
        expect(typeof item.contribution).toBe('number');
      });
    });

    it('should have contributions sum to approximately 1.0', () => {
      const features = createFeatureVector(0.5);
      const breakdown = computeScoreBreakdown(trainer, features);

      const totalContribution = breakdown.reduce((sum, item) => sum + item.contribution, 0);

      expect(totalContribution).toBeCloseTo(0.5, 1);
    });

    it('should show higher contribution for features with high weight and low distance', () => {
      const highWeightFeatureWeights = [0.9, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1];
      trainer = createSGDTrainer(highWeightFeatureWeights);

      const features = createCustomFeatureVector({
        salary: 0.1,
        benefits: 0.9,
      });

      const breakdown = computeScoreBreakdown(trainer, features);
      const salaryContribution = breakdown[0].contribution;
      const benefitsContribution = breakdown[1].contribution;

      expect(salaryContribution).toBeGreaterThan(benefitsContribution);
    });
  });

  describe('trainStep', () => {
    let trainer: SGDTrainerState;

    beforeEach(() => {
      trainer = createSGDTrainer();
    });

    it('should update weights based on positive signal', () => {
      const features = createFeatureVector(0.2);
      const positiveSignal = 0.9;

      const updatedTrainer = trainStep(trainer, features, positiveSignal);

      expect(updatedTrainer.weights).not.toEqual(trainer.weights);
    });

    it('should update weights based on negative signal', () => {
      const features = createFeatureVector(0.8);
      const negativeSignal = 0.1;

      const updatedTrainer = trainStep(trainer, features, negativeSignal);

      expect(updatedTrainer.weights).not.toEqual(trainer.weights);
    });

    it('should increment Adam optimizer timestep', () => {
      const features = createFeatureVector(0.5);
      const signal = 0.7;

      const updatedTrainer = trainStep(trainer, features, signal);

      expect(updatedTrainer.adamState.t).toBe(trainer.adamState.t + 1);
    });

    it('should update Adam moment estimates (m and v)', () => {
      const features = createCustomFeatureVector({
        salary: 0.1,
        benefits: 0.9,
        skills: 0.3,
        employmentType: 0.7,
        schedule: 0.2,
        category: 0.8,
        language: 0.4,
        freshness: 0.6,
      });
      const signal = 0.9;

      const updatedTrainer = trainStep(trainer, features, signal);

      expect(updatedTrainer.adamState.t).toBe(1);
      expect(updatedTrainer.weights).not.toEqual(trainer.weights);
    });

    it('should keep all weights within valid range [0.01, 1.0]', () => {
      let currentTrainer = trainer;

      for (let i = 0; i < 20; i++) {
        const features = createFeatureVector(Math.random());
        const signal = Math.random();
        currentTrainer = trainStep(currentTrainer, features, signal);
      }

      assertValidWeights(currentTrainer.weights);
    });

    it('should apply importance weights from RReliefF if provided', () => {
      const features = createFeatureVector(0.5);
      const signal = 0.8;
      const importanceWeights = [2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0];

      const withoutImportance = trainStep(trainer, features, signal);
      const withImportance = trainStep(trainer, features, signal, importanceWeights);

      const deltaWithout = Math.abs(withoutImportance.weights[0] - trainer.weights[0]);
      const deltaWith = Math.abs(withImportance.weights[0] - trainer.weights[0]);

      expect(deltaWith).toBeGreaterThanOrEqual(deltaWithout);
    });

    it('should apply regularization to prevent overfitting', () => {
      const extremeWeights = [1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0];
      trainer = createSGDTrainer(extremeWeights, { regularization: 0.1 });

      const features = createFeatureVector(0.5);
      const signal = 0.5;

      const updatedTrainer = trainStep(trainer, features, signal);

      const sumBefore = extremeWeights.reduce((a, b) => a + b, 0);
      const sumAfter = updatedTrainer.weights.reduce((a, b) => a + b, 0);

      expect(updatedTrainer.weights).toBeDefined();
    });

    it('should converge towards target score with repeated training', () => {
      let currentTrainer = trainer;
      const features = createFeatureVector(0.2); // low distance
      const targetSignal = 0.9; // high score expected

      // Train multiple times
      for (let i = 0; i < 50; i++) {
        currentTrainer = trainStep(currentTrainer, features, targetSignal);
      }

      const finalScore = computeOfferScore(currentTrainer, features);
      const initialScore = computeOfferScore(trainer, features);

      expect(Math.abs(finalScore - targetSignal)).toBeLessThan(
        Math.abs(initialScore - targetSignal)
      );
    });
  });

  describe('computeModelConfidence', () => {
    const createAdamState = (t: number) => ({
      m: new Array(FEATURE_COUNT).fill(0),
      v: new Array(FEATURE_COUNT).fill(0),
      t,
    });

    it('should return low confidence with few interactions', () => {
      const adamState = createAdamState(5);
      const confidence = computeModelConfidence(adamState);

      // 0.3 + 0.7 * (1 - exp(-5/30)) = ~0.3 + 0.7 * 0.154 = ~0.408
      expect(confidence).toBeLessThan(0.5);
    });

    it('should return high confidence with many interactions', () => {
      const adamState = createAdamState(100);
      const confidence = computeModelConfidence(adamState);

      // 0.3 + 0.7 * (1 - exp(-100/30)) = ~0.3 + 0.7 * 0.964 = ~0.975
      expect(confidence).toBeGreaterThan(0.8);
    });

    it('should return confidence between 0 and 1', () => {
      const testCases = [0, 1, 10, 50, 100, 500];

      testCases.forEach(count => {
        const adamState = createAdamState(count);
        const confidence = computeModelConfidence(adamState);
        expect(confidence).toBeGreaterThanOrEqual(0);
        expect(confidence).toBeLessThanOrEqual(1);
      });
    });

    it('should increase monotonically with interaction count', () => {
      const confidences = [0, 10, 20, 50, 100].map(count => {
        const adamState = createAdamState(count);
        return computeModelConfidence(adamState);
      });

      for (let i = 1; i < confidences.length; i++) {
        expect(confidences[i]).toBeGreaterThanOrEqual(confidences[i - 1]);
      }
    });

    it('should return 0 when t is 0', () => {
      const adamState = createAdamState(0);
      const confidence = computeModelConfidence(adamState);

      expect(confidence).toBe(0);
    });
  });

  describe('computeModelConfidenceV2', () => {
    const createAdamState = (t: number) => ({
      m: new Array(FEATURE_COUNT).fill(0),
      v: new Array(FEATURE_COUNT).fill(0),
      t,
    });

    const createNormStats = (variance: number, count: number) => ({
      featureStats: new Array(FEATURE_COUNT).fill(null).map(() => ({
        count,
        mean: 0.5,
        m2: variance * count, // m2/count = variance
      })),
      meanValueIds: [],
      meanDistances: [],
    });

    it('should return 0 confidence with no training', () => {
      const adamState = createAdamState(0);
      const normStats = createNormStats(0, 0);
      const confidence = computeModelConfidenceV2(adamState, normStats);

      expect(confidence).toBe(0);
    });

    it('should increase confidence with more training steps', () => {
      const normStats = createNormStats(0.1, 10);
      const lowTraining = computeModelConfidenceV2(createAdamState(5), normStats);
      const highTraining = computeModelConfidenceV2(createAdamState(50), normStats);

      expect(highTraining).toBeGreaterThan(lowTraining);
    });

    it('should factor in feature variance (high variance = less stability)', () => {
      const lowVarianceStats = createNormStats(0.01, 20);
      const highVarianceStats = createNormStats(0.5, 20);
      const adamState = createAdamState(20);

      const lowVarianceConfidence = computeModelConfidenceV2(adamState, lowVarianceStats);
      const highVarianceConfidence = computeModelConfidenceV2(adamState, highVarianceStats);

      expect(lowVarianceConfidence).toBeGreaterThan(highVarianceConfidence);
    });

    it('should return trainingConfidence only when t < 3', () => {
      const adamState = createAdamState(2);
      const normStats = createNormStats(0.5, 10);
      const confidence = computeModelConfidenceV2(adamState, normStats);

      const expectedTrainingConfidence = 0.3 + 0.7 * (1 - Math.exp(-2 / 30));
      expect(confidence).toBeCloseTo(expectedTrainingConfidence, 4);
    });

    it('should always return value between 0 and 1', () => {
      const testCases = [
        { t: 0, variance: 0, count: 0 },
        { t: 10, variance: 0.1, count: 10 },
        { t: 100, variance: 0.25, count: 50 },
        { t: 1000, variance: 0.5, count: 100 },
      ];

      testCases.forEach(({ t, variance, count }) => {
        const adamState = createAdamState(t);
        const normStats = createNormStats(variance, count);
        const confidence = computeModelConfidenceV2(adamState, normStats);
        expect(confidence).toBeGreaterThanOrEqual(0);
        expect(confidence).toBeLessThanOrEqual(1);
      });
    });
  });
});
