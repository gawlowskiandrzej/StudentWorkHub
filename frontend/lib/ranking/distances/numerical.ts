import { offer } from '@/types/list/Offer/offer';
import { UserPreferences, DistanceCalculator } from '../types';
import { normalizeString } from '../utils';

export class SalaryDistanceCalculator implements DistanceCalculator {
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const offerFrom = offer.salary?.from ?? null;
        const offerTo = offer.salary?.to ?? null;
        const userFrom = preferences.salaryFrom;
        const userTo = preferences.salaryTo;
        
        if ((offerFrom === null && offerTo === null) || 
            (userFrom === null && userTo === null)) {
            return 0.5;
        }
        
        const offerMin = offerFrom ?? offerTo!;
        const offerMax = offerTo ?? offerFrom!;
        const userMin = userFrom ?? userTo!;
        const userMax = userTo ?? userFrom!;
        
        const overlap = Math.max(0, 
            Math.min(offerMax, userMax) - Math.max(offerMin, userMin)
        );
        
        if (overlap > 0) {
            const userRange = userMax - userMin;
            const overlapRatio = userRange > 0 ? overlap / userRange : 1;
            return 1 - Math.min(1, overlapRatio);
        }
        
        const distance = offerMin > userMax 
            ? (offerMin - userMax) / userMax
            : (userMin - offerMax) / userMin;
            
        return Math.min(1, distance);
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const mid1 = this.getOfferSalaryMidpoint(offer1);
        const mid2 = this.getOfferSalaryMidpoint(offer2);
        
        if (mid1 === null || mid2 === null) return 0.5;
        
        const maxDifference = 50000;
        return Math.min(1, Math.abs(mid1 - mid2) / maxDifference);
    }
    
    private getOfferSalaryMidpoint(offer: offer): number | null {
        const from = offer.salary?.from;
        const to = offer.salary?.to;
        
        if (from !== null && to !== null) return (from + to) / 2;
        if (from !== null) return from;
        if (to !== null) return to;
        return null;
    }
}

//TODO values from dictionary JSON
export class ScheduleDistanceCalculator implements DistanceCalculator {
    
    //TODO dynamic
    private static readonly SCHEDULE_VALUES: Record<string, number> = {
        'pełny etat': 1.0,
        'full-time': 1.0,
        'pelny etat': 1.0,
        '3/4 etatu': 0.75,
        'pół etatu': 0.5,
        'pol etatu': 0.5,
        'half-time': 0.5,
        '1/4 etatu': 0.25,
        'part-time': 0.25,
        'dorywcza': 0.1,
        'staż': 0.1
    };
    
    private getScheduleValue(schedule: string): number {
        return ScheduleDistanceCalculator.SCHEDULE_VALUES[normalizeString(schedule)] ?? 0.5;
    }
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const offerSchedules = (offer.employment?.schedules ?? []);
        
        if (offerSchedules.length === 0) return 0.5;
        
        const offerMaxSchedule = Math.max(
            ...offerSchedules.map(s => this.getScheduleValue(s))
        );
        
        const preferredSchedule = 1.0;
        
        return Math.abs(offerMaxSchedule - preferredSchedule);
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const schedules1 = (offer1.employment?.schedules ?? []);
        const schedules2 = (offer2.employment?.schedules ?? []);
        
        if (schedules1.length === 0 || schedules2.length === 0) return 0.5;
        
        const max1 = Math.max(...schedules1.map(s => this.getScheduleValue(s)));
        const max2 = Math.max(...schedules2.map(s => this.getScheduleValue(s)));
        
        return Math.abs(max1 - max2);
    }
}
