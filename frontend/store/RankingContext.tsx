"use client";

import React, {
  createContext,
  useContext,
  useEffect,
  useCallback,
  useReducer,
  useRef
} from 'react';
import { offer } from '@/types/list/Offer/offer';
import { useUser } from '@/store/userContext';
import {
  getRanking,
  resetRanking,
  ScoredOffer,
  InteractionType,
  FEATURE_KEYS,
  FEATURE_COUNT
} from '@/lib/ranking';
import {
  InteractionEvent,
  createInteractionCollector
} from '@/hooks/useOfferInteraction';

interface RankingContextType {
  // State
  isInitialized: boolean;
  isLearning: boolean;
  
  // Ranking functions
  rankOffersList: (offers: offer[]) => ScoredOffer[];
  getOfferScore: (offerData: offer) => number;
  
  // Learning
  recordInteraction: (
    offerData: offer,
    type: InteractionType,
    durationMs?: number
  ) => void;
  showMoreLikeThis: (offerData: offer) => void;
  
  // Profile
  currentWeights: number[];
  featureImportance: { name: string; weight: number }[];
  confidence: number;
  
  // Sync
  syncProfile: () => Promise<void>;
  hasUnsyncedChanges: boolean;
}

interface RankingState {
  isInitialized: boolean;
  isLearning: boolean;
  hasUnsyncedChanges: boolean;
  lastSyncTime: number | null;
  version: number; // Force re-renders when ranking updates
}

type RankingAction =
  | { type: 'INITIALIZE' }
  | { type: 'UPDATE' }
  | { type: 'SET_LEARNING'; isLearning: boolean }
  | { type: 'MARK_UNSYNCED' }
  | { type: 'MARK_SYNCED' }
  | { type: 'RESET' };

function rankingReducer(
  state: RankingState,
  action: RankingAction
): RankingState {
  switch (action.type) {
    case 'INITIALIZE':
      return {
        ...state,
        isInitialized: true
      };
    
    case 'UPDATE':
      return {
        ...state,
        hasUnsyncedChanges: true,
        version: state.version + 1
      };
    
    case 'SET_LEARNING':
      return {
        ...state,
        isLearning: action.isLearning
      };
    
    case 'MARK_UNSYNCED':
      return {
        ...state,
        hasUnsyncedChanges: true
      };
    
    case 'MARK_SYNCED':
      return {
        ...state,
        hasUnsyncedChanges: false,
        lastSyncTime: Date.now()
      };
    
    case 'RESET':
      return {
        isInitialized: false,
        isLearning: false,
        hasUnsyncedChanges: false,
        lastSyncTime: null,
        version: 0
      };
    
    default:
      return state;
  }
}

const RankingContext = createContext<RankingContextType | undefined>(undefined);

const SYNC_INTERVAL_MS = 30000; // Sync every 30 seconds TOD fix
const LOCAL_STORAGE_KEY = 'ranking_profile';

export function RankingProvider({ children }: { children: React.ReactNode }) {
  const { weights, preferences, updateWeights, isAuthenticated } = useUser();
  
  const [state, dispatch] = useReducer(rankingReducer, {
    isInitialized: false,
    isLearning: false,
    hasUnsyncedChanges: false,
    lastSyncTime: null,
    version: 0
  });
  
  const collectorRef = useRef<ReturnType<typeof createInteractionCollector> | null>(null);
  const syncTimeoutRef = useRef<NodeJS.Timeout | null>(null);
  
  useEffect(() => {
    console.log('[Ranking] Initializing...', { isAuthenticated, hasWeights: !!weights, hasPreferences: !!preferences });
    
    const ranking = getRanking();
    
    ranking.setUserPreferences(preferences ?? null);
    
    if (isAuthenticated && weights) {
      ranking.loadWeights(weights);
      console.log('[Ranking] Loaded weights from server');
    } else {
      // Try to load from localStorage for unauth users
      try {
        const stored = localStorage.getItem(LOCAL_STORAGE_KEY);
        if (stored) {
          const profile = JSON.parse(stored);
          ranking.loadWeights(profile);
          console.log('[Ranking] Loaded profile from localStorage');
        }
      } catch (e) {
        console.warn('Failed to load ranking profile from localStorage', e);
      }
    }
    
    dispatch({ type: 'INITIALIZE' });
    
  }, [isAuthenticated, weights, preferences]);
  

  useEffect(() => {
    if (!state.isInitialized) return;
    
    collectorRef.current = createInteractionCollector(
      (events) => {
        dispatch({ type: 'SET_LEARNING', isLearning: true });
        
        const ranking = getRanking();
        
        for (const event of events) {
          ranking.recordInteraction(event.offerId, event.type, event.durationMs);
        }
        
        dispatch({ type: 'UPDATE' });
        dispatch({ type: 'SET_LEARNING', isLearning: false });
      },
      5000,
      20
    );
    
    return () => {
      collectorRef.current?.destroy();
    };
  }, [state.isInitialized]);
  
  // Auto-sync effect
  useEffect(() => {
    if (!isAuthenticated || !state.hasUnsyncedChanges) return;
    
    if (syncTimeoutRef.current) {
      clearTimeout(syncTimeoutRef.current);
    }
    
    syncTimeoutRef.current = setTimeout(() => {
      syncProfile();
    }, SYNC_INTERVAL_MS);
    
    return () => {
      if (syncTimeoutRef.current) {
        clearTimeout(syncTimeoutRef.current);
      }
    };
  }, [isAuthenticated, state.hasUnsyncedChanges]);
  
  // Save to localStorage for unauth users
  useEffect(() => {
    if (isAuthenticated || !state.isInitialized || !state.hasUnsyncedChanges) return;
    
    try {
      const ranking = getRanking();
      const profile = ranking.getProfileForSync();
      localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(profile));
    } catch (e) {
      console.warn('Failed to save ranking profile to localStorage', e);
    }
  }, [isAuthenticated, state.isInitialized, state.hasUnsyncedChanges]);
  

  const rankOffersList = useCallback((offers: offer[]): ScoredOffer[] => {
    console.log('[Ranking] rankOffersList called', { 
      offersCount: offers.length, 
      isInitialized: state.isInitialized
    });
    
    if (!state.isInitialized) {
      console.warn('[Ranking] Not initialized, returning neutral scores');
      return offers.map(o => ({
        offerId: o.id,
        score: 0.5,
        features: { 
          values: new Array(FEATURE_COUNT).fill(0.5), 
          raw: {} as Record<string, unknown> 
        },
        confidence: 0
      }));
    }
    
    const ranking = getRanking();
    const result = ranking.loadOffersAndScore(offers);
    
    console.log('[Ranking] Scored offers', result.slice(0, 3).map(o => ({
      id: o.offerId,
      score: o.score.toFixed(3),
      confidence: o.confidence.toFixed(2)
    })));
    
    return result;
  }, [state.isInitialized, state.version]);
  
  const getOfferScore = useCallback((offerData: offer): number => {
    if (!state.isInitialized) {
      console.warn('[Ranking] getOfferScore: Not initialized');
      return 0.5;
    }
    
    const ranking = getRanking();
    const score = ranking.getOfferScore(offerData);
    
    // console.log('[Ranking] getOfferScore', { offerId: offerData.id, score: score.toFixed(3) });
    return score;
  }, [state.isInitialized, state.version]);
  

  const recordInteraction = useCallback((
    offerData: offer,
    type: InteractionType,
    durationMs?: number
  ) => {
    if (!state.isInitialized) return;
    
    const ranking = getRanking();
    ranking.recordInteraction(offerData.id, type, durationMs);
    
    dispatch({ type: 'UPDATE' });
  }, [state.isInitialized]);
  
  const showMoreLikeThis = useCallback((offerData: offer) => {
    if (!state.isInitialized) return;
    
    const ranking = getRanking();
    ranking.showMoreLikeThis(offerData.id);
    
    dispatch({ type: 'UPDATE' });
  }, [state.isInitialized]);
  
  const syncProfile = useCallback(async () => {
    if (!state.isInitialized || !isAuthenticated) return;
    
    const ranking = getRanking();
    
    if (!ranking.hasUnsyncedChanges()) return;
    
    try {
      const profile = ranking.getProfileForSync();
      
      const success = await updateWeights({
        vector: profile.vector,
        meanDist: profile.mean_dist,
        orderByOption: profile.order_by_option,
        meansWeightSum: profile.means_weight_sum,
        meansWeightSsum: profile.means_weight_ssum,
        meansWeightCount: profile.means_weight_count
      });
      
      if (success) {
        dispatch({ type: 'MARK_SYNCED' });
      }
    } catch (e) {
      console.error('Failed to sync ranking profile', e);
    }
  }, [state.isInitialized, isAuthenticated, updateWeights]);
  

  const ranking = state.isInitialized ? getRanking() : null;
  
  const currentWeights = ranking 
    ? Object.values(ranking.getFeatureWeights())
    : new Array(FEATURE_COUNT).fill(0.5);
  
    //TODO fix const value
  const FEATURE_NAMES = [
    'Wynagrodzenie',
    'Benefity',
    'Umiejętności',
    'Typ umowy',
    'Wymiar pracy',
    'Kategoria',
    'Języki',
    'Świeżość'
  ];
  
  const featureImportanceMap = ranking ? ranking.getFeatureImportance() : {};
  const featureImportance = FEATURE_KEYS.map((key, i) => ({
    name: FEATURE_NAMES[i] ?? `Cecha ${i}`,
    weight: featureImportanceMap[key] ?? (1 / FEATURE_COUNT)
  })).sort((a, b) => b.weight - a.weight);
  
  const confidence = 0.5; // TODO: Get from ranking class
  

  const value: RankingContextType = {
    isInitialized: state.isInitialized,
    isLearning: state.isLearning,
    rankOffersList,
    getOfferScore,
    recordInteraction,
    showMoreLikeThis,
    currentWeights,
    featureImportance,
    confidence,
    syncProfile,
    hasUnsyncedChanges: state.hasUnsyncedChanges
  };
  
  return (
    <RankingContext.Provider value={value}>
      {children}
    </RankingContext.Provider>
  );
}

export function useRanking() {
  const context = useContext(RankingContext);
  
  if (!context) {
    throw new Error('useRanking must be used within RankingProvider');
  }
  
  return context;
}
