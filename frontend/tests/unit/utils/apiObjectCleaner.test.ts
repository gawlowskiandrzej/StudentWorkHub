import { describe, expect, it } from "vitest";
import { cleanObject } from "@/utils/others/apiObjectCleaner";

describe("cleanObject", () => {
  it("removes empty strings, null, undefined and empty arrays/sets", () => {
    const dirty = {
      a: "value",
      b: "",
      c: null,
      d: undefined,
      e: [],
      f: new Set(),
      g: 0,
      h: false,
    } as const;

    const cleaned = cleanObject(dirty);

    expect(cleaned).toEqual({ a: "value", g: 0, h: false });
  });

  it("keeps non-empty arrays and sets intact", () => {
    const dirty = {
      arr: [1],
      set: new Set(["x"]),
    } as const;

    const cleaned = cleanObject(dirty);

    expect(cleaned.arr).toEqual([1]);
    expect(cleaned.set?.has("x")).toBe(true);
  });
});
