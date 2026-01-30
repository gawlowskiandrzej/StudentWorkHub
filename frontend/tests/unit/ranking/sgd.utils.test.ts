import { describe, expect, it } from "vitest";
import { normalizeWeights, scaleSignalByViewTime, serializeSGDState, deserializeSGDState } from "@/lib/ranking/algorithms/sgd";

describe("sgd utility functions", () => {
  it("returns zero signal for very short views", () => {
    const result = scaleSignalByViewTime(1, 500, 2000, 30000);
    expect(result).toBe(0);
  });

  it("caps signal at base value for long views", () => {
    const result = scaleSignalByViewTime(0.8, 60000, 2000, 30000);
    expect(result).toBeCloseTo(0.8, 5);
  });

  it("normalizes zero-sum weights to uniform distribution", () => {
    const normalized = normalizeWeights([0, 0]);
    expect(normalized).toEqual([0.5, 0.5]);
  });

  it("serializes and deserializes trainer state losslessly", () => {
    const serialized = serializeSGDState({
      weights: [0.1, 0.9],
      adamState: { m: [0.2, 0.3], v: [0.4, 0.5], t: 3 },
      hyperparameters: {} as any,
    });

    const restored = deserializeSGDState({
      vector: serialized.vector,
      adamT: serialized.adamT,
      adamM: serialized.adamM,
      adamV: serialized.adamV,
    });

    expect(restored.weights).toEqual([0.1, 0.9]);
    expect(restored.adamState).toMatchObject({ t: 3, m: [0.2, 0.3], v: [0.4, 0.5] });
  });
});
