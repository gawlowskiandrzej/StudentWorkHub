import { offer } from '@/types/list/Offer/offer';
import { UserPreferences } from '@/lib/ranking/types';

export function createMockOffer(overrides: Partial<any> = {}): offer {
  const offerId = overrides.offerId ?? overrides.id ?? 1;
  const now = new Date();
  const publishedDate = overrides.dateAdded instanceof Date 
    ? overrides.dateAdded.toISOString()
    : (typeof overrides.published === 'string' ? overrides.published : new Date('2026-01-20').toISOString());
  const expiresDate = overrides.expires ?? new Date(now.getTime() + 60 * 24 * 60 * 60 * 1000).toISOString();
  
  const baseOffer: offer = {
    id: offerId,
    source: overrides.source ?? 'test-source',
    url: overrides.url ?? `https://example.com/job/${offerId}`,
    jobTitle: overrides.jobTitle ?? overrides.offerName ?? 'Senior TypeScript Developer',
    company: {
      name: overrides.companyName ?? 'Tech Corp',
      logoUrl: null,
    },
    description: overrides.description ?? 'Great job opportunity',
    salary: {
      from: overrides.salaryFrom ?? 15000,
      to: overrides.salaryTo ?? 20000,
      currency: overrides.currency ?? 'PLN',
      period: 'month',
      type: 'gross',
    },
    location: {
      buildingNumber: null,
      street: null,
      city: overrides.cityName ?? 'Warsaw',
      postalCode: null,
      coordinates: null,
      isRemote: overrides.isRemote ?? true,
      isHybrid: overrides.isHybrid ?? false,
    },
    category: {
      leadingCategory: overrides.categoryName ?? overrides.leadingCategory ?? 'IT/Development',
      subCategories: overrides.subCategories ?? null,
    },
    requirements: {
      skills: overrides.skills ?? (overrides.techStackNames 
        ? overrides.techStackNames.map((s: string) => ({
            skill: s,
            experienceMonths: 24,
            experienceLevel: ['Mid'],
          }))
        : [
            { skill: 'TypeScript', experienceMonths: 24, experienceLevel: ['Mid'] },
            { skill: 'React', experienceMonths: 18, experienceLevel: ['Mid'] },
            { skill: 'Node.js', experienceMonths: 12, experienceLevel: ['Junior'] },
          ]),
      education: null,
      languages: overrides.requiredLanguages 
        ? overrides.requiredLanguages.map((lang: any) => ({
            language: lang.languageName ?? lang.language ?? 'English',
            level: lang.languageLevelName ?? lang.level ?? 'B2',
          }))
        : [{ language: 'English', level: 'B2' }],
    },
    employment: {
      types: overrides.employmentTypeNames ?? overrides.employmentTypes ?? ['B2B'],
      schedules: overrides.schedules ?? ['Full-time'],
    },
    dates: {
      published: publishedDate,
      expires: expiresDate,
    },
    benefits: overrides.benefitNames ?? overrides.benefits ?? ['Medical care', 'Sports card'],
    isUrgent: overrides.isUrgent ?? false,
    isForUkrainians: overrides.isForUkrainians ?? false,
  };

  return baseOffer;
}

export function createMockPreferences(overrides: Partial<UserPreferences> = {}): UserPreferences {
  return {
    leadingCategoryId: 10,
    leadingCategoryName: 'IT/Development',
    
    salaryFrom: 12000,
    salaryTo: 18000,
    
    employmentTypeIds: [1],
    employmentTypeNames: ['B2B'],
    
    jobStatus: 'actively_looking',
    isActivelyLooking: true,
    
    cityName: 'Warsaw',
    
    workTypes: ['Remote'],
    
    languages: [
      {
        languageId: 1,
        languageName: 'English',
        languageLevelId: 5,
        languageLevelName: 'B2',
      },
    ],
    
    skills: overrides.skills ?? [
      { skillName: 'TypeScript', experienceMonths: 24 },
      { skillName: 'React', experienceMonths: 18 },
      { skillName: 'Node.js', experienceMonths: 12 },
    ],
    
    ...overrides,
  };
}

export function createMockOfferBatch(count: number): offer[] {
  const offers: offer[] = [];
  
  for (let i = 0; i < count; i++) {
    offers.push(
      createMockOffer({
        offerId: i + 1,
        offerName: `Developer Position ${i + 1}`,
        salaryFrom: 10000 + i * 1000,
        salaryTo: 15000 + i * 1000,
        dateAdded: new Date(Date.now() - i * 24 * 60 * 60 * 1000),
      })
    );
  }
  
  return offers;
}


export const MOCK_OFFERS = {
  perfectMatch: createMockOffer({
    offerId: 100,
    offerName: 'Perfect Match Job',
    salaryFrom: 15000,
    salaryTo: 20000,
    categoryId: 10,
    categoryName: 'IT/Development',
    workTypeNames: ['Remote'],
    techStackNames: ['TypeScript', 'React', 'Node.js'],
    benefitNames: ['Medical care', 'Sports card'],
    dateAdded: new Date(),
  }),
  
  poorMatch: createMockOffer({
    offerId: 101,
    offerName: 'Poor Match Job',
    salaryFrom: 5000,
    salaryTo: 7000,
    categoryId: 99,
    categoryName: 'Other/Manual Labor',
    workTypeNames: ['Office'],
    techStackNames: ['Excel'],
    benefitNames: [],
    dateAdded: new Date(Date.now() - 30 * 24 * 60 * 60 * 1000),
  }),
  
  partialMatch: createMockOffer({
    offerId: 102,
    offerName: 'Partial Match Job',
    salaryFrom: 18000,
    salaryTo: 25000,
    categoryId: 10,
    categoryName: 'IT/Development',
    cityName: 'Krakow',
    workTypeNames: ['Hybrid'],
    techStackNames: ['TypeScript', 'Vue.js'],
    benefitNames: ['Medical care'],
    dateAdded: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000),
  }),
};
