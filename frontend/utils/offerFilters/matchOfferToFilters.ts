import { FiltersState } from "@/app/(public)/list/page";
import { FilterKey, FilterValue } from "@/types/details/dynamicFilter";
import { offer } from "@/types/list/Offer/offer";

export function matchOfferToFilters(
  offer: offer,
  filters: FiltersState
): boolean {
  return (Object.entries(filters) as [
    FilterKey,
    Set<FilterValue>
  ][]).every(([key, values]) => {
    if (!values || values.size === 0) return true;

    switch (key) {
      case "languages":
        return (
          offer.requirements.languages?.some(lang =>
            values.has(lang.language?.toLowerCase() ?? "")
          ) ?? false
        );

      case "languageLevels":
        return (
          offer.requirements.languages?.some(lang =>
            values.has(lang.level ?? "")
          ) ?? false
        );

      case "experienceLevels":
        return (
          offer.requirements.skills?.every(skill =>{
            if (skill.experienceLevel?.length == 0) return true;
            return skill.experienceLevel?.some(level =>
              values.has(level)
            )
    }) ?? false
        );

      default:
        return true;
    }
  });
}
