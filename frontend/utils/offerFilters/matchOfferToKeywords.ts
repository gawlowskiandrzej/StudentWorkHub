import { offer } from "@/types/list/Offer/offer";
import { searchFilterKeywords } from "@/types/list/searchFilterKeywords";
import { buildMatcher } from "../others/buildMatcher";

export function matchSearchFilterKeywords(
  offer: offer,
  keywords: searchFilterKeywords
) {
  const hasAnyKeyword =
    keywords.skillName || keywords.educationName || keywords.benefitName;

  if (!hasAnyKeyword) return true;

  const skillMatcher = keywords.skillName
    ? buildMatcher(keywords.skillName) 
    : null;

  const educationMatcher = keywords.educationName
    ? buildMatcher(keywords.educationName)
    : null;

  const benefitMatcher = keywords.benefitName
    ? buildMatcher(keywords.benefitName)
    : null;

  const skillMatch = skillMatcher
    ? skillMatcher(offer.requirements.skills ?? [])
    : false;

  const educationMatch = educationMatcher
    ? educationMatcher(offer.requirements.education ?? [])
    : false;

  const benefitMatch = benefitMatcher
    ? benefitMatcher(offer.benefits ?? [])
    : false;

  return skillMatch || educationMatch || benefitMatch;
}
