import {
    SGDTrainerState,
    SGDHyperparameters,
    AdamOptimizerState,
    OfferFeatureVector,
    NormalizationStats,
    WelfordStats,
    FEATURE_COUNT,
    DEFAULT_ADAM_STATE,
    DEFAULT_SGD_HYPERPARAMETERS
} from '../types';
import { clamp } from '../utils';

export function createSGDTrainer(
    initialWeights?: number[],
    hyperparameters?: Partial<SGDHyperparameters>
): SGDTrainerState {
    return {
        weights: initialWeights ?? new Array(FEATURE_COUNT).fill(0.5),
        adamState: { ...DEFAULT_ADAM_STATE },
        hyperparameters: {
            ...DEFAULT_SGD_HYPERPARAMETERS,
            ...hyperparameters
        }
    };
}

export function computeOfferScore(
    trainer: SGDTrainerState,
    features: OfferFeatureVector
): number {
    let weightedSum = 0;
    let totalWeight = 0;
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const rawWeight = trainer.weights[i];
        const weight = Number.isFinite(rawWeight) ? Math.max(0.001, rawWeight) : 0.001;
        const distance = Number.isFinite(features.values[i]) ? features.values[i] : 1;
        const similarity = 1 - distance;
        
        weightedSum += weight * similarity;
        totalWeight += weight;
    }
    
    if (totalWeight < 0.001) {
        return 0.5;
    }
    
    return clamp(weightedSum / totalWeight, 0, 1);
}

export function computeScoreBreakdown(
    trainer: SGDTrainerState,
    features: OfferFeatureVector
): { featureName: string; distance: number; weight: number; contribution: number }[] {
    const FEATURE_NAMES = [
        'SALARY_MATCH', 'BENEFIT_MATCH', 'SKILLS_MATCH', 'EMPLOYMENT_TYPE_MATCH',
        'SCHEDULE_MATCH', 'CATEGORY_MATCH', 'LANGUAGE_MATCH', 'FRESHNESS'
    ];
    
    const totalWeight = trainer.weights.reduce((sum, w) => sum + Math.max(0.001, w), 0);
    
    return FEATURE_NAMES.map((name, i) => {
        const weight = Math.max(0.001, trainer.weights[i]);
        const distance = features.values[i];
        const similarity = 1 - distance;
        const contribution = (weight * similarity) / totalWeight;
        
        return { featureName: name, distance, weight, contribution };
    });
}

export function trainStep(
    trainer: SGDTrainerState,
    features: OfferFeatureVector,
    targetSignal: number,
    importanceWeights?: number[]
): SGDTrainerState {
    const { weights, adamState, hyperparameters } = trainer;
    const { learningRate, beta1, beta2, epsilon, regularization } = hyperparameters;
    
    const predicted = computeOfferScore(trainer, features);
    const error = targetSignal - predicted;
    
    console.log('[SGD] Training step:', {
        targetSignal: targetSignal.toFixed(3),
        predicted: predicted.toFixed(3),
        error: error.toFixed(3)
    });
    
    const gradients: number[] = [];
    const totalWeight = weights.reduce((sum, w) => sum + Math.max(0.001, w), 0);
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        const similarity = 1 - features.values[i]; // distance -> similarity
        
        let gradient = -error * (similarity - predicted) / totalWeight;
        
        // RReliefF importance
        if (importanceWeights && importanceWeights[i] !== undefined) {
            gradient *= (1 + importanceWeights[i]);
        }
        
        gradient += regularization * weights[i];
        gradients.push(gradient);
    }
    
    // Adam 
    const newT = adamState.t + 1;
    const newM: number[] = [];
    const newV: number[] = [];
    const newWeights: number[] = [];
    
    for (let i = 0; i < FEATURE_COUNT; i++) {
        // mean of gradients
        const m_i = beta1 * adamState.m[i] + (1 - beta1) * gradients[i];
        newM.push(m_i);
        
        // variance of gradients
        const v_i = beta2 * adamState.v[i] + (1 - beta2) * gradients[i] * gradients[i];
        newV.push(v_i);
        
        // bias correction
        const m_hat = m_i / (1 - Math.pow(beta1, newT));
        const v_hat = v_i / (1 - Math.pow(beta2, newT));
        
        // weight update
        const weightUpdate = learningRate * m_hat / (Math.sqrt(v_hat) + epsilon);
        const newWeight = weights[i] - weightUpdate;
        
        newWeights.push(clamp(newWeight, 0.01, 1.0));
    }
    
    return {
        weights: newWeights,
        adamState: { m: newM, v: newV, t: newT },
        hyperparameters
    };
}

export function trainBatch(
    trainer: SGDTrainerState,
    trainingData: Array<{ features: OfferFeatureVector; signal: number }>,
    importanceWeights?: number[]
): SGDTrainerState {
    let currentTrainer = trainer;
    
    for (const { features, signal } of trainingData) {
        currentTrainer = trainStep(currentTrainer, features, signal, importanceWeights);
    }
    
    return currentTrainer;
}

//sum to 1
export function normalizeWeights(weights: number[]): number[] {
    const sum = weights.reduce((s, w) => s + w, 0);
    if (sum < 0.001) {
        return new Array(weights.length).fill(1 / weights.length);
    }
    return weights.map(w => w / sum);
}

export function computeModelConfidence(adamState: AdamOptimizerState): number {
    const t = adamState.t;
    if (t === 0) return 0;
    
    return 0.3 + 0.7 * (1 - Math.exp(-t / 30));
}

/**
 * training confidence (50%)
 * stability confidence (30%)
 * observation confidence (20%)
 */
export function computeModelConfidenceV2(
    adamState: AdamOptimizerState,
    normalizationStats: NormalizationStats
): number {
    const t = adamState.t;
    if (t === 0) return 0;
    
    const trainingConfidence = 0.3 + 0.7 * (1 - Math.exp(-t / 30));
    
    if (t < 3) return trainingConfidence; // too less data
    
    const avgVariance = normalizationStats.featureStats
        .map((s: WelfordStats) => {
            if (s.count < 1) return 0;
            return s.m2 / s.count;
        })
        .reduce((a: number, b: number) => a + b, 0) / normalizationStats.featureStats.length;
    
    //TODO find if 0.25 is good
    const normalizedVariance = Math.min(1, avgVariance / 0.25);
    
    const stabilityConfidence = 1 - normalizedVariance;
    
    const totalObservations = normalizationStats.featureStats
        .map((s: WelfordStats) => s.count)
        .reduce((a: number, b: number) => a + b, 0) / normalizationStats.featureStats.length;
    
    const observationConfidence = Math.min(1, totalObservations / 50);
    
    //TODO is proper
    return (
        0.5 * trainingConfidence +
        0.3 * stabilityConfidence +
        0.2 * observationConfidence
    );
}

export function computeModelConfidenceWithBreakdown(
    adamState: AdamOptimizerState,
    normalizationStats: NormalizationStats
): {
    total: number;
    training: number;
    stability: number;
    observations: number;
    avgVariance: number;
    avgObservations: number;
} {
    const t = adamState.t;
    
    const trainingConfidence = t === 0 
        ? 0 
        : 0.3 + 0.7 * (1 - Math.exp(-t / 30));
    
    const avgVariance = normalizationStats.featureStats
        .map((s: WelfordStats) => {
            if (s.count < 1) return 0;
            return s.m2 / s.count;
        })
        .reduce((a: number, b: number) => a + b, 0) / normalizationStats.featureStats.length;
    
    const normalizedVariance = Math.min(1, avgVariance / 0.25);
    const stabilityConfidence = 1 - normalizedVariance;
    
    const avgObservations = normalizationStats.featureStats
        .map((s: WelfordStats) => s.count)
        .reduce((a: number, b: number) => a + b, 0) / normalizationStats.featureStats.length;
    
    const observationConfidence = Math.min(1, avgObservations / 50);
    
    const total = (
        0.5 * trainingConfidence +
        0.3 * stabilityConfidence +
        0.2 * observationConfidence
    );
    
    return {
        total,
        training: trainingConfidence,
        stability: stabilityConfidence,
        observations: observationConfidence,
        avgVariance,
        avgObservations
    };
}

export function resetAdamState(trainer: SGDTrainerState): SGDTrainerState {
    return {
        ...trainer,
        adamState: { ...DEFAULT_ADAM_STATE }
    };
}

export function scaleSignalByViewTime(
    baseSignal: number,
    viewTimeMs: number,
    minTimeMs: number = 2000,
    maxTimeMs: number = 30000
): number {
    if (viewTimeMs < minTimeMs) return 0;
    
    const normalizedTime = Math.min(1, Math.log(viewTimeMs / minTimeMs) / Math.log(maxTimeMs / minTimeMs));
    
    return baseSignal * normalizedTime;
}

export function serializeSGDState(trainer: SGDTrainerState): {
    vector: number[];
    adamT: number;
    adamM: number[];
    adamV: number[];
} {
    return {
        vector: trainer.weights,
        adamT: trainer.adamState.t,
        adamM: trainer.adamState.m,
        adamV: trainer.adamState.v
    };
}

export function deserializeSGDState(data: {
    vector?: number[];
    adamT?: number;
    adamM?: number[];
    adamV?: number[];
}): SGDTrainerState {
    return {
        weights: data.vector ?? new Array(FEATURE_COUNT).fill(0.5),
        adamState: {
            m: data.adamM ?? new Array(FEATURE_COUNT).fill(0),
            v: data.adamV ?? new Array(FEATURE_COUNT).fill(0),
            t: data.adamT ?? 0
        },
        hyperparameters: { ...DEFAULT_SGD_HYPERPARAMETERS }
    };
}
