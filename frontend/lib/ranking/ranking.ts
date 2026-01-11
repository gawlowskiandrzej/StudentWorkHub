import { offer } from '@/types/list/Offer/offer';
import { PreferencesDto, WeightsDto } from '@/types/api/usersDTO';
import {
    UserPreferences,
    OfferFeatureVector,
    SGDTrainerState,
    RReliefFState,
    NormalizationStats,
    InteractionType,
    INTERACTION_SIGNALS,
    FEATURE_KEYS,
    FEATURE_COUNT,
    DEFAULT_USER_PREFERENCES,
    ScoredOffer,
    FeatureKey,
    WelfordStats
} from './types';
import {
    extractFeatureVector,
    buildNormalizationStatsFromOffers,
    createInitialNormalizationStats,
    updateNormalizationStats,
    getVariance
} from './features/extractor';
import {
    createSGDTrainer,
    computeOfferScore,
    trainStep,
    computeModelConfidence,
    computeModelConfidenceV2,
    computeModelConfidenceWithBreakdown,
    scaleSignalByViewTime
} from './algorithms/sgd';
import {
    createRReliefFState,
    trainRReliefFStep,
    getFeaturesByImportance,
    ScoredOfferContext
} from './algorithms/rrelieff';
import { buildDictionariesFromOffers } from './distances/dictionaries';

export class Ranking {
    private userPreferences: UserPreferences;
    private sgdTrainer: SGDTrainerState;
    private rreliefF: RReliefFState;
    private normalizationStats: NormalizationStats;
    
    private offersCache: Map<number, offer>;
    private featuresCache: Map<number, OfferFeatureVector>;
    private scoresCache: Map<number, number>;
    
    private isDirty: boolean;
    
    constructor() {
        this.userPreferences = { ...DEFAULT_USER_PREFERENCES };
        this.sgdTrainer = createSGDTrainer();
        this.rreliefF = createRReliefFState();
        this.normalizationStats = createInitialNormalizationStats();
        
        this.offersCache = new Map();
        this.featuresCache = new Map();
        this.scoresCache = new Map();
        
        this.isDirty = false;
    }
    
    //TODO better name loadUserPreferences
    public setUserPreferences(preferences: PreferencesDto | null): void {
        if (!preferences) {
            this.userPreferences = { ...DEFAULT_USER_PREFERENCES };
            return;
        }
        
        this.userPreferences = this.convertPreferences(preferences);
        
        if (this.offersCache.size > 0) {
            this.recalculateScores();
        }
    }
    
    public loadWeights(weights: WeightsDto | null): void {
        if (!weights) return;
        
        if (weights.vector && weights.vector.length === FEATURE_COUNT) {
            this.sgdTrainer = {
                ...this.sgdTrainer,
                weights: [...weights.vector]
            };
        }
        
        if (weights.order_by_option && weights.order_by_option.length > 0) {
            const importance = new Array(FEATURE_COUNT).fill(0);
            weights.order_by_option.forEach((key: string, idx: number) => {
                const featureIdx = FEATURE_KEYS.indexOf(key as FeatureKey);
                if (featureIdx !== -1) {
                    importance[featureIdx] = (weights.order_by_option!.length - idx) / weights.order_by_option!.length;
                }
            });
            
            const sum = importance.reduce((s: number, v: number) => s + v, 0);
            this.rreliefF = {
                ...this.rreliefF,
                featureImportance: sum > 0 ? importance.map((v: number) => v / sum) : importance
            };
        }
        
        if (weights.mean_dist && weights.mean_dist.length === FEATURE_COUNT) {
            this.normalizationStats.meanDistances = [...weights.mean_dist];
        }
    }
    
    //TODO clear dictionary each time loadOffers from DB
    public loadOffersAndScore(offers: offer[]): ScoredOffer[] {
        buildDictionariesFromOffers(offers);
        
        this.offersCache.clear();
        for (const o of offers) {
            this.offersCache.set(o.id, o);
        }
        
        const { stats, features } = buildNormalizationStatsFromOffers(
            offers,
            this.userPreferences
        );
        
        this.normalizationStats = stats;
        this.featuresCache = features;
        
        this.scoresCache.clear();
        const results: ScoredOffer[] = [];
        
        for (const o of offers) {
            const feat = features.get(o.id)!;
            const score = computeOfferScore(this.sgdTrainer, feat);
            const confidence = computeModelConfidence(this.sgdTrainer.adamState);
            
            this.scoresCache.set(o.id, score);
            
            results.push({
                offerId: o.id,
                score,
                features: feat,
                confidence
            });
        }
        
        results.sort((a, b) => b.score - a.score);
        
        return results;
    }
    
    public getOfferScore(offer: offer): number {
        const cached = this.scoresCache.get(offer.id);
        if (cached !== undefined) return cached;
        
        const features = this.extractOrGetFeatures(offer);
        const score = computeOfferScore(this.sgdTrainer, features);
        
        this.scoresCache.set(offer.id, score);
        return score;
    }
    
    public rankOffers(offers: offer[]): ScoredOffer[] {
        const results: ScoredOffer[] = [];
        
        for (const o of offers) {
            const features = this.extractOrGetFeatures(o);
            const score = computeOfferScore(this.sgdTrainer, features);
            const confidence = computeModelConfidence(this.sgdTrainer.adamState);
            
            results.push({
                offerId: o.id,
                score,
                features,
                confidence
            });
        }
        
        results.sort((a, b) => b.score - a.score);
        return results;
    }
    
    public recordInteraction(
        offerId: number,
        interactionType: InteractionType,
        durationMs?: number
    ): void {
        const offerData = this.offersCache.get(offerId);
        if (!offerData) {
            return;
        }
        
        let signal: number = INTERACTION_SIGNALS[interactionType];
        if (interactionType === 'view_time' && durationMs) {
            signal = scaleSignalByViewTime(signal, durationMs);
        }
        
        if (signal < 0.05) {
            return;
        }
        
        const features = this.extractOrGetFeatures(offerData);
        
        this.normalizationStats = updateNormalizationStats(this.normalizationStats, features);
        
        // const variances = this.normalizationStats.featureStats.map(s => getVariance(s));
        // console.log('[Ranking] Feature variances:', 
        //     variances.map((v, i) => `${FEATURE_KEYS[i]}: ${v.toFixed(4)}`)
        // );
        
        this.sgdTrainer = trainStep(
            this.sgdTrainer,
            features,
            signal,
            this.rreliefF.featureImportance
        );
        
        console.log('[Ranking] Nowe wagi:', 
            this.sgdTrainer.weights.map((w, i) => `${FEATURE_KEYS[i]}: ${w.toFixed(4)}`).join(', ')
        );
        
        this.trainRReliefFWithContext(offerData, features, signal);
        
        // const confidenceBreakdown = computeModelConfidenceWithBreakdown(
        //     this.sgdTrainer.adamState,
        //     this.normalizationStats
        // );
        // console.log('[Ranking] Model confidence:', {
        //     total: confidenceBreakdown.total.toFixed(3),
        //     training: confidenceBreakdown.training.toFixed(3),
        //     stability: confidenceBreakdown.stability.toFixed(3),
        //     observations: confidenceBreakdown.observations.toFixed(3),
        //     avgVariance: confidenceBreakdown.avgVariance.toFixed(4)
        // });
        
        this.isDirty = true;
        
        const newScore = computeOfferScore(this.sgdTrainer, features);
        this.scoresCache.set(offerId, newScore);
    }
    
    public showMoreLikeThis(offerId: number): void {
        this.recordInteraction(offerId, 'show_more_like');
    }
    
    public hasUnsyncedChanges(): boolean {
        return this.isDirty;
    }
    
    public getProfileForSync(): WeightsDto {
        this.isDirty = false;
        
        const variances = this.normalizationStats.featureStats.map((s: WelfordStats) => 
            getVariance(s)
        );
        const stdDevs = variances.map(v => Math.sqrt(v));
        
        return {
            vector: [...this.sgdTrainer.weights],
            mean_dist: [...this.normalizationStats.meanDistances],
            mean_value_ids: [...this.normalizationStats.meanValueIds],
            order_by_option: getFeaturesByImportance(this.rreliefF),
            means_value_sum: this.normalizationStats.featureStats.map((s: WelfordStats) => s.mean * s.count),
            means_value_ssum: this.normalizationStats.featureStats.map((s: WelfordStats) => s.m2),
            means_value_count: this.normalizationStats.featureStats.map((s: WelfordStats) => s.count),
            means_weight_sum: this.sgdTrainer.adamState.m,
            means_weight_ssum: this.sgdTrainer.adamState.v,
            means_weight_count: new Array(FEATURE_COUNT).fill(this.sgdTrainer.adamState.t),
            
            //TODO fix save in some other place
            //feature_variances: variances,
            //feature_std_devs: stdDevs,
            
            /*confidence_breakdown: computeModelConfidenceWithBreakdown(
                this.sgdTrainer.adamState,
                this.normalizationStats
            ) as any
             */
        };
    }
    
    public getFeatureWeights(): Record<string, number> {
        const result: Record<string, number> = {};
        FEATURE_KEYS.forEach((key: FeatureKey, i: number) => {
            result[key] = this.sgdTrainer.weights[i];
        });
        return result;
    }
    
    public getFeatureImportance(): Record<string, number> {
        const result: Record<string, number> = {};
        FEATURE_KEYS.forEach((key: FeatureKey, i: number) => {
            result[key] = this.rreliefF.featureImportance[i];
        });
        return result;
    }
    
    private convertPreferences(dto: PreferencesDto): UserPreferences {
        return {
            leadingCategoryId: dto.leading_category_id ?? null,
            leadingCategoryName: null, // TODO get from dictionary
            salaryFrom: dto.salary_from ?? null,
            salaryTo: dto.salary_to ?? null,
            employmentTypeIds: dto.employment_type_ids ?? [],
            employmentTypeNames: [], // TODO get from dictionary
            jobStatus: dto.job_status as UserPreferences['jobStatus'] ?? null,
            isActivelyLooking: dto.job_status === 'actively_looking',
            cityName: dto.city_name ?? null,
            workTypes: dto.work_types ?? [],
            languages: (dto.languages ?? []).map(l => ({
                languageId: l.language_id,
                languageName: null, // TODO get from dictionary
                languageLevelId: l.language_level_id,
                languageLevelName: null // TODO get from dictionary
            })),
            skills: (dto.skills ?? []).map(s => ({
                skillName: s.skill_name,
                experienceMonths: s.experience_months
            }))
        };
    }
    
    private extractOrGetFeatures(offer: offer): OfferFeatureVector {
        let features = this.featuresCache.get(offer.id);
        
        if (!features) {
            features = extractFeatureVector(offer, this.userPreferences, this.normalizationStats);
            this.featuresCache.set(offer.id, features);
            this.offersCache.set(offer.id, offer);
        }
        
        return features;
    }
    
    private trainRReliefFWithContext(
        selectedOffer: offer,
        selectedFeatures: OfferFeatureVector,
        signal: number
    ): void {
        const contextOffers: ScoredOfferContext[] = [];
        
        this.offersCache.forEach((o, id) => {
            if (id === selectedOffer.id) return;
            
            const feat = this.featuresCache.get(id);
            const score = this.scoresCache.get(id);
            
            if (feat && score !== undefined) {
                contextOffers.push({
                    offerId: id,
                    offer: o,
                    score,
                    features: feat
                });
            }
        });
        
        if (contextOffers.length < 3) {
            return;
        }
        
        const selectedContext: ScoredOfferContext = {
            offerId: selectedOffer.id,
            offer: selectedOffer,
            score: signal,
            features: selectedFeatures
        };
        
        this.rreliefF = trainRReliefFStep(
            this.rreliefF,
            selectedContext,
            signal,
            contextOffers
        );
    }
    
    private recalculateScores(): void {
        this.featuresCache.clear();
        this.scoresCache.clear();
        
        this.offersCache.forEach((offer, id) => {
            const features = extractFeatureVector(offer, this.userPreferences, this.normalizationStats);
            this.featuresCache.set(id, features);
            
            const score = computeOfferScore(this.sgdTrainer, features);
            this.scoresCache.set(id, score);
        });
    }
}

let rankingInstance: Ranking | null = null;

export function getRanking(): Ranking {
    if (!rankingInstance) {
        rankingInstance = new Ranking();
    }
    return rankingInstance;
}

export function resetRanking(): void {
    rankingInstance = null;
}
