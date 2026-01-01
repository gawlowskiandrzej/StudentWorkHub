import { searchFilterKeywords } from "@/types/list/searchFilterKeywords";

type SearchKeywordFilterConfig = {
  filterKey: keyof searchFilterKeywords;
  header: string;
};

export const searchKeywordFilters: SearchKeywordFilterConfig[] = [
  {
    filterKey: "skillName",
    header: "Umiejętności",
  },
  {
    filterKey: "educationName",
    header: "Edukacja",
  },
  {
    filterKey: "benefitName",
    header: "Korzyści",
  },
];
