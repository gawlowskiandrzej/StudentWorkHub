import { offer } from '@/types/list/Offer/offer';
import { UserPreferences, DistanceCalculator } from '../types';
import { daysDifference } from '../utils';

export class FreshnessDistanceCalculator implements DistanceCalculator {
    
    private parseDate(dateStr: string): Date {
        return new Date(dateStr);
    }
    
    sgdDistance(offer: offer, preferences: UserPreferences): number {
        const now = new Date();
        const published = this.parseDate(offer.dates.published);
        const expires = this.parseDate(offer.dates.expires);
        
        const daysOld = daysDifference(now, published);
        const daysToExpire = daysDifference(expires, now);
        
        if (expires < now) return 1.0;
        
        //TODO dynamic scale
        const ageFactor = Math.min(1, daysOld / 60);
        
        let expirationUrgency = 0;
        if (daysToExpire < 7) {
            expirationUrgency = 1 - (daysToExpire / 7);
            
            if (preferences.isActivelyLooking && daysToExpire < 3) {
                expirationUrgency = -0.3;
            }
        }
        
        return Math.max(0, Math.min(1, ageFactor * 0.7 + expirationUrgency * 0.3));
    }
    
    rrelieffDifference(offer1: offer, offer2: offer): number {
        const published1 = this.parseDate(offer1.dates.published);
        const published2 = this.parseDate(offer2.dates.published);
        
        const daysDiff = daysDifference(published1, published2);
        
        return Math.min(1, daysDiff / 30);
    }
}
