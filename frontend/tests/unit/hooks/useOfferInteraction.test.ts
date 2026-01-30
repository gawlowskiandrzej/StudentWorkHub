import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import { renderHook, act, waitFor } from '@testing-library/react';
import {
  useOfferInteraction,
  useViewTimeTracker,
  createInteractionCollector,
  InteractionEvent,
} from '@/hooks/useOfferInteraction';
import { createMockOffer } from '@tests/utils/mockData';

describe('useOfferInteraction Hook', () => {
  beforeEach(() => {
    vi.useFakeTimers();
  });

  afterEach(() => {
    vi.useRealTimers();
    vi.clearAllMocks();
  });

  describe('initialization', () => {
    it('should return interaction handlers', () => {
      const offer = createMockOffer();
      const { result } = renderHook(() => useOfferInteraction(offer));

      expect(result.current.handleMouseEnter).toBeDefined();
      expect(result.current.handleMouseLeave).toBeDefined();
      expect(result.current.handleClick).toBeDefined();
      expect(result.current.handleShowMoreLike).toBeDefined();
      expect(result.current.recordInteraction).toBeDefined();
    });

    it('should work with null offer', () => {
      const { result } = renderHook(() => useOfferInteraction(null));

      expect(result.current.handleClick).toBeDefined();

      act(() => {
        result.current.handleClick();
      });
    });
  });

  describe('click interactions', () => {
    it('should record click interaction', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer({ id: 123 });

      const { result } = renderHook(() =>
        useOfferInteraction(offer, { onInteraction })
      );

      act(() => {
        result.current.handleClick();
      });

      expect(onInteraction).toHaveBeenCalledTimes(1);
      expect(onInteraction).toHaveBeenCalledWith(
        expect.objectContaining({
          offerId: 123,
          type: 'click',
          timestamp: expect.any(Number),
        })
      );
    });

    it('should not record interaction when offer is null', () => {
      const onInteraction = vi.fn();

      const { result } = renderHook(() =>
        useOfferInteraction(null, { onInteraction })
      );

      act(() => {
        result.current.handleClick();
      });

      expect(onInteraction).not.toHaveBeenCalled();
    });

    it('should not throw when onInteraction is missing', () => {
      const offer = createMockOffer({ id: 777 });

      const { result } = renderHook(() =>
        useOfferInteraction(offer)
      );

      expect(() => {
        act(() => {
          result.current.handleClick();
        });
      }).not.toThrow();
    });
  });

  describe('hover interactions', () => {
    it('should record hover interaction after minimum duration', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer({ id: 456 });

      const { result } = renderHook(() =>
        useOfferInteraction(offer, {
          onInteraction,
          minHoverDuration: 300,
          hoverDebounce: 100,
        })
      );

      act(() => {
        result.current.handleMouseEnter();
      });

      // minimum hover duration
      act(() => {
        vi.advanceTimersByTime(400);
      });

      act(() => {
        result.current.handleMouseLeave();
      });

      // time past debounce
      act(() => {
        vi.advanceTimersByTime(150);
      });

      expect(onInteraction).toHaveBeenCalledWith(
        expect.objectContaining({
          offerId: 456,
          type: 'hover',
          durationMs: expect.any(Number),
        })
      );
    });

    it('should not record hover interaction if duration is too short', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer();

      const { result } = renderHook(() =>
        useOfferInteraction(offer, {
          onInteraction,
          minHoverDuration: 300,
        })
      );

      act(() => {
        result.current.handleMouseEnter();
      });

      // less than minHoverDuration
      act(() => {
        vi.advanceTimersByTime(100);
      });

      act(() => {
        result.current.handleMouseLeave();
      });

      act(() => {
        vi.advanceTimersByTime(200);
      });

      expect(onInteraction).not.toHaveBeenCalled();
    });

    it('should not emit hover when unmounted before debounce', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer({ id: 999 });

      const { result, unmount } = renderHook(() =>
        useOfferInteraction(offer, {
          onInteraction,
          minHoverDuration: 300,
          hoverDebounce: 200,
        })
      );

      act(() => {
        result.current.handleMouseEnter();
      });

      // unmount before debounce
      unmount();

      act(() => {
        vi.advanceTimersByTime(500);
      });

      expect(onInteraction).not.toHaveBeenCalled();
    });
  });

  describe('show more like this', () => {
    it('should record show_more_like interaction', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer({ id: 789 });

      const { result } = renderHook(() =>
        useOfferInteraction(offer, { onInteraction })
      );

      act(() => {
        result.current.handleShowMoreLike();
      });

      expect(onInteraction).toHaveBeenCalledWith(
        expect.objectContaining({
          offerId: 789,
          type: 'show_more_like',
        })
      );
    });
  });

  describe('manual interaction recording', () => {
    it('should allow manual interaction recording', () => {
      const onInteraction = vi.fn();
      const offer = createMockOffer({ id: 111 });

      const { result } = renderHook(() =>
        useOfferInteraction(offer, { onInteraction })
      );

      act(() => {
        result.current.recordInteraction('view_time', 5000);
      });

      expect(onInteraction).toHaveBeenCalledWith(
        expect.objectContaining({
          offerId: 111,
          type: 'view_time',
          durationMs: 5000,
        })
      );
    });
  });

  describe('cleanup', () => {
    it('should cleanup timeouts on unmount', () => {
      const offer = createMockOffer();
      const { result, unmount } = renderHook(() => useOfferInteraction(offer));

      act(() => {
        result.current.handleMouseEnter();
      });

      expect(() => unmount()).not.toThrow();
    });
  });
});

describe('useViewTimeTracker Hook', () => {
  beforeEach(() => {
    vi.useFakeTimers();
  });

  afterEach(() => {
    vi.useRealTimers();
    vi.clearAllMocks();
  });

  it('should track view duration', () => {
    const offer = createMockOffer();
    const { result } = renderHook(() => useViewTimeTracker(offer));

    act(() => {
      vi.advanceTimersByTime(5000);
    });

    const duration = result.current.getViewDuration();
    expect(duration).toBeGreaterThanOrEqual(5000);
  });

  it('should record view time on unmount if above minimum', () => {
    const onInteraction = vi.fn();
    const offer = createMockOffer({ id: 222 });

    const { unmount } = renderHook(() =>
      useViewTimeTracker(offer, {
        onInteraction,
        minViewDuration: 1000,
      })
    );

    act(() => {
      vi.advanceTimersByTime(2000);
    });

    unmount();

    expect(onInteraction).toHaveBeenCalledWith(
      expect.objectContaining({
        offerId: 222,
        type: 'view_time',
        durationMs: expect.any(Number),
      })
    );
  });

  it('should not record view time if below minimum duration', () => {
    const onInteraction = vi.fn();
    const offer = createMockOffer();

    const { unmount } = renderHook(() =>
      useViewTimeTracker(offer, {
        onInteraction,
        minViewDuration: 5000,
      })
    );

    act(() => {
      vi.advanceTimersByTime(1000);
    });

    unmount();

    expect(onInteraction).not.toHaveBeenCalled();
  });

  it('should allow manual view time recording', () => {
    const onInteraction = vi.fn();
    const offer = createMockOffer({ id: 333 });

    const { result } = renderHook(() =>
      useViewTimeTracker(offer, {
        onInteraction,
        minViewDuration: 1000,
      })
    );

    act(() => {
      vi.advanceTimersByTime(2000);
    });

    act(() => {
      result.current.recordViewTime();
    });

    expect(onInteraction).toHaveBeenCalled();
  });

  it('should not record view time multiple times', () => {
    const onInteraction = vi.fn();
    const offer = createMockOffer();

    const { result } = renderHook(() =>
      useViewTimeTracker(offer, {
        onInteraction,
        minViewDuration: 1000,
      })
    );

    act(() => {
      vi.advanceTimersByTime(2000);
    });

    act(() => {
      result.current.recordViewTime();
    });

    act(() => {
      result.current.recordViewTime();
    });

    expect(onInteraction).toHaveBeenCalledTimes(1);
  });
});

describe('createInteractionCollector', () => {
  beforeEach(() => {
    vi.useFakeTimers();
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  it('should collect interactions and flush after interval', () => {
    const onFlush = vi.fn();
    const collector = createInteractionCollector(onFlush, 5000, 20);

    const event: InteractionEvent = {
      offerId: 1,
      type: 'click',
      timestamp: Date.now(),
    };

    collector.add(event);

    expect(onFlush).not.toHaveBeenCalled();

    act(() => {
      vi.advanceTimersByTime(5000);
    });

    expect(onFlush).toHaveBeenCalledWith([event]);

    collector.destroy();
  });

  it('should flush immediately when batch size is reached', () => {
    const onFlush = vi.fn();
    const collector = createInteractionCollector(onFlush, 5000, 3);

    // max batch size
    for (let i = 0; i < 3; i++) {
      collector.add({
        offerId: i,
        type: 'click',
        timestamp: Date.now(),
      });
    }

    expect(onFlush).toHaveBeenCalledTimes(1);
    expect(onFlush).toHaveBeenCalledWith(
      expect.arrayContaining([
        expect.objectContaining({ offerId: 0 }),
        expect.objectContaining({ offerId: 1 }),
        expect.objectContaining({ offerId: 2 }),
      ])
    );

    collector.destroy();
  });

  it('should flush remaining events on destroy', () => {
    const onFlush = vi.fn();
    const collector = createInteractionCollector(onFlush, 10000, 20);

    collector.add({
      offerId: 1,
      type: 'click',
      timestamp: Date.now(),
    });

    collector.destroy();

    expect(onFlush).toHaveBeenCalledTimes(1);
  });

  it('should allow manual flush', () => {
    const onFlush = vi.fn();
    const collector = createInteractionCollector(onFlush, 10000, 20);

    collector.add({
      offerId: 1,
      type: 'click',
      timestamp: Date.now(),
    });

    collector.flush();

    expect(onFlush).toHaveBeenCalledTimes(1);

    collector.destroy();
  });

  it('should not flush if no events collected', () => {
    const onFlush = vi.fn();
    const collector = createInteractionCollector(onFlush, 5000, 20);

    collector.flush();

    expect(onFlush).not.toHaveBeenCalled();

    collector.destroy();
  });
});
