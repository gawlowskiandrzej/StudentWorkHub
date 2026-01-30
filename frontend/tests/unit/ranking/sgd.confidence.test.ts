import { describe, expect, it } from "vitest";
import {
  computeModelConfidence,
  computeModelConfidenceV2,
  computeModelConfidenceWithBreakdown,
  resetAdamState,
} from "@/lib/ranking/algorithms/sgd";
import { FEATURE_COUNT, NormalizationStats } from "@/lib/ranking/types";

const makeStats = (variance: number, count: number): NormalizationStats => {
  const featureStats = Array(FEATURE_COUNT).fill(null).map(() => ({
    count,
    mean: 0.5,
    m2: variance * count,
  }));
  return {
    featureStats,
    meanDistances: new Array(FEATURE_COUNT).fill(0.5),
    meanValueIds: [],
  };
};

describe("model confidence", () => {
  it("grows with training steps", () => {
    const low = computeModelConfidence({ m: [], v: [], t: 0 });
    const high = computeModelConfidence({ m: [], v: [], t: 20 });

    expect(low).toBe(0);
    expect(high).toBeGreaterThan(low);
  });

  it("penalizes high variance in V2 and rewards stability", () => {
    const adam = { m: [], v: [], t: 10 };
    const noisyStats = makeStats(1, 10);
    const stableStats = makeStats(0.01, 10);

    const noisy = computeModelConfidenceV2(adam, noisyStats);
    const stable = computeModelConfidenceV2(adam, stableStats);

    expect(stable).toBeGreaterThan(noisy);
  });

  it("returns detailed breakdown consistent with V2", () => {
    const adam = { m: [], v: [], t: 15 };
    const stats = makeStats(0.05, 20);

    const breakdown = computeModelConfidenceWithBreakdown(adam, stats);
    const total = computeModelConfidenceV2(adam, stats);

    expect(breakdown.total).toBeCloseTo(total, 5);
    expect(breakdown.training).toBeGreaterThan(0);
    expect(breakdown.stability).toBeGreaterThan(0);
    expect(breakdown.observations).toBeGreaterThan(0);
  });

  it("resets Adam state to t=0", () => {
    const trainer = {
      weights: [0.2, 0.8],
      adamState: { m: [1, 1], v: [1, 1], t: 5 },
      hyperparameters: {} as any,
    };

    const reset = resetAdamState(trainer);

    expect(reset.adamState.t).toBe(0);
    expect(reset.adamState.m.every(v => v === 0)).toBe(true);
    expect(reset.adamState.v.every(v => v === 0)).toBe(true);
  });
});
