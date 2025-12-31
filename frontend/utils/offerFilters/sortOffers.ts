import { offer } from "@/types/list/Offer/offer";

export function sortData(items: offer[], sortValue: string) {
  switch (sortValue) {
    case "CreationDate":
      return [...items].sort(
        (a, b) =>
          new Date(a.dates.expires).getTime() -
          new Date(b.dates.published).getTime()
      );
    case "Salaryasc":
      return [...items].sort((a, b) => {
        const salaryA = a.salary?.from ?? 0;
        const salaryB = b.salary?.from ?? 0;
        return salaryA - salaryB;
      });

    case "Salarydesc":
      return [...items].sort((a, b) => {
        const salaryA = a.salary?.from ?? 0;
        const salaryB = b.salary?.from ?? 0;
        return salaryB - salaryA;
      });
    case "Nameasc":
      return [...items].sort((a, b) => a.jobTitle.localeCompare(b.jobTitle));
    default:
      return items;
  }
}
