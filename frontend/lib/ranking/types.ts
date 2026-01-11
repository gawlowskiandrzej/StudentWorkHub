
import { offer } from '@/types/list/Offer/offer';
import { PreferencesDto } from '@/types/api/usersDTO';

export const FEATURE_KEYS = [
    'SALARY_MATCH',
    'BENEFIT_MATCH', 
    'SKILLS_MATCH',
    'EMPLOYMENT_TYPE_MATCH',
    'SCHEDULE_MATCH',
    'CATEGORY_MATCH',
    'LANGUAGE_MATCH',
    'FRESHNESS'
] as const;

export type FeatureKey = typeof FEATURE_KEYS[number];

export const FEATURE_COUNT = FEATURE_KEYS.length;

export const INTERACTION_SIGNALS = {
    hover: 0.1,          
    click: 0.8,          
    view_time: 0.4,      
    show_more_like: 0.8  
} as const;

export type InteractionType = keyof typeof INTERACTION_SIGNALS;

export interface UserPreferences {
    leadingCategoryId: number | null;
    leadingCategoryName: string | null;
    
    salaryFrom: number | null;
    salaryTo: number | null;
    
    employmentTypeIds: number[];
    employmentTypeNames: string[];
    
    jobStatus: 'actively_looking' | 'open_to_offers' | 'not_looking' | null; //TODO use dictionary
    isActivelyLooking: boolean;
    
    cityName: string | null;
    
    workTypes: string[];
    
    languages: Array<{
        languageId: number;
        languageName: string | null;
        languageLevelId: number;
        languageLevelName: string | null;
    }>;
    
    skills: Array<{
        skillName: string;
        experienceMonths: number;
    }>;
}

export interface OfferFeatureVector {
    /** [0-1] */
    values: number[];
    /** debuging */
    raw: Record<FeatureKey, any>;
}

export interface RawFeatureData {
    SALARY_MATCH: {
        offerMin: number | null;
        offerMax: number | null;
        userMin: number | null;
        userMax: number | null;
    };
    BENEFIT_MATCH: {
        offerBenefits: string[];
        matchedCount: number;
        totalCount: number;
    };
    SKILLS_MATCH: {
        offerSkills: string[];
        userSkills: string[];
        matchedSkills: string[];
        jaccardSimilarity: number;
    };
    EMPLOYMENT_TYPE_MATCH: {
        offerTypes: string[];
        userTypes: string[];
        hasMatch: boolean;
    };
    SCHEDULE_MATCH: {
        offerSchedules: string[];
        userWorkTypes: string[];
        hasMatch: boolean;
    };
    CATEGORY_MATCH: {
        offerCategory: string | null;
        userCategory: string | null;
        isExactMatch: boolean;
    };
    LANGUAGE_MATCH: {
        offerLanguages: Array<{ language: string; level: string }>;
        userLanguages: Array<{ language: string; level: string }>;
        jaccardSimilarity: number;
    };
    FRESHNESS: {
        publishedDate: Date;
        expiresDate: Date;
        daysOld: number;
        daysToExpire: number;
        isActivelyLooking: boolean;
    };
}


export interface AdamOptimizerState {
    /**
     * mean of gradients
     */
    m: number[];
    /** 
     * mean of squared gradients
     */
    v: number[];
    /** steps bias correction */
    t: number;
}

export interface SGDHyperparameters {
    learningRate: number;
    beta1: number;
    beta2: number;
    epsilon: number;
    regularization: number;
}

export interface SGDTrainerState {
    weights: number[];
    adamState: AdamOptimizerState;
    hyperparameters: SGDHyperparameters;
}

export interface RReliefFState {
    featureImportance: number[];
    k: number;
    sigma: number;
}


export interface WelfordStats {
    count: number;
    mean: number;
    m2: number;
}


export interface WelfordStatsWithVariance extends WelfordStats {
    variance: number;
    stdDev: number;
}

export interface NormalizationStats {
    featureStats: WelfordStats[];
    meanValueIds: string[];
    meanDistances: number[];
}

export interface RankerState {
    userPreferences: UserPreferences;
    sgdTrainer: SGDTrainerState;
    rreliefF: RReliefFState;
    normalizationStats: NormalizationStats;
    orderByWeights: FeatureKey[];
    extractedFeaturesCache: Map<number, OfferFeatureVector>;
}

export interface UserRankingProfile {
    vector: number[];
    meanDist: number[];
    meanValueIds: string[];
    orderByOption: string[];
    meansValueSum: number[];
    meansValueSsum: number[];
    meansValueCount: number[];
    meansWeightSum: number[];
    meansWeightSsum: number[];
    meansWeightCount: number[];
}

export interface ScoredOffer {
    offerId: number;
    score: number;
    features: OfferFeatureVector;
    confidence: number;
}

export const DEFAULT_ADAM_STATE: AdamOptimizerState = {
    m: new Array(FEATURE_COUNT).fill(0),
    v: new Array(FEATURE_COUNT).fill(0),
    t: 0
};

export const DEFAULT_SGD_HYPERPARAMETERS: SGDHyperparameters = {
    learningRate: 0.01,
    beta1: 0.9,
    beta2: 0.999,
    epsilon: 1e-8,
    regularization: 0.01
};

export const DEFAULT_RRELIEFF_STATE: RReliefFState = {
    featureImportance: new Array(FEATURE_COUNT).fill(1.0 / FEATURE_COUNT),
    k: 10,
    sigma: 0.5
};

export const DEFAULT_WELFORD_STATS: WelfordStats = {
    count: 0,
    mean: 0.5,
    m2: 0
};

export const DEFAULT_USER_PROFILE: UserRankingProfile = {
    vector: new Array(FEATURE_COUNT).fill(0.5),
    meanDist: new Array(FEATURE_COUNT).fill(0.5),
    meanValueIds: FEATURE_KEYS.slice(),
    orderByOption: FEATURE_KEYS.slice(),
    meansValueSum: new Array(FEATURE_COUNT).fill(0),
    meansValueSsum: new Array(FEATURE_COUNT).fill(0),
    meansValueCount: new Array(FEATURE_COUNT).fill(0),
    meansWeightSum: new Array(FEATURE_COUNT).fill(0),
    meansWeightSsum: new Array(FEATURE_COUNT).fill(0),
    meansWeightCount: new Array(FEATURE_COUNT).fill(0)
};

export const DEFAULT_USER_PREFERENCES: UserPreferences = {
    leadingCategoryId: null,
    leadingCategoryName: null,
    salaryFrom: null,
    salaryTo: null,
    employmentTypeIds: [],
    employmentTypeNames: [],
    jobStatus: null,
    isActivelyLooking: false,
    cityName: null,
    workTypes: [],
    languages: [],
    skills: []
};

export interface DistanceCalculator {
    sgdDistance(offer: offer, preferences: UserPreferences): number;
    rrelieffDifference(offer1: offer, offer2: offer): number;
}
