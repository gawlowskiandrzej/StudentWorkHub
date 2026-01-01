import { offer } from "@/types/list/Offer/offer";

export function filterOffers(offers: offer[], keyword: string): offer[] {
  const lowerKeyword = keyword.toLowerCase();

  return offers.filter(o => {
    return (
      o.jobTitle.toLowerCase().includes(lowerKeyword) ||
      o.company?.name?.toLowerCase().includes(lowerKeyword) ||
      o.location?.city?.toLowerCase().includes(lowerKeyword) ||
      (o.description?.toLowerCase().includes(lowerKeyword) ?? false) ||
      o.category.leadingCategory?.toLowerCase().includes(lowerKeyword)
    );
  });
}