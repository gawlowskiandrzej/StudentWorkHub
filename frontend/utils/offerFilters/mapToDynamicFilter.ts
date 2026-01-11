import { dynamicFilterResponse } from "@/types/api/offer";
import { dynamicFilter } from "@/types/details/dynamicFilter";
import dictionaries from "@/store/data/Dictionaries.json";

export function mapApiFilters(
  api?: dynamicFilterResponse
): dynamicFilter[] {
  if (!api || api.experienceLevels?.length < 1) return [];
  
  // Sort experience levels by min_months
  const sortedExperienceLevels = api.experienceLevels?.sort((a, b) => {
    const aMinMonths = dictionaries.experience_levels_map[a as keyof typeof dictionaries.experience_levels_map]?.min_months ?? Infinity;
    const bMinMonths = dictionaries.experience_levels_map[b as keyof typeof dictionaries.experience_levels_map]?.min_months ?? Infinity;
    return (aMinMonths as number) - (bMinMonths as number);
  });
  
  return [
    // {
    //   key: "languages",
    //   header: "Języki",
    //   items: api.languagesNames.map(name => ({
    //     label: name,
    //     value: name.toLowerCase(),
    //   })),
    // },
    // {
    //   key: "languageLevels",
    //   header: "Poziom języka",
    //   items: api.languagesLevels.map(level => ({
    //     label: level,
    //     value: level,
    //   })),
    // },
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
      items: sortedExperienceLevels?.map(level => ({
        label: level,
        value: level,
      })),
    },
  ];
}