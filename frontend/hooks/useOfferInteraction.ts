/**
 * useOfferInteraction Hook
 * Tracks user interactions with offers for learning
 */

import { useCallback, useRef, useEffect } from 'react';
import { offer } from '@/types/list/Offer/offer';
import { InteractionType } from '@/lib/ranking/types';

export interface InteractionEvent {
  offerId: number;
  type: InteractionType;
  timestamp: number;
  durationMs?: number;
}

interface InteractionTrackerOptions {
  // Minimum hover duration to register (ms)
  minHoverDuration?: number;
  
  // Minimum view duration to register (ms)
  minViewDuration?: number;
  
  // Debounce hover events (ms)
  hoverDebounce?: number;
  
  // Callback when interaction is recorded
  onInteraction?: (event: InteractionEvent) => void;
}

const DEFAULT_OPTIONS: Required<InteractionTrackerOptions> = {
  minHoverDuration: 300,
  minViewDuration: 1000,
  hoverDebounce: 100,
  onInteraction: () => {}
};

/**
 * Hook for tracking user interactions with a single offer
 */
export function useOfferInteraction(
  offer: offer | null,
  options: InteractionTrackerOptions = {}
) {
  const opts = { ...DEFAULT_OPTIONS, ...options };
  
  const hoverStartRef = useRef<number | null>(null);
  const hoverTimeoutRef = useRef<NodeJS.Timeout | null>(null);
  const viewStartRef = useRef<number | null>(null);
  
  // Record interaction
  const recordInteraction = useCallback((
    type: InteractionType,
    durationMs?: number
  ) => {
    if (!offer) return;
    
    const event: InteractionEvent = {
      offerId: offer.id,
      type,
      timestamp: Date.now(),
      durationMs
    };
    
    opts.onInteraction(event);
  }, [offer, opts]);
  
  // Handle mouse enter
  const handleMouseEnter = useCallback(() => {
    if (hoverTimeoutRef.current) {
      clearTimeout(hoverTimeoutRef.current);
    }
    
    hoverStartRef.current = Date.now();
  }, []);
  
  // Handle mouse leave
  const handleMouseLeave = useCallback(() => {
    if (hoverStartRef.current) {
      const duration = Date.now() - hoverStartRef.current;
      
      if (duration >= opts.minHoverDuration) {
        // Debounce to avoid multiple rapid hovers
        hoverTimeoutRef.current = setTimeout(() => {
          recordInteraction('hover', duration);
        }, opts.hoverDebounce);
      }
      
      hoverStartRef.current = null;
    }
  }, [opts.minHoverDuration, opts.hoverDebounce, recordInteraction]);
  
  // Handle click
  const handleClick = useCallback(() => {
    recordInteraction('click');
    
    // Start view tracking
    viewStartRef.current = Date.now();
  }, [recordInteraction]);
  
  // Handle "show more like this"
  const handleShowMoreLike = useCallback(() => {
    recordInteraction('show_more_like');
  }, [recordInteraction]);
  
  // Record view time when component unmounts or offer changes
  useEffect(() => {
    return () => {
      if (viewStartRef.current) {
        const duration = Date.now() - viewStartRef.current;
        
        if (duration >= opts.minViewDuration) {
          recordInteraction('view_time', duration);
        }
        
        viewStartRef.current = null;
      }
    };
  }, [offer?.id, opts.minViewDuration, recordInteraction]);
  
  // Cleanup timeouts
  useEffect(() => {
    return () => {
      if (hoverTimeoutRef.current) {
        clearTimeout(hoverTimeoutRef.current);
      }
    };
  }, []);
  
  return {
    handleMouseEnter,
    handleMouseLeave,
    handleClick,
    handleShowMoreLike,
    
    // Manual interaction recording
    recordInteraction
  };
}

/**
 * Hook for tracking view time on detail page
 */
export function useViewTimeTracker(
  offer: offer | null,
  options: Pick<InteractionTrackerOptions, 'minViewDuration' | 'onInteraction'> = {}
) {
  const opts = { 
    minViewDuration: DEFAULT_OPTIONS.minViewDuration,
    onInteraction: DEFAULT_OPTIONS.onInteraction,
    ...options 
  };
  
  const viewStartRef = useRef<number>(Date.now());
  const hasRecordedRef = useRef(false);
  
  // Record view time
  const recordViewTime = useCallback(() => {
    if (!offer || hasRecordedRef.current) return;
    
    const duration = Date.now() - viewStartRef.current;
    
    if (duration >= opts.minViewDuration) {
      opts.onInteraction({
        offerId: offer.id,
        type: 'view_time',
        timestamp: Date.now(),
        durationMs: duration
      });
      
      hasRecordedRef.current = true;
    }
  }, [offer, opts]);
  
  // Record on unmount
  useEffect(() => {
    viewStartRef.current = Date.now();
    hasRecordedRef.current = false;
    
    return () => {
      recordViewTime();
    };
  }, [offer?.id]);
  
  // Also record on visibility change (tab switch)
  useEffect(() => {
    const handleVisibilityChange = () => {
      if (document.hidden) {
        recordViewTime();
      } else {
        // Reset timer when becoming visible again
        viewStartRef.current = Date.now();
        hasRecordedRef.current = false;
      }
    };
    
    document.addEventListener('visibilitychange', handleVisibilityChange);
    
    return () => {
      document.removeEventListener('visibilitychange', handleVisibilityChange);
    };
  }, [recordViewTime]);
  
  // Also record on beforeunload
  useEffect(() => {
    const handleBeforeUnload = () => {
      recordViewTime();
    };
    
    window.addEventListener('beforeunload', handleBeforeUnload);
    
    return () => {
      window.removeEventListener('beforeunload', handleBeforeUnload);
    };
  }, [recordViewTime]);
  
  return {
    recordViewTime,
    getViewDuration: () => Date.now() - viewStartRef.current
  };
}

/**
 * Batch interaction collector
 * Collects interactions and flushes them periodically
 */
export function createInteractionCollector(
  onFlush: (events: InteractionEvent[]) => void,
  flushIntervalMs: number = 5000,
  maxBatchSize: number = 20
) {
  const events: InteractionEvent[] = [];
  let flushTimeout: NodeJS.Timeout | null = null;
  
  const flush = () => {
    if (events.length > 0) {
      const batch = events.splice(0, events.length);
      onFlush(batch);
    }
    
    if (flushTimeout) {
      clearTimeout(flushTimeout);
      flushTimeout = null;
    }
  };
  
  const add = (event: InteractionEvent) => {
    events.push(event);
    
    if (events.length >= maxBatchSize) {
      flush();
    } else if (!flushTimeout) {
      flushTimeout = setTimeout(flush, flushIntervalMs);
    }
  };
  
  const destroy = () => {
    flush();
    if (flushTimeout) {
      clearTimeout(flushTimeout);
    }
  };
  
  return { add, flush, destroy };
}
