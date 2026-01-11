/**
 * J(A,B) = |A ∩ B| / |A ∪ B|
 * 
 * @returns [0, 1] 1 = identical
 */
export function jaccardSimilarity(setA: Set<string>, setB: Set<string>): number {
    if (setA.size === 0 && setB.size === 0) return 1.0;
    if (setA.size === 0 || setB.size === 0) return 0.0;
    
    const intersection = new Set([...setA].filter(x => setB.has(x)));
    const union = new Set([...setA, ...setB]);
    
    return intersection.size / union.size;
}

/**
 * lowercase, trim
 */
export function normalizeString(str: string | null | undefined): string {
    return (str ?? '').toLowerCase().trim();
}

/**
 * TODO use langiage level id from database
 * noramlize language [0-1]
 */
export function languageLevelToNumeric(level: string | null): number {
    const levelMap: Record<string, number> = {
        'a1': 0.17,
        'a2': 0.33,
        'b1': 0.50,
        'b2': 0.67,
        'c1': 0.83,
        'c2': 1.00,
        'native': 1.00,
        'natywny': 1.00
    };
    return levelMap[normalizeString(level)] ?? 0.5;
}

/**
 * data days difference
 */
export function daysDifference(date1: Date, date2: Date): number {
    const msPerDay = 24 * 60 * 60 * 1000;
    return Math.abs(date1.getTime() - date2.getTime()) / msPerDay;
}

/**
 * clamp value to range [min, max]
 */
export function clamp(value: number, min: number, max: number): number {
    return Math.max(min, Math.min(max, value));
}

/**
 * maps (-∞, +∞) to (0, 1)
 */
export function sigmoid(x: number): number {
    return 1 / (1 + Math.exp(-x));
}
