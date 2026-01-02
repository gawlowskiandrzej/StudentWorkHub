import { language } from "../list/Offer/language";
import { offer } from "../list/Offer/offer";

export type searchOfferDto = {
    keyword: string;
    category: string;
    localization: string;
    employmentType: string;
    salaryPeriod: string;
    employmentSchedule: string;
    salaryFrom: number;
    salaryTo: number;
};

export type pagination = {
  items: offer[]
  page: number
  pageSize: number
  totalItems: number
  totalPages: number
  isLastPage: boolean
};
export type dynamicFilterResponse = {
  languages: language[]
  experienceLevels: string[]
  educationNames: string[]
}
export type offerListResponse = {
  pagination: pagination;
  dynamicFilters: dynamicFilterResponse;
}
