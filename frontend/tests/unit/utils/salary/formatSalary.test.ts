import { describe, it, expect } from "vitest";
import { formatSalaryValue } from "@/utils/salary/formatSalary";

describe("formatSalaryValue", () => {
  it("should format number with thousand separators", () => {
    expect(formatSalaryValue(1000)).toMatch(/1\s?000/);
    expect(formatSalaryValue(10000)).toMatch(/10\s?000/);
  });

  it("should return empty string for null", () => {
    expect(formatSalaryValue(null)).toBe("");
  });

  it("should handle string inputs that are numbers", () => {
    expect(formatSalaryValue("5000")).toMatch(/5\s?000/);
  });
});
