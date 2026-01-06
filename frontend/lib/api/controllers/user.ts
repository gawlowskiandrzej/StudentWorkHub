// src/lib/api/controllers/user.ts

import { apiClient } from "../apiClient";
import { omitUndefined } from "@/utils/others/omitUndefined";
import type {
  StandardRegisterRequestDto,
  StandardRegisterResponseDto,
  StandardLoginRequestDto,
  StandardLoginResponseDto,
  TokenLoginRequestDto,
  TokenLoginResponseDto,
  UpdateDataRequestDto,
  UpdateDataResponseDto,
  UpdateWeightsRequestDto,
  UpdateWeightsResponseDto,
  ChangePasswordRequestDto,
  ChangePasswordResponseDto,
  LogoutRequestDto,
  LogoutResponseDto,
  DeleteUserRequestDto,
  DeleteUserResponseDto,
  RefreshJwtRequestDto,
  RefreshJwtResponseDto,
  CheckJwtRequestDto,
  CheckJwtResponseDto,
  CheckPermissionRequestDto,
  CheckPermissionResponseDto,
  InsertSearchHistoryRequestDto,
  InsertSearchHistoryResponseDto,
  GetSearchHistoryRequestDto,
  GetSearchHistoryResponseDto,
  GetLastSearchesRequestDto,
  GetLastSearchesResponseDto,
  GetWeightsRequestDto,
  GetWeightsResponseDto,
  GetDataRequestDto,
  GetDataResponseDto,
  UpdatePreferencesRequestDto,
  UpdatePreferencesResponseDto,
  GetPreferencesRequestDto,
  GetPreferencesResponseDto,
} from "@/types/api/usersDTO";

export const UserApi = {
  standardRegister: async (dto: StandardRegisterRequestDto) => {
    return apiClient.post<StandardRegisterResponseDto>(
      `/users/standard-register`,
      omitUndefined(dto)
    );
  },

  standardLogin: async (dto: StandardLoginRequestDto) => {
    return apiClient.post<StandardLoginResponseDto>(
      `/users/standard-login`,
      omitUndefined(dto)
    );
  },

  tokenLogin: async (dto: TokenLoginRequestDto) => {
    return apiClient.post<TokenLoginResponseDto>(
      `/users/token-login`,
      omitUndefined(dto)
    );
  },

  updateData: async (dto: UpdateDataRequestDto) => {
    return apiClient.post<UpdateDataResponseDto>(
      `/users/update-data`,
      omitUndefined(dto)
    );
  },

  updateWeights: async (dto: UpdateWeightsRequestDto) => {
    return apiClient.post<UpdateWeightsResponseDto>(
      `/users/update-weights`,
      omitUndefined(dto)
    );
  },

  changePassword: async (dto: ChangePasswordRequestDto) => {
    return apiClient.post<ChangePasswordResponseDto>(
      `/users/change-password`,
      omitUndefined(dto)
    );
  },

  logout: async (dto: LogoutRequestDto) => {
    return apiClient.post<LogoutResponseDto>(`/users/logout`, omitUndefined(dto));
  },

  deleteUser: async (dto: DeleteUserRequestDto) => {
    return apiClient.post<DeleteUserResponseDto>(
      `/users/delete-user`,
      omitUndefined(dto)
    );
  },

  refreshJwt: async (dto: RefreshJwtRequestDto) => {
    return apiClient.post<RefreshJwtResponseDto>(
      `/users/refresh-jwt`,
      omitUndefined(dto)
    );
  },

  checkJwt: async (dto: CheckJwtRequestDto) => {
    return apiClient.post<CheckJwtResponseDto>(
      `/users/check-jwt`,
      omitUndefined(dto)
    );
  },

  checkPermission: async (dto: CheckPermissionRequestDto) => {
    return apiClient.post<CheckPermissionResponseDto>(
      `/users/check-permission`,
      omitUndefined(dto)
    );
  },

  insertSearchHistory: async (dto: InsertSearchHistoryRequestDto) => {
    return apiClient.post<InsertSearchHistoryResponseDto>(
      `/users/insert-search-history`,
      omitUndefined(dto)
    );
  },

  getSearchHistory: async (dto: GetSearchHistoryRequestDto) => {
    return apiClient.post<GetSearchHistoryResponseDto>(
      `/users/get-search-history`,
      omitUndefined(dto)
    );
  },

  getLastSearches: async (dto: GetLastSearchesRequestDto) => {
    return apiClient.post<GetLastSearchesResponseDto>(
      `/users/get-last-searches`,
      omitUndefined(dto)
    );
  },

  getWeights: async (dto: GetWeightsRequestDto) => {
    return apiClient.post<GetWeightsResponseDto>(
      `/users/get-weights`,
      omitUndefined(dto)
    );
  },

  getData: async (dto: GetDataRequestDto) => {
    return apiClient.post<GetDataResponseDto>(`/users/get-data`, omitUndefined(dto));
  },

  updatePreferences: async (dto: UpdatePreferencesRequestDto) => {
    return apiClient.post<UpdatePreferencesResponseDto>(
      `/users/update-preferences`,
      omitUndefined(dto)
    );
  },

  getPreferences: async (dto: GetPreferencesRequestDto) => {
    return apiClient.post<GetPreferencesResponseDto>(
      `/users/get-preferences`,
      omitUndefined(dto)
    );
  },
};
