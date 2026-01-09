
/* dictionary item for profile creation id + name of property */
export interface DictionaryItem {
  id: number;
  name: string;
}

export interface FullDictionariesDto {
  leading_categories: DictionaryItem[];
  employment_types: DictionaryItem[];
  languages: DictionaryItem[];
  language_levels: DictionaryItem[];
}

export interface staticDictionariesDto {
  employmentType: string[]
  employmentSchedules: string[]
  salaryPeriods: string[]
}
