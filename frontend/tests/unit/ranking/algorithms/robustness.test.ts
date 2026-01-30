import { describe, it, expect } from 'vitest';
import { computeOfferScore } from '@/lib/ranking/algorithms/sgd';
import { createSGDTrainer } from '@/lib/ranking/algorithms/sgd';
import { FEATURE_COUNT } from '@/lib/ranking/types';

describe('Ranking Algorithm Robustness', () => {
  it('should handle NaN values in feature vector without crashing and return safe score', () => {
    const trainer = createSGDTrainer();
    const brokenFeatures = {
      values: new Array(FEATURE_COUNT).fill(NaN),
      metadata: {}
    };

    const score = computeOfferScore(trainer, brokenFeatures);

    expect(Number.isFinite(score)).toBe(true);
    expect(score).toBeGreaterThanOrEqual(0);
    expect(score).toBeLessThanOrEqual(1);
  });

  it('should handle Infinity in feature vector safely', () => {
    const trainer = createSGDTrainer();
    const brokenFeatures = {
      values: new Array(FEATURE_COUNT).fill(Infinity),
      metadata: {}
    };

    const score = computeOfferScore(trainer, brokenFeatures);

    expect(Number.isFinite(score)).toBe(true);
    expect(score).toBeGreaterThanOrEqual(0);
    expect(score).toBeLessThanOrEqual(1);
  });

  it('should handle weights becoming corrupted/NaN gracefully', () => {
    const trainer = createSGDTrainer();
    trainer.weights = new Array(FEATURE_COUNT).fill(NaN);
    
    const validFeatures = {
      values: new Array(FEATURE_COUNT).fill(0.5),
      metadata: {}
    };

    const score = computeOfferScore(trainer, validFeatures);

    expect(Number.isFinite(score)).toBe(true);
    expect(score).toBe(0.5); 
  });
});
