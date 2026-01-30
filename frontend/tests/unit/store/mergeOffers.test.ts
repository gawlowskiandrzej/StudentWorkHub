import { describe, expect, it } from "vitest";
import { mergeOffers } from "@/store/OffersContext";
import { offer } from "@/types/list/Offer/offer";

const makeOffer = (id: number, title: string, salaryFrom?: number): offer => ({
  id,
  title,
  highlight: false,
  offerSource: "test",
  createdAt: "2024-01-01",
  company: "ACME",
  companyImageUrl: null,
  isRemote: false,
  level: "Junior",
  city: "City",
  country: "PL",
  salaryFrom,
  salaryTo: salaryFrom ? salaryFrom + 1000 : undefined,
  salaryCurrency: "PLN",
  street: null,
  latitude: null,
  longitude: null,
  tech: [],
  description: null,
  contractType: null,
  experienceLevel: null,
  employmentType: null,
  employmentMode: null,
  validTo: null,
  employmentSchedule: null,
  links: [],
});

describe("mergeOffers", () => {
  it("deduplicates by id and keeps the latest version", () => {
    const original = [makeOffer(1, "Old title", 5000), makeOffer(2, "Keep me")];
    const incoming = [makeOffer(1, "New title", 9000)];

    const result = mergeOffers(original, incoming);

    expect(result).toHaveLength(2);
    const updated = result.find(o => o.id === 1)!;
    expect(updated.title).toBe("New title");
    expect(updated.salaryFrom).toBe(9000);
    expect(result.find(o => o.id === 2)?.title).toBe("Keep me");
  });

  it("appends new ids while preserving insertion order for existing ones", () => {
    const base = [makeOffer(1, "First"), makeOffer(2, "Second")];
    const incoming = [makeOffer(3, "Third"), makeOffer(1, "First updated")];

    const result = mergeOffers(base, incoming);

    expect(result.map(o => o.id)).toEqual([1, 2, 3]);
    expect(result.find(o => o.id === 1)?.title).toBe("First updated");
  });
});
