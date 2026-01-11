import { DistanceCalculator } from '../types';
import { SalaryDistanceCalculator, ScheduleDistanceCalculator } from './numerical';
import { 
    BenefitDistanceCalculator, 
    SkillsDistanceCalculator,
    EmploymentTypeDistanceCalculator,
    CategoryDistanceCalculator,
    LanguageDistanceCalculator
} from './jaccard';
import { FreshnessDistanceCalculator } from './freshness';

export * from './numerical';
export * from './jaccard';
export * from './freshness';
export * from './dictionaries';

export const DISTANCE_CALCULATORS: Record<string, DistanceCalculator> = {
    'SALARY_MATCH': new SalaryDistanceCalculator(),
    'BENEFIT_MATCH': new BenefitDistanceCalculator(),
    'SKILLS_MATCH': new SkillsDistanceCalculator(),
    'EMPLOYMENT_TYPE_MATCH': new EmploymentTypeDistanceCalculator(),
    'SCHEDULE_MATCH': new ScheduleDistanceCalculator(),
    'CATEGORY_MATCH': new CategoryDistanceCalculator(),
    'LANGUAGE_MATCH': new LanguageDistanceCalculator(),
    'FRESHNESS': new FreshnessDistanceCalculator()
};
