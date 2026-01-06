/* =========================
   /standard-register
========================= */

export type StandardRegisterRequestDto = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
};

export type StandardRegisterResponseDto = {
  errorMessage: string;
};

/* =========================
   /standard-login
========================= */

export type StandardLoginRequestDto = {
  login: string; // email
  password: string;
  rememberMe: boolean;
};

export type StandardLoginResponseDto = {
  errorMessage: string;
  jwt: string;
  rememberMeToken: string;
};

/* =========================
   /token-login
========================= */

export type TokenLoginRequestDto = {
  token: string; // rememberMeToken
};

export type TokenLoginResponseDto = {
  errorMessage: string;
  jwt: string;
  rememberMeToken: string;
};

/* =========================
   /update-data
========================= */

export type UpdateDataRequestDto = {
  jwt: string;

  userFirstName?: string;
  userSecondName?: string;
  userLastName?: string;
  userPhone?: string; // e.g. "+48111111111"
};

export type UpdateDataResponseDto = {
  errorMessage: string;

  userFirstNameUpdated: boolean;
  userSecondNameUpdated: boolean;
  userLastNameUpdated: boolean;
  userPhoneUpdated: boolean;
};

/* =========================
   /update-weights
========================= */

export type UpdateWeightsRequestDto = {
  jwt: string;

  // any subset; arrays must be non-empty to update (backend validation)
  vector?: number[];
  meanDist?: number[];
  meanValueIds?: string[];
  orderByOption?: string[];
  meansValueSum?: number[];
  meansValueSsum?: number[];
  meansValueCount?: number[];
  meansWeightSum?: number[];
  meansWeightSsum?: number[];
  meansWeightCount?: number[];
};

export type UpdateWeightsResponseDto = {
  errorMessage: string;

  orderByOptionUpdated: boolean;
  meanValueIdsUpdated: boolean;
  vectorUpdated: boolean;
  meanDistUpdated: boolean;
  meansValueSumUpdated: boolean;
  meansValueSsumUpdated: boolean;
  meansValueCountUpdated: boolean;
  meansWeightSumUpdated: boolean;
  meansWeightSsumUpdated: boolean;
  meansWeightCountUpdated: boolean;
};

/* =========================
   /change-password
========================= */

export type ChangePasswordRequestDto = {
  jwt: string;
  newPassword: string;
};

export type ChangePasswordResponseDto = {
  errorMessage: string;
};

/* =========================
   /logout
========================= */

export type LogoutRequestDto = {
  jwt: string;
};

export type LogoutResponseDto = {
  errorMessage: string;
};

/* =========================
   /delete-user
========================= */

export type DeleteUserRequestDto = {
  jwt: string;
};

export type DeleteUserResponseDto = {
  errorMessage: string;
};

/* =========================
   /refresh-jwt
========================= */

export type RefreshJwtRequestDto = {
  jwt: string;
};

export type RefreshJwtResponseDto = {
  errorMessage: string;
  jwt: string;
};

/* =========================
   /check-jwt
========================= */

export type CheckJwtRequestDto = {
  jwt: string;
};

export type CheckJwtResponseDto = {
  result: boolean;
};

/* =========================
   /check-permission
========================= */

export type CheckPermissionRequestDto = {
  jwt: string;
  permissionName: string;
};

export type CheckPermissionResponseDto = {
  errorMessage: string;
};

/* =========================
   /insert-search-history
========================= */

export type InsertSearchHistoryRequestDto = {
  jwt: string;

  // send any subset; at least one must be present (backend validation)
  keywords?: string;
  distance?: number;
  isRemote?: boolean;
  isHybrid?: boolean;

  leadingCategoryId?: number; // offers dictionary id
  cityId?: number; // offers dictionary id

  salaryFrom?: number;
  salaryTo?: number;

  salaryPeriodId?: number; // offers dictionary id
  salaryCurrencyId?: number; // offers dictionary id

  employmentScheduleIds?: number[]; // offers dictionary ids
  employmentTypeIds?: number[]; // offers dictionary ids
};

export type InsertSearchHistoryResponseDto = {
  errorMessage: string;
};

/* =========================
   /get-search-history
========================= */

export type GetSearchHistoryRequestDto = {
  jwt: string;
  limit: number; // must be > 0
};

// Response uses snake_case inside the array.
export type SearchHistoryEntryDto = {
  city_id: number | null;
  distance: number | null;
  keywords: string | null;

  is_hybrid: boolean | null;
  is_remote: boolean | null;

  salary_to: number | null;
  salary_from: number | null;
  salary_period_id: number | null;
  salary_currency_id: number | null;

  employment_type_ids: number[];
  leading_category_id: number | null;
  employment_schedule_ids: number[];
};

export type GetSearchHistoryResponseDto = {
  errorMessage: string;
  searchHistory: SearchHistoryEntryDto[];
};

/* =========================
   /get-last-searches
========================= */

export type GetLastSearchesRequestDto = {
  limit: number; // must be > 0
};

export type LastSearchesEntryDto = SearchHistoryEntryDto;

export type GetLastSearchesResponseDto = {
  errorMessage: string;
  lastSearches: LastSearchesEntryDto[];
};

/* =========================
   /get-weights
========================= */

// Response uses snake_case in "weights".
export type GetWeightsRequestDto = {
  jwt: string;
};

export type WeightsDto = {
  vector?: number[];
  mean_dist?: number[];
  mean_value_ids?: string[];
  means_value_sum?: number[];
  order_by_option?: string[];
  means_value_ssum?: number[];
  means_weight_sum?: number[];
  means_value_count?: number[];
  means_weight_ssum?: number[];
  means_weight_count?: number[];
};

export type GetWeightsResponseDto = {
  errorMessage: string;
  weights: WeightsDto | Record<string, unknown>; // may be {} on error
};

/* =========================
   /get-data
========================= */

// Response uses snake_case keys in userData.
export type GetDataRequestDto = {
  jwt: string;
};

export type UserDataDto = {
  role: string | null;
  email: string | null;
  phone: string | null;

  last_name: string | null;
  first_name: string | null;
  second_name: string | null;
};

export type GetDataResponseDto = {
  errorMessage: string;
  userData: UserDataDto | Record<string, unknown>; // may be {} on error
};

/* =========================
   /update-preferences
========================= */

export type UpdatePreferencesRequestDto = {
  jwt: string;

  leadingCategoryId?: number;
  salaryFrom?: number;
  salaryTo?: number;
  employmentTypeIds?: number[];

  // WARNING (backend validation): send BOTH languageId and languageLevelId
  languageId?: number;
  languageLevelId?: number;

  jobStatusName?: string;
  cityName?: string;
  workTypeNames?: string[];

  // WARNING (backend validation): send BOTH arrays and same length
  skillNames?: string[];
  skillMonths?: number[];
};

export type UpdatePreferencesResponseDto = {
  errorMessage: string;

  leadingCategoryIdUpdated: boolean;
  salaryFromUpdated: boolean;
  salaryToUpdated: boolean;
  employmentTypeIdsUpdated: boolean;

  languageIdUpdated: boolean;
  languageLevelIdUpdated: boolean;

  jobStatusNameUpdated: boolean;
  cityNameUpdated: boolean;
  workTypeNamesUpdated: boolean;

  skillNamesUpdated: boolean;
  skillMonthsUpdated: boolean;
};

/* =========================
   /get-preferences
========================= */

// Response uses snake_case inside "preferences".
export type GetPreferencesRequestDto = {
  jwt: string;
};

export type PreferencesLanguageDto = {
  language_id: number;
  language_level_id: number;
};

export type PreferencesSkillDto = {
  skill_name: string;
  experience_months: number;
  entry_date: string; // ISO string
};

export type PreferencesDto = {
  leading_category_id?: number;
  salary_from?: number;
  salary_to?: number;

  employment_type_ids?: number[];
  job_status?: string;
  city_name?: string;

  work_types?: string[];
  languages?: PreferencesLanguageDto[];
  skills?: PreferencesSkillDto[];
};

export type GetPreferencesResponseDto = {
  errorMessage: string;
  preferences: PreferencesDto | Record<string, unknown>; // may be {} on error
};
