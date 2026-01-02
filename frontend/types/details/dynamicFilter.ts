import { language } from "../list/Offer/language";

export interface ApiDynamicFilters {
  languages: language[];
  experienceLevels: string[];
  educationNames: string[];
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