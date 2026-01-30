import { describe, it, expect } from 'vitest';
import { SalaryDistanceCalculator } from '@/lib/ranking/distances/numerical';
import { offer } from '@/types/list/Offer/offer';
import { UserPreferences } from '@/lib/ranking/types';

describe('SalaryDistanceCalculator', () => {
    const calculator = new SalaryDistanceCalculator();

    it('should handle division by zero when userMax is 0', () => {
        const offerData: offer = {
            id: 1,
            salary: { from: 5000, to: 6000, currency: 'PLN', type: 'gross' }
        } as any;

        const preferences: UserPreferences = {
            salaryFrom: 0,
            salaryTo: 0,
        } as any;

        const distance = calculator.sgdDistance(offerData, preferences);
        expect(Number.isFinite(distance)).toBe(true);
        expect(distance).toBeGreaterThanOrEqual(0);
        expect(distance).toBeLessThanOrEqual(1);
    });

    it('should handle malformed offer salary (NaN)', () => {
        const offerData: offer = {
            id: 2,
            salary: { from: NaN, to: NaN }
        } as any;

        const preferences: UserPreferences = {
            salaryFrom: 3000,
            salaryTo: 5000
        } as any;

        const distance = calculator.sgdDistance(offerData, preferences);
        expect(distance).not.toBeNaN();
    });
});
