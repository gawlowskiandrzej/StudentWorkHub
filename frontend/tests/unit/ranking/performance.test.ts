import { describe, it, expect, beforeEach } from 'vitest';
import { Ranking } from '@/lib/ranking/ranking';
import { createMockOfferBatch, createMockPreferences } from '@tests/utils/mockData';

describe('Ranking Performance & Sustainability', () => {
    let ranking: Ranking;

    beforeEach(() => {
        ranking = new Ranking();
        ranking.setUserPreferences(createMockPreferences() as any);
    });

    it('should rank 5000 offers in less than 500ms', () => {
        const offers = createMockOfferBatch(5000);
        
        const start = performance.now();
        const ranked = ranking.loadOffersAndScore(offers);
        const end = performance.now();
        
        const duration = end - start;
        console.log(`Ranking 5000 offers took ${duration.toFixed(2)}ms`);
        
        expect(ranked).toHaveLength(5000);
        expect(duration).toBeLessThan(500);
    });

    it('should maintain stable memory/speed over 100 ranking cycles', () => {
        const offers = createMockOfferBatch(100);
        const durations: number[] = [];

        for (let i = 0; i < 100; i++) {
            const start = performance.now();
            ranking.loadOffersAndScore(offers);
            durations.push(performance.now() - start);
        }

        const avgDuration = durations.reduce((a, b) => a + b) / durations.length;
        console.log(`Average ranking duration over 100 cycles: ${avgDuration.toFixed(2)}ms`);

        expect(avgDuration).toBeLessThan(50);
    });

    it('should handle offers with missing or null data without crashing', () => {
        const brokenOffers = [
            { id: 1 } as any,
            { id: 2, title: null, salaryFrom: NaN } as any,
            null as any
        ];

        expect(() => {
            ranking.loadOffersAndScore(brokenOffers);
        }).not.toThrow();
    });
});
