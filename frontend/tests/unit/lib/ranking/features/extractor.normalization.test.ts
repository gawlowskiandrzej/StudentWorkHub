import { describe, expect, it } from "vitest";
import { extractFeatureVector, createInitialNormalizationStats } from "@/lib/ranking/features/extractor";
import { createMockOffer, createMockPreferences } from "@tests/utils/mockData";

describe("extractFeatureVector normalization", () => {
  it("clamps normalized values to [0,1] and preserves feature count", () => {
    const offer = createMockOffer({ salaryFrom: 0, salaryTo: 0 });
    const prefs = createMockPreferences({ salaryFrom: 100000, salaryTo: 200000 });
    const stats = createInitialNormalizationStats();

    const features = extractFeatureVector(offer as any, prefs as any, stats);

    expect(features.values.length).toBe(features.raw && Object.keys(features.raw).length);
    expect(features.values.every(v => v >= 0 && v <= 1)).toBe(true);
  });
});
