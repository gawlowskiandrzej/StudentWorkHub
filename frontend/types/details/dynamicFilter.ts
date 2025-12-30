export interface ApiDynamicFilters {
  languagesNames: string[];
  languagesLevels: string[];
  experienceMonths: number[];
  experienceLevels: string[];
}

export type FilterValue = string | number;

export interface FilterItem {
  label: string;
  value: FilterValue;
}

export type FilterKey =
  | "languages"
  | "languageLevels"
  | "experienceMonths"
  | "experienceLevels";

export interface dynamicFilter {
  key: FilterKey;
  header: string;
  items: FilterItem[];
}