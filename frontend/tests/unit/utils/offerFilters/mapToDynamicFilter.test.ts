import { describe, expect, it } from "vitest";
import { mapApiFilters } from "@/utils/offerFilters/mapToDynamicFilter";

const makeApi = (levels: string[]) => ({
  experienceLevels: levels,
});

describe("mapApiFilters", () => {
  it("returns empty list when api payload is missing or empty", () => {
    expect(mapApiFilters(undefined)).toEqual([]);
    expect(mapApiFilters(makeApi([]))).toEqual([]);
  });

  it("sorts experience levels by dictionary min_months", () => {
    const api = makeApi(["Ekspert", "Zaawansowany", "Początkujący"]);
    const [experienceFilter] = mapApiFilters(api);
    const labels = experienceFilter?.items?.map(item => item.label);

    expect(labels).toEqual(["Początkujący", "Zaawansowany", "Ekspert"]);
  });

  it("keeps unknown experience levels but places them after known ones", () => {
    const api = makeApi(["Zaawansowany", "Nowy" as string, "Początkujący"]);
    const [experienceFilter] = mapApiFilters(api);
    const labels = experienceFilter?.items?.map(item => item.label);

    expect(labels).toEqual(["Początkujący", "Zaawansowany", "Nowy"]);
  });

  it("handles object with undefined experienceLevels gracefully", () => {
    const api = { experienceLevels: undefined } as any; 
    expect(mapApiFilters(api)).toEqual([]);
  });
});
