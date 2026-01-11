import { offer } from '@/types/list/Offer/offer';
import {
    RReliefFState,
    OfferFeatureVector,
    FEATURE_COUNT,
    FEATURE_KEYS,
    FeatureKey,
    DEFAULT_RRELIEFF_STATE
} from '../types';
import { extractFeatureDifferences } from '../features/extractor';

export interface ScoredOfferContext {
    offerId: number;
    offer: offer;
    score: number;
    features: OfferFeatureVector;
}

interface NeighborInfo {
    context: ScoredOfferContext;
    distance: number;
    weight: number;
}
// initial TODO parameters
export function createRReliefFState(
    k: number = 10,
    sigma: number = 0.5
): RReliefFState {
    return {
        featureImportance: new Array(FEATURE_COUNT).fill(1.0 / FEATURE_COUNT),
        k,
        sigma
    };
}

//IMPORTANT TODO tylko ocnenione oferty powinny być używane jako sąsiedzi
export function trainRReliefFStep(
    state: RReliefFState,
    selectedOffer: ScoredOfferContext,
    selectedScore: number,
    contextOffers: ScoredOfferContext[]
): RReliefFState {
    const { k, sigma } = state;
    
    const otherOffers = contextOffers.filter(o => o.offerId !== selectedOffer.offerId);
    
    if (otherOffers.length === 0) {
        console.log('[RReliefF] No context offers to compare');
        return state;
    }
    
    const neighbors = findKNearestNeighbors(
        selectedOffer,
        otherOffers,
        Math.min(k, otherOffers.length),
        sigma
    );
    
    if (neighbors.length === 0) {
        return state;
    }
    
    let totalTargetDiffInfluence = 0;
    const totalFeatureDiffInfluence = new Array(FEATURE_COUNT).fill(0);
    const targetAndFeatureDiffInfluence = new Array(FEATURE_COUNT).fill(0);
    
    for (const neighbor of neighbors) {
        const weight = neighbor.weight;
        const scoreDiff = Math.abs(selectedScore - neighbor.context.score);
        
        totalTargetDiffInfluence += scoreDiff * weight;
        
        const featureDiffs = extractFeatureDifferences(
            selectedOffer.offer,
            neighbor.context.offer
        );
        
        for (let i = 0; i < FEATURE_COUNT; i++) {
            const featureDiff = featureDiffs[i];
            
            totalFeatureDiffInfluence[i] += featureDiff * weight;
            targetAndFeatureDiffInfluence[i] += scoreDiff * featureDiff * weight;
        }
    }
    
    const newFeatureImportance = [...state.featureImportance];
    
    if (totalTargetDiffInfluence < 0.0001) {
        return state;
    }
    
    const totalInfluence = neighbors.reduce((sum, n) => sum + n.weight, 0);
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const probDiffTargetGivenDiffFeature = 
            targetAndFeatureDiffInfluence[i] / (totalTargetDiffInfluence + 0.0001);
        
        const probDiffTargetGivenNoDiffFeature = 
            (totalFeatureDiffInfluence[i] - targetAndFeatureDiffInfluence[i]) /
            (totalInfluence - totalTargetDiffInfluence + 0.0001);
        
        const newImportance = probDiffTargetGivenDiffFeature - probDiffTargetGivenNoDiffFeature;
        
        newFeatureImportance[i] = 0.7 * state.featureImportance[i] + 0.3 * Math.max(0, newImportance + 0.5);
    }
    
    const importanceSum = newFeatureImportance.reduce((s, v) => s + v, 0);
    const normalizedImportance = importanceSum > 0
        ? newFeatureImportance.map(v => v / importanceSum)
        : new Array(FEATURE_COUNT).fill(1 / FEATURE_COUNT);
    
    return {
        ...state,
        featureImportance: normalizedImportance
    };
}

//TODO
function findKNearestNeighbors(
    target: ScoredOfferContext,
    candidates: ScoredOfferContext[],
    k: number,
    sigma: number
): NeighborInfo[] {
    const neighborsWithDistance: NeighborInfo[] = candidates.map(candidate => {
        const distance = computeFeatureDistance(target.features, candidate.features);
        const weight = computeInfluenceWeight(distance, sigma);
        
        return { context: candidate, distance, weight };
    });
    
    neighborsWithDistance.sort((a, b) => a.distance - b.distance);
    
    return neighborsWithDistance.slice(0, k);
}

function computeFeatureDistance(
    features1: OfferFeatureVector,
    features2: OfferFeatureVector
): number {
    let sumSquares = 0;
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const diff = features1.values[i] - features2.values[i];
        sumSquares += diff * diff;
    }
    
    return Math.sqrt(sumSquares);
}

function computeInfluenceWeight(distance: number, sigma: number): number {
    return Math.exp(-(distance * distance) / (sigma * sigma));
}

export function getFeaturesByImportance(state: RReliefFState): FeatureKey[] {
    const indexed = state.featureImportance.map((importance, index) => ({
        key: FEATURE_KEYS[index],
        importance
    }));
    
    indexed.sort((a, b) => b.importance - a.importance);
    
    return indexed.map(item => item.key);
}

export function getFeatureImportance(state: RReliefFState, featureKey: FeatureKey): number {
    const index = FEATURE_KEYS.indexOf(featureKey);
    if (index === -1) return 0;
    return state.featureImportance[index];
}

// TODO fix debiging should never be used
export function formatFeatureImportance(state: RReliefFState): string {
    const sorted = getFeaturesByImportance(state);
    
    //todo bar graph fix AI
    return sorted.map(key => {
        const importance = getFeatureImportance(state, key);
        const bar = '█'.repeat(Math.round(importance * 20));
        return `${key.padEnd(22)} ${bar} ${(importance * 100).toFixed(1)}%`;
    }).join('\n');
}

// should never be used
export function trainRReliefFBatch(
    state: RReliefFState,
    interactions: Array<{
        selectedOffer: ScoredOfferContext;
        selectedScore: number;
        contextOffers: ScoredOfferContext[];
    }>
): RReliefFState {
    let currentState = state;
    
    for (const interaction of interactions) {
        currentState = trainRReliefFStep(
            currentState,
            interaction.selectedOffer,
            interaction.selectedScore,
            interaction.contextOffers
        );
    }
    
    return currentState;
}

export function serializeRReliefFState(state: RReliefFState): {
    orderByOption: string[];
    featureImportance: number[];
} {
    return {
        orderByOption: getFeaturesByImportance(state),
        featureImportance: state.featureImportance
    };
}

export function deserializeRReliefFState(data: {
    orderByOption?: string[];
    featureImportance?: number[];
}): RReliefFState {
    let featureImportance = data.featureImportance;
    
    if (!featureImportance && data.orderByOption) {
        featureImportance = new Array(FEATURE_COUNT).fill(0);
        const numFeatures = data.orderByOption.length;
        
        data.orderByOption.forEach((key, index) => {
            const featureIndex = FEATURE_KEYS.indexOf(key as FeatureKey);
            if (featureIndex !== -1) {
                featureImportance![featureIndex] = (numFeatures - index) / numFeatures;
            }
        });
        
        const sum = featureImportance.reduce((s, v) => s + v, 0);
        if (sum > 0) {
            featureImportance = featureImportance.map(v => v / sum);
        }
    }
    
    return {
        featureImportance: featureImportance ?? new Array(FEATURE_COUNT).fill(1 / FEATURE_COUNT),
        k: DEFAULT_RRELIEFF_STATE.k,
        sigma: DEFAULT_RRELIEFF_STATE.sigma
    };
}
