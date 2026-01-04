import { language } from "../list/Offer/language";
import { offer } from "../list/Offer/offer";
import { jobInfoDto } from "./jobInfoDto";

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
export type scrappingStatus = {
  jobInfos: jobInfoDto[],
  scrappingDone:boolean,
  scrappingDuration?: string
}
export type scrappedOffersResponse = {
  databaseOffersResponse: offerListResponse,
  scrappingStatus: scrappingStatus;
}
export type offerListResponse = {
  pagination: pagination;
  dynamicFilter: dynamicFilterResponse;
}
