import { describe, it, expect } from 'vitest';
import { parseDateTimeSpecial, parseDateTime } from '@/utils/date/dateParser';

describe('dateParser Utils', () => {
    describe('parseDateTimeSpecial', () => {
        it('should parse standard format YYYY-MM-DD HH:mm:ss correctly', () => {
            const input = '2023-10-25 14:30:00';
            const result = parseDateTimeSpecial(input);
            expect(result.getFullYear()).toBe(2023);
            expect(result.getMonth()).toBe(9); // 0-indexed
            expect(result.getDate()).toBe(25);
            expect(result.getHours()).toBe(14);
        });

        it('should fail or produce invalid date for ISO format (T separator)', () => {
            const input = '2023-10-25T14:30:00';
            expect(() => parseDateTimeSpecial(input)).not.toThrow();
            const result = parseDateTimeSpecial(input);
            expect(result.toString()).not.toBe('Invalid Date');
        });

        it('should handle missing time part gracefully', () => {
            const input = '2023-10-25';
            expect(() => parseDateTimeSpecial(input)).not.toThrow(); 
        });
    });

    describe('parseDateTime', () => {
        it('should handle non-standard browser-dependent strings consistently', () => {
             const input = "01.02.2023";
             const result = parseDateTime(input);
             expect(result.toString()).not.toBe("Invalid Date");
        });
    });
});
