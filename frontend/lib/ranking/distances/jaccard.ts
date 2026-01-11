/**
 * distances/jaccard.ts
 * 
 * Kalkulatory dystansów oparte na współczynniku Jaccarda.
 * Używa dynamicznych słowników budowanych z załadowanych ofert.
 */

import { offer } from '@/types/list/Offer/offer';
import { UserPreferences, DistanceCalculator } from '../types';
import { jaccardSimilarity, normalizeString, languageLevelToNumeric } from '../utils';
import { getDictionaries, normalizeEmploymentType, EXPERIENCE_LEVELS_MAP } from './dictionaries';

// ============================================
// BENEFIT_MATCH - Dopasowanie benefitów
// ============================================

export class BenefitDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const dictionaries = getDictionaries();
        const offerBenefits = new Set(
            (offer.benefits ?? []).map(b => normalizeString(b))
        );
        
        // Brak benefitów w ofercie - neutralny dystans
        if (offerBenefits.size === 0) return 0.5;
        
        // Brak słownika - neutralny dystans
        if (dictionaries.allBenefits.size === 0) return 0.5;
        
        // Oblicz ile benefitów oferty jest w słowniku wszystkich ofert
        const similarity = jaccardSimilarity(offerBenefits, dictionaries.allBenefits);
        
        // Im więcej benefitów względem całego zbioru, tym lepiej
        const relativeScore = offerBenefits.size / Math.max(1, dictionaries.allBenefits.size) * 10;
        
        // Kombinacja: czy są popularne + ile ich jest
        return 1 - Math.min(1, (similarity * 0.5 + Math.min(1, relativeScore) * 0.5));
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const benefits1 = new Set((offer1.benefits ?? []).map(b => normalizeString(b)));
        const benefits2 = new Set((offer2.benefits ?? []).map(b => normalizeString(b)));
        
        return 1 - jaccardSimilarity(benefits1, benefits2);
    }
}

//TODO required months calculation
export class SkillsDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const dictionaries = getDictionaries();
        const offerReqFn = offer.requirements?.skills ?? [];
        
        const offerSkillsMap = new Map<string, { months: number, levels: string[] }>();
        
        for (const s of offerReqFn) {
            const name = normalizeString(s.skill ?? '');
            if (!name) continue;
            
            let requiredMonths = s.experienceMonths ?? 0;
            const levels = s.experienceLevel ?? [];
            
            if (requiredMonths === 0 && levels.length > 0) {
                let minLevelMonths = 999;
                let found = false;
                
                for (const lvl of levels) {
                    const normLvl = normalizeString(lvl);
                    for (const [key, range] of Object.entries(EXPERIENCE_LEVELS_MAP)) {
                        if (normalizeString(key) === normLvl) {
                            const mm = range.min_months ?? 0;
                            if (mm < minLevelMonths) {
                                minLevelMonths = mm;
                                found = true;
                            }
                        }
                    }
                }
                
                if (found) {
                    requiredMonths = minLevelMonths;
                }
            }
            
            offerSkillsMap.set(name, { months: requiredMonths, levels });
        }
        
        const userSkills = preferences.skills.filter(s => s.skillName);
        
        //neutral distance
        if (offerSkillsMap.size === 0) return 0.5;
        
        // TODO fix commones
        if (userSkills.length === 0) {
            if (dictionaries.allSkills.size > 0) {
                const offerSkillNames = new Set(offerSkillsMap.keys());
                const commonness = jaccardSimilarity(offerSkillNames, dictionaries.allSkills);
                return 1 - commonness * 0.5
            }
            return 0.5;
        }
        
        let totalMatchScore = 0;
        let matchedCount = 0;
        
        for (const uSkill of userSkills) {
            const uName = normalizeString(uSkill.skillName);
            const uMonths = uSkill.experienceMonths ?? 0;
            
            let bestMatchVal = 0;
            let foundMatch = false;

            for (const [oName, oDetails] of offerSkillsMap.entries()) {
                if (oName === uName || (oName.length > 4 && uName.length > 4 && (oName.includes(uName) || uName.includes(oName)))) {
                    foundMatch = true;
                    
                    const required = oDetails.months;
                    
                    if (uMonths >= required) {
                        bestMatchVal = Math.min(1.2, 1.0 + (uMonths - required) / 24); 
                    } else {
                        const ratio = Math.max(0, uMonths) / Math.max(1, required);
                        bestMatchVal = ratio;
                    }
                    
                    break; //TODO multiple skill matches
                }
            }
            
            if (foundMatch) {
                totalMatchScore += bestMatchVal;
                matchedCount++;
            }
        }
        
        const unionSize = Math.max(offerSkillsMap.size, userSkills.length);
        
        if (unionSize === 0) return 0.5;
        
        const score = totalMatchScore / unionSize;
        
        return Math.max(0, 1 - Math.min(1, score));
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const skills1 = new Set(
            (offer1.requirements?.skills ?? [])
                .map(s => normalizeString(s.skill))
                .filter(s => s.length > 0)
        );
        
        const skills2 = new Set(
            (offer2.requirements?.skills ?? [])
                .map(s => normalizeString(s.skill))
                .filter(s => s.length > 0)
        );
        
        return 1 - jaccardSimilarity(skills1, skills2);
    }
}

export class EmploymentTypeDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const dictionaries = getDictionaries();
        
        const offerTypes = new Set(
            (offer.employment?.types ?? []).map(t => normalizeEmploymentType(t))
        );
        
        const userTypes = new Set(
            preferences.employmentTypeNames.map(t => normalizeEmploymentType(t))
        );
        
        if (offerTypes.size === 0) return 0.5;
        
        //TODO fix commonness
        if (userTypes.size === 0) {
            if (dictionaries.allEmploymentTypes.size > 0) {
                const commonness = jaccardSimilarity(offerTypes, dictionaries.allEmploymentTypes);
                return 1 - commonness * 0.3;
            }
            return 0.3;
        }
        
        const hasMatch = [...offerTypes].some(t => userTypes.has(t));
        return hasMatch ? 0 : 1;
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const types1 = new Set(
            (offer1.employment?.types ?? []).map(t => normalizeEmploymentType(t))
        );
        
        const types2 = new Set(
            (offer2.employment?.types ?? []).map(t => normalizeEmploymentType(t))
        );
        
        return 1 - jaccardSimilarity(types1, types2);
    }
}

export class CategoryDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const dictionaries = getDictionaries();
        
        const offerCategory = normalizeString(offer.category?.leadingCategory);
        const userCategory = normalizeString(preferences.leadingCategoryName);
        
        if (!offerCategory) return 0.5;
        
        if (!userCategory) {
            if (dictionaries.allCategories.size > 0 && dictionaries.allCategories.has(offerCategory)) {
                return 0.3;
            }
            return 0.5;
        }
        
        if (offerCategory === userCategory) return 0;
        
        if (offerCategory.includes(userCategory) || userCategory.includes(offerCategory)) {
            return 0.3;
        }
        
        return 1;
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const cat1 = normalizeString(offer1.category?.leadingCategory);
        const cat2 = normalizeString(offer2.category?.leadingCategory);
        
        if (!cat1 || !cat2) return 0.5;
        if (cat1 === cat2) return 0;
        
        return 1;
    }
}

//TODO levels from dictionary
export class LanguageDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const dictionaries = getDictionaries();
        
        const offerLanguages = offer.requirements?.languages ?? [];
        const userLanguages = preferences.languages;
        
        if (offerLanguages.length === 0) return 0.1;
        
        if (userLanguages.length === 0) {
            if (dictionaries.allLanguages.size > 0) {
                const offerLangSet = new Set(offerLanguages.map(l => normalizeString(l.language)));
                const commonness = jaccardSimilarity(offerLangSet, dictionaries.allLanguages);
                return 1 - commonness * 0.3;
            }
            return 0.7;
        }
        
        let totalScore = 0;
        let matchedCount = 0;
        
        for (const offerLang of offerLanguages) {
            const offerLangName = normalizeString(offerLang.language);
            const offerLevel = languageLevelToNumeric(offerLang.level);
            
            const userLang = userLanguages.find(
                ul => normalizeString(ul.languageName) === offerLangName
            );
            
            if (userLang) {
                const userLevel = languageLevelToNumeric(userLang.languageLevelName);
                if (userLevel >= offerLevel) {
                    totalScore += 1.0;
                } else {
                    totalScore += userLevel / Math.max(1, offerLevel);
                }
                matchedCount++;
            }
        }
        
        const matchRatio = matchedCount / offerLanguages.length;
        const qualityScore = matchedCount > 0 ? totalScore / matchedCount : 0;
        
        return 1 - (matchRatio * 0.5 + qualityScore * 0.5);
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const langs1 = new Set(
            (offer1.requirements?.languages ?? [])
                .map(l => normalizeString(l.language))
        );
        
        const langs2 = new Set(
            (offer2.requirements?.languages ?? [])
                .map(l => normalizeString(l.language))
        );
        
        return 1 - jaccardSimilarity(langs1, langs2);
    }
}
