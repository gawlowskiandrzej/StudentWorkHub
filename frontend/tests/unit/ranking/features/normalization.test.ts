import { describe, expect, it } from "vitest";
import {
  createInitialNormalizationStats,
  updateNormalizationStats,
  getVariance,
  getCoefficientOfVariation,
} from "@/lib/ranking/features/extractor";
import { FEATURE_COUNT } from "@/lib/ranking/types";

const makeVector = (value: number) => ({
  values: new Array(FEATURE_COUNT).fill(value),
  raw: {} as any,
});

describe("normalization stats", () => {
  it("updates Welford stats and mean distances", () => {
    const stats = createInitialNormalizationStats();
    const vec = makeVector(0.25);

    const updated = updateNormalizationStats(stats, vec);

    expect(updated.featureStats[0].count).toBe(1);
    expect(updated.featureStats[0].mean).toBeCloseTo(0.25, 5);
    expect(updated.meanDistances[0]).toBeCloseTo(0.25, 5);
  });

  it("computes variance and coefficient of variation", () => {
    const stats = createInitialNormalizationStats();
    const v1 = updateNormalizationStats(stats, makeVector(0.2));
    const v2 = updateNormalizationStats(v1, makeVector(0.8));

    const variance = getVariance(v2.featureStats[0]);
    expect(variance).toBeGreaterThan(0);

    const cov = getCoefficientOfVariation(v2.featureStats[0]);
    expect(cov).toBeGreaterThan(0);
  });
});
