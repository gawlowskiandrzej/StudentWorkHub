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

      case "experienceMonths":
        return (
          offer.requirements.skills?.some(skill =>
            skill.experienceMonths != null &&
            values.has(skill.experienceMonths)
          ) ?? false
        );

      case "experienceLevels":
        return (
          offer.requirements.skills?.some(skill =>
            skill.experienceLevel?.some(level =>
              values.has(level)
            )
          ) ?? false
        );

      default:
        return true;
    }
  });
}
