import { offer } from '@/types/list/Offer/offer';
import { 
    UserPreferences, 
    OfferFeatureVector, 
    NormalizationStats,
    WelfordStats,
    FEATURE_KEYS,
    FEATURE_COUNT,
    DEFAULT_WELFORD_STATS
} from '../types';
import { DISTANCE_CALCULATORS } from '../distances';
import { clamp, sigmoid } from '../utils';

// extract feature vector for SGD and normalization with user preferences
export function extractFeatureVector(
    offerData: offer,
    preferences: UserPreferences,
    stats?: NormalizationStats
): OfferFeatureVector {
    const rawValues: Record<string, number> = {};
    const normalizedValues: number[] = [];
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const featureKey = FEATURE_KEYS[i];
        const calculator = DISTANCE_CALCULATORS[featureKey];
        
        if (!calculator) {
            console.warn(`[FeatureExtraction] No calculator for feature: ${featureKey}`);
            rawValues[featureKey] = 0.5;
            normalizedValues.push(0.5);
            continue;
        }
        
        const rawDistance = calculator.sgdDistance(offerData, preferences);
        rawValues[featureKey] = rawDistance;
        
        const normalizedDistance = stats 
            ? normalizeWithStats(rawDistance, stats.featureStats[i])
            : rawDistance;
            
        normalizedValues.push(clamp(normalizedDistance, 0, 1));
    }
    
    return {
        values: normalizedValues,
        raw: rawValues as any
    };
}

// extract feature differences between two offers for RReliefF
export function extractFeatureDifferences(
    offer1: offer,
    offer2: offer
): number[] {
    const differences: number[] = [];
    
    for (const featureKey of FEATURE_KEYS) {
        const calculator = DISTANCE_CALCULATORS[featureKey];
        
        if (!calculator) {
            differences.push(0);
            continue;
        }
        
        const difference = calculator.rrelieffDifference(offer1, offer2);
        differences.push(clamp(difference, 0, 1));
    }
    
    return differences;
}

//z-sore
function normalizeWithStats(value: number, stats: WelfordStats): number {
    if (stats.count < 2) {
        return value;
    }
    
    const variance = getVariance(stats);
    const stdDev = Math.sqrt(variance);
    
    if (stdDev < 0.001) {
        return 0.5;
    }
    
    const zScore = (value - stats.mean) / stdDev;
    const clampedZScore = Math.max(-5, Math.min(5, zScore));
    return sigmoid(clampedZScore);
}


// WELFORD'S
export function updateWelfordStats(
    stats: WelfordStats,
    value: number
): WelfordStats {
    const newCount = stats.count + 1;
    const delta = value - stats.mean;
    const newMean = stats.mean + delta / newCount;
    const delta2 = value - newMean;
    const newM2 = stats.m2 + delta * delta2;
    
    return {
        count: newCount,
        mean: newMean,
        m2: newM2
    };
}

/**
 * variance = M2 / n
 */
export function getVariance(stats: WelfordStats): number {
    if (stats.count < 1) return 0;
    return stats.m2 / stats.count;
}

export function getVarianceSample(stats: WelfordStats): number {
    if (stats.count < 2) return 0;
    return stats.m2 / (stats.count - 1);
}

/**
 * - CV < 0.1: stable
 * - CV > 0.5: unstable
 */
export function getCoefficientOfVariation(stats: WelfordStats): number {
    if (stats.count < 2 || Math.abs(stats.mean) < 0.001) {
        return 0;
    }
    const variance = getVariance(stats);
    const stdDev = Math.sqrt(variance);
    return stdDev / Math.abs(stats.mean);
}

export function getStandardDeviation(stats: WelfordStats): number {
    if (stats.count < 2) return 0;
    return Math.sqrt(getVariance(stats));
}

export function updateNormalizationStats(
    normStats: NormalizationStats,
    featureVector: OfferFeatureVector
): NormalizationStats {
    const newFeatureStats = normStats.featureStats.map((stats, i) => 
        updateWelfordStats(stats, featureVector.values[i])
    );
    
    const newMeanDistances = newFeatureStats.map(s => s.mean);
    
    return {
        ...normStats,
        featureStats: newFeatureStats,
        meanDistances: newMeanDistances
    };
}

// initail 
export function createInitialNormalizationStats(): NormalizationStats {
    return {
        featureStats: Array(FEATURE_COUNT).fill(null).map(() => ({ ...DEFAULT_WELFORD_STATS })),
        meanValueIds: [...FEATURE_KEYS],
        meanDistances: Array(FEATURE_COUNT).fill(0.5)
    };
}

export function extractFeaturesForOffers(
    offers: offer[],
    preferences: UserPreferences,
    stats?: NormalizationStats
): Map<number, OfferFeatureVector> {
    const featuresMap = new Map<number, OfferFeatureVector>();
    
    for (const offerData of offers) {
        const features = extractFeatureVector(offerData, preferences, stats);
        featuresMap.set(offerData.id, features);
    }
    
    return featuresMap;
}

export function buildNormalizationStatsFromOffers(
    offers: offer[],
    preferences: UserPreferences
): { stats: NormalizationStats; features: Map<number, OfferFeatureVector> } {
    let stats = createInitialNormalizationStats();
    const features = new Map<number, OfferFeatureVector>();
    
    // first run
    for (const offerData of offers) {
        const rawFeatures = extractFeatureVector(offerData, preferences);
        features.set(offerData.id, rawFeatures);
        stats = updateNormalizationStats(stats, rawFeatures);
    }
    
    // second run
    for (const offerData of offers) {
        const normalizedFeatures = extractFeatureVector(offerData, preferences, stats);
        features.set(offerData.id, normalizedFeatures);
    }
    
    return { stats, features };
}

//DEBUG

export function formatFeatureVector(features: OfferFeatureVector): string {
    const lines: string[] = [];
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const key = FEATURE_KEYS[i];
        const value = features.values[i];
        const raw = features.raw[key];
        
        lines.push(`${key}: ${value.toFixed(3)} (raw: ${JSON.stringify(raw)})`);
    }
    
    return lines.join('\n');
}
