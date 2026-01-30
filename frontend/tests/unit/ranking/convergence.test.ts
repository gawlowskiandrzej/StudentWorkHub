import { describe, it, expect } from 'vitest';
import { Ranking } from '@/lib/ranking/ranking';
import { FEATURE_COUNT, FEATURE_KEYS } from '@/lib/ranking/types';
import { createSGDTrainer, trainStep, computeOfferScore } from '@/lib/ranking/algorithms/sgd';


function calculateCorrelation(rank1: number[], rank2: number[]): number {
    const n = rank1.length;
    let sumDiffSq = 0;
    for (let i = 0; i < n; i++) {
        const diff = rank1[i] - rank2[i];
        sumDiffSq += diff * diff;
    }
    return 1 - (6 * sumDiffSq) / (n * (n * n - 1));
}

describe('Analiza stabilności i zbieżności algorytmu rankingowania', () => {
    it('powinien osiągnąć stabilność i poprawność sortowania', () => {
        const trainer = createSGDTrainer();
        
        const groundTruthWeights = [1.0, 0.1, 1.0, 0.1, 0.1, 0.5, 0.1, 0.2];
        const totalGTWeight = groundTruthWeights.reduce((a, b) => a + b, 0);

        const testOffers = Array.from({ length: 20 }, (_, id) => ({
            id,
            features: {
                values: Array.from({ length: FEATURE_COUNT }, () => Math.random()),
                raw: {} as any
            }
        }));

        const getIdealScore = (features: any) => {
            let s = 0;
            for (let j = 0; j < FEATURE_COUNT; j++) s += groundTruthWeights[j] * (1 - features.values[j]);
            return s / totalGTWeight;
        };

        const idealScores = testOffers.map(o => getIdealScore(o.features));
        const idealRanking = [...testOffers]
            .sort((a, b) => getIdealScore(b.features) - getIdealScore(a.features))
            .map(o => o.id);

        console.log('--- Analiza zbieżności i poprawności sortowania ---');
        
        let currentTrainer = trainer;
        const iterations = 150;
        let thresholdReachedAt = -1;

        for (let i = 0; i < iterations; i++) {
            const trainingFeatures = {
                values: Array.from({ length: FEATURE_COUNT }, () => Math.random()),
                raw: {} as any
            };
            const targetSignal = getIdealScore(trainingFeatures) + (Math.random() - 0.5) * 0.05;
            
            currentTrainer = trainStep(currentTrainer, trainingFeatures, Math.max(0, Math.min(1, targetSignal)));

            const currentRanking = [...testOffers]
                .sort((a, b) => computeOfferScore(currentTrainer, b.features) - computeOfferScore(currentTrainer, a.features))
                .map(o => o.id);

            const posIdeal = new Array(20);
            const posCurrent = new Array(20);
            idealRanking.forEach((id, pos) => posIdeal[id] = pos);
            currentRanking.forEach((id, pos) => posCurrent[id] = pos);

            const correlation = calculateCorrelation(posIdeal, posCurrent);

            if (correlation > 0.9 && thresholdReachedAt === -1) {
                thresholdReachedAt = i + 1;
            }

            if ((i + 1) % 25 === 0 || i === 0) {
                console.log(`Sygnał ${i + 1}: Korelacja z ideałem = ${correlation.toFixed(4)}`);
            }
        }

        console.log('\n--- PODSUMOWANIE DLA PRACY NAUKOWEJ ---');
        console.log(`Liczba sygnałów potrzebna do osiągnięcia wysokiej poprawności (Korelacja > 0.9): ${thresholdReachedAt !== -1 ? thresholdReachedAt : 'Niezabardzo (potrzeba więcej)'}`);
        console.log(`Końcowa korelacja po ${iterations} sygnałach: ${calculateCorrelation(
            testOffers.map(o => idealRanking.indexOf(o.id)),
            testOffers.map(o => {
                const currentRanking = [...testOffers].sort((a, b) => computeOfferScore(currentTrainer, b.features) - computeOfferScore(currentTrainer, a.features)).map(x => x.id);
                return currentRanking.indexOf(o.id);
            })
        ).toFixed(4)}`);

        const finalRanking = [...testOffers]
            .sort((a, b) => computeOfferScore(currentTrainer, b.features) - computeOfferScore(currentTrainer, a.features))
            .slice(0, 5);

        console.log('\nPrzykład top 5 ofert po nauce:');
        finalRanking.forEach((o, i) => {
            console.log(`${i+1}. Oferta ID: ${o.id}, Score: ${computeOfferScore(currentTrainer, o.features).toFixed(4)} (Ideal: ${getIdealScore(o.features).toFixed(4)})`);
        });

        expect(thresholdReachedAt).toBeGreaterThan(0);
        expect(thresholdReachedAt).toBeLessThanOrEqual(100);
        });
});
