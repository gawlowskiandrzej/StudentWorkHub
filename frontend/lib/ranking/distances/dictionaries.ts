import { offer } from '@/types/list/Offer/offer';
import { normalizeString } from '../utils';

export interface DynamicDictionaries {
    allBenefits: Set<string>;
    
    allEmploymentTypes: Set<string>;
    
    employmentTypeAliases: Map<string, string>;

    allCategories: Set<string>;

    allSkills: Set<string>;
    
    allLanguages: Set<string>;
    
    offerCount: number;
}

//TODO load from JSON
export const EXPERIENCE_LEVELS_MAP: Record<string, { min_months: number | null, max_months: number | null }> = {
    "początkujący": { "min_months": 0, "max_months": 6 },
    "średniozaawansowany": { "min_months": 7, "max_months": 18 },
    "dobry": { "min_months": 19, "max_months": 30 },
    "młodszy specjalista": { "min_months": 12, "max_months": 24 },
    "zaawansowany": { "min_months": 31, "max_months": 48 },
    "specjalista": { "min_months": 36, "max_months": 60 },
    "starszy specjalista": { "min_months": 61, "max_months": 96 },
    "biegły": { "min_months": 84, "max_months": 120 },
    "ekspert": { "min_months": 121, "max_months": null },
    "inny": { "min_months": null, "max_months": null }
};

let currentDictionaries: DynamicDictionaries | null = null;


export function buildDictionariesFromOffers(offers: offer[]): DynamicDictionaries {
    const allBenefits = new Set<string>();
    const allEmploymentTypes = new Set<string>();
    const employmentTypeAliases = new Map<string, string>();
    const allCategories = new Set<string>();
    const allSkills = new Set<string>();
    const allLanguages = new Set<string>();
    
    for (const offer of offers) {
        if (offer.benefits) {
            for (const benefit of offer.benefits) {
                const normalized = normalizeString(benefit);
                if (normalized.length > 0) {
                    allBenefits.add(normalized);
                }
            }
        }
        
        if (offer.employment?.types) {
            for (const type of offer.employment.types) {
                const normalized = normalizeString(type);
                if (normalized.length > 0) {
                    allEmploymentTypes.add(normalized);
                    
                    const canonical = detectEmploymentTypeCanonical(normalized);
                    if (canonical !== normalized) {
                        employmentTypeAliases.set(normalized, canonical);
                    }
                }
            }
        }
        
        if (offer.category?.leadingCategory) {
            const normalized = normalizeString(offer.category.leadingCategory);
            if (normalized.length > 0) {
                allCategories.add(normalized);
            }
        }
        
        if (offer.requirements?.skills) {
            for (const skill of offer.requirements.skills) {
                const normalized = normalizeString(skill.skill);
                if (normalized.length > 0) {
                    allSkills.add(normalized);
                }
            }
        }
        
        if (offer.requirements?.languages) {
            for (const lang of offer.requirements.languages) {
                const normalized = normalizeString(lang.language);
                if (normalized.length > 0) {
                    allLanguages.add(normalized);
                }
            }
        }
    }
    
    currentDictionaries = {
        allBenefits,
        allEmploymentTypes,
        employmentTypeAliases,
        allCategories,
        allSkills,
        allLanguages,
        offerCount: offers.length
    };
    
    return currentDictionaries;
}
//TODO load from JSON or better tahan static
function detectEmploymentTypeCanonical(type: string): string {
    const lowerType = type.toLowerCase();
    
    if (lowerType.includes('b2b') || lowerType.includes('kontrakt')) {
        return 'kontrakt b2b';
    }
    
    if (lowerType.includes('umowa o prac') || lowerType === 'uop') {
        return 'umowa o pracę';
    }
    
    if (lowerType.includes('zlecenie')) {
        return 'umowa zlecenie';
    }
    
    if (lowerType.includes('dzieło') || lowerType.includes('dzielo')) {
        return 'umowa o dzieło';
    }
    
    if (lowerType.includes('staż') || lowerType.includes('staz') || lowerType.includes('praktyk')) {
        return 'staż/praktyka';
    }
    
    return type;
}

export function getDictionaries(): DynamicDictionaries {
    if (!currentDictionaries) {
        return {
            allBenefits: new Set(),
            allEmploymentTypes: new Set(),
            employmentTypeAliases: new Map(),
            allCategories: new Set(),
            allSkills: new Set(),
            allLanguages: new Set(),
            offerCount: 0
        };
    }
    return currentDictionaries;
}

// reset
export function resetDictionaries(): void {
    currentDictionaries = null;
}

//dynamic alias
export function normalizeEmploymentType(type: string): string {
    const normalized = normalizeString(type);
    const dictionaries = getDictionaries();
    return dictionaries.employmentTypeAliases.get(normalized) ?? detectEmploymentTypeCanonical(normalized);
}

//probablity TODO fix naming
export function calculateRelativeBenefitScore(offerBenefits: Set<string>): number {
    const dictionaries = getDictionaries();
    
    if (dictionaries.allBenefits.size === 0 || offerBenefits.size === 0) {
        return 0.5;
    }
    
    let matchCount = 0;
    for (const benefit of offerBenefits) {
        if (dictionaries.allBenefits.has(benefit)) {
            matchCount++;
        }
    }
    
    const avgBenefitsPerOffer = dictionaries.allBenefits.size / Math.max(1, dictionaries.offerCount);
    
    return Math.min(1, offerBenefits.size / Math.max(1, avgBenefitsPerOffer * 2));
}
