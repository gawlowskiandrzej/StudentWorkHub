export type {
    FeatureKey,
    InteractionType,
    UserPreferences,
    OfferFeatureVector,
    ScoredOffer,
    SGDTrainerState,
    RReliefFState,
    RankerState,
    NormalizationStats,
    WelfordStats,
    UserRankingProfile,
    DistanceCalculator
} from './types';

export {
    FEATURE_KEYS,
    FEATURE_COUNT,
    INTERACTION_SIGNALS,
    DEFAULT_USER_PROFILE,
    DEFAULT_USER_PREFERENCES,
    DEFAULT_SGD_HYPERPARAMETERS,
    DEFAULT_RRELIEFF_STATE,
    DEFAULT_WELFORD_STATS
} from './types';

export {
    jaccardSimilarity,
    normalizeString,
    languageLevelToNumeric,
    daysDifference,
    clamp,
    sigmoid
} from './utils';

export {
    DISTANCE_CALCULATORS,
    SalaryDistanceCalculator,
    ScheduleDistanceCalculator,
    BenefitDistanceCalculator,
    SkillsDistanceCalculator,
    EmploymentTypeDistanceCalculator,
    CategoryDistanceCalculator,
    LanguageDistanceCalculator,
    FreshnessDistanceCalculator
} from './distances';

export {
    extractFeatureVector,
    extractFeatureDifferences,
    updateWelfordStats,
    getStandardDeviation,
    updateNormalizationStats,
    createInitialNormalizationStats,
    extractFeaturesForOffers,
    buildNormalizationStatsFromOffers,
    formatFeatureVector
} from './features';

export {
    // SGD
    createSGDTrainer,
    computeOfferScore,
    computeScoreBreakdown,
    trainStep,
    trainBatch,
    normalizeWeights,
    computeModelConfidence,
    resetAdamState,
    scaleSignalByViewTime,
    serializeSGDState,
    deserializeSGDState,
    
    // RReliefF
    createRReliefFState,
    trainRReliefFStep,
    trainRReliefFBatch,
    getFeaturesByImportance,
    getFeatureImportance,
    formatFeatureImportance,
    serializeRReliefFState,
    deserializeRReliefFState
} from './algorithms';

export type { ScoredOfferContext } from './algorithms/rrelieff';

export {
    Ranking,
    getRanking,
    resetRanking
} from './ranking';
