import { OfferFeatureVector, SGDTrainerState, RReliefFState } from '@/lib/ranking/types';


export function createFeatureVector(distance: number = 0.5): OfferFeatureVector {
  return {
    values: new Array(8).fill(distance),
    raw: {
      SALARY_MATCH: distance,
      BENEFIT_MATCH: distance,
      SKILLS_MATCH: distance,
      EMPLOYMENT_TYPE_MATCH: distance,
      SCHEDULE_MATCH: distance,
      CATEGORY_MATCH: distance,
      LANGUAGE_MATCH: distance,
      FRESHNESS: distance,
    },
  };
}


export function createCustomFeatureVector(values: {
  salary?: number;
  benefits?: number;
  skills?: number;
  employmentType?: number;
  schedule?: number;
  category?: number;
  language?: number;
  freshness?: number;
}): OfferFeatureVector {
  const vectorValues = [
    values.salary ?? 0.5,
    values.benefits ?? 0.5,
    values.skills ?? 0.5,
    values.employmentType ?? 0.5,
    values.schedule ?? 0.5,
    values.category ?? 0.5,
    values.language ?? 0.5,
    values.freshness ?? 0.5,
  ];

  return {
    values: vectorValues,
    raw: {
      SALARY_MATCH: vectorValues[0],
      BENEFIT_MATCH: vectorValues[1],
      SKILLS_MATCH: vectorValues[2],
      EMPLOYMENT_TYPE_MATCH: vectorValues[3],
      SCHEDULE_MATCH: vectorValues[4],
      CATEGORY_MATCH: vectorValues[5],
      LANGUAGE_MATCH: vectorValues[6],
      FRESHNESS: vectorValues[7],
    },
  };
}

export function isWithinRange(value: number, min: number, max: number): boolean {
  return value >= min && value <= max;
}

export function assertValidWeights(weights: number[]): void {
  weights.forEach((weight, idx) => {
    if (weight < 0.01 || weight > 1.0) {
      throw new Error(`Weight at index ${idx} is out of range: ${weight}`);
    }
  });
}

export function sum(values: number[]): number {
  return values.reduce((acc, val) => acc + val, 0);
}

export function mean(values: number[]): number {
  return sum(values) / values.length;
}

export function variance(values: number[]): number {
  const m = mean(values);
  return sum(values.map(v => Math.pow(v - m, 2))) / values.length;
}

export function stdDev(values: number[]): number {
  return Math.sqrt(variance(values));
}

export function arraysAlmostEqual(
  arr1: number[],
  arr2: number[],
  tolerance: number = 0.001
): boolean {
  if (arr1.length !== arr2.length) return false;
  
  return arr1.every((val, idx) => Math.abs(val - arr2[idx]) < tolerance);
}

export function createSeededRandom(seed: number = 42) {
  let state = seed;
  
  return function random(): number {
    state = (state * 9301 + 49297) % 233280;
    return state / 233280;
  };
}

export function generateRandomArray(
  length: number,
  min: number = 0,
  max: number = 1,
  seed: number = 42
): number[] {
  const random = createSeededRandom(seed);
  const result: number[] = [];
  
  for (let i = 0; i < length; i++) {
    result.push(min + random() * (max - min));
  }
  
  return result;
}
