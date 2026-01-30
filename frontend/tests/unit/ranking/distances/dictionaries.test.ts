import { describe, expect, it, beforeEach } from "vitest";
import {
  buildDictionariesFromOffers,
  getDictionaries,
  normalizeEmploymentType,
  calculateRelativeBenefitScore,
  resetDictionaries,
} from "@/lib/ranking/distances/dictionaries";
import { createMockOffer } from "@tests/utils/mockData";

describe("dynamic dictionaries", () => {
  beforeEach(() => {
    resetDictionaries();
  });

  it("aggregates categories, benefits, skills, languages and employment types", () => {
    const offers = [
      createMockOffer({
        benefits: ["Medicover", "Pizza"],
        techStackNames: ["TypeScript"],
        requiredLanguages: [{ language: "English", level: "B2" }],
        employmentTypes: ["UoP"],
        categoryName: "IT",
      }),
      createMockOffer({
        benefits: ["Pizza", "Gym"],
        techStackNames: ["React"],
        requiredLanguages: [{ language: "Polish", level: "C1" }],
        employmentTypes: ["Kontrakt B2B"],
        categoryName: "IT",
      }),
    ];

    buildDictionariesFromOffers(offers as any);
    const dict = getDictionaries();

    expect(dict.allBenefits.has("medicover")).toBe(true);
    expect(dict.allBenefits.has("pizza")).toBe(true);
    expect(dict.allLanguages.has("english")).toBe(true);
    expect(dict.allEmploymentTypes.has("uop")).toBe(true);
    expect(dict.allEmploymentTypes.has("kontrakt b2b")).toBe(true);
    expect(dict.allCategories.has("it")).toBe(true);
    expect(dict.allSkills.has("typescript")).toBe(true);
    expect(dict.offerCount).toBe(2);
  });

  it("normalizes employment type using aliases", () => {
    const offers = [createMockOffer({ employmentTypes: ["Umowa o pracę"] })];
    buildDictionariesFromOffers(offers as any);

    const normalized = normalizeEmploymentType("uop");
    expect(normalized).toBe("umowa o pracę");
  });

  it("benefit score scales with known benefits and offer count", () => {
    const offers = [createMockOffer({ benefits: ["A", "B"] }), createMockOffer({ benefits: ["B"] })];
    buildDictionariesFromOffers(offers as any);

    const score = calculateRelativeBenefitScore(new Set(["a", "b", "c"]));
    expect(score).toBeGreaterThan(0);
    expect(score).toBeLessThanOrEqual(1);
  });
});
