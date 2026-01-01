import { ApiDynamicFilters, dynamicFilter } from "@/types/details/dynamicFilter";

export function mapApiFilters(
  api: ApiDynamicFilters
): dynamicFilter[] {
  return [
    {
      key: "languages",
      header: "Języki",
      items: api.languagesNames.map(name => ({
        label: name,
        value: name.toLowerCase(),
      })),
    },
    {
      key: "languageLevels",
      header: "Poziom języka",
      items: api.languagesLevels.map(level => ({
        label: level,
        value: level,
      })),
    },
    // {
    //   key: "experienceMonths",
    //   header: "Doświadczenie (miesiące)",
    //   items: [...api.experienceMonths]
    //     .sort((a, b) => a - b)
    //     .map(months => ({
    //       label: `${months} miesięcy`,
    //       value: months,
    //     })),
    // },
    {
      key: "experienceLevels",
      header: "Poziom doświadczenia",
      items: api.experienceLevels.map(level => ({
        label: level,
        value: level,
      })),
    },
  ];
}