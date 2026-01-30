import { describe, it, expect, vi, beforeEach, afterEach, Mock } from 'vitest';
import { UserApi } from '@/lib/api/controllers/user';
import { apiClient } from '@/lib/api/apiClient';

// mock
vi.mock('@/lib/api/apiClient', () => ({
  apiClient: {
    get: vi.fn(),
    post: vi.fn(),
    put: vi.fn(),
    delete: vi.fn(),
  },
}));

describe('UserApi', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  afterEach(() => {
    vi.restoreAllMocks();
  });

  describe('standardRegister', () => {
    it('should call correct endpoint with registration data', async () => {
      const registerDto = {
        email: 'test@example.com',
        password: 'SecurePass123!',
        firstName: 'Jan',
        lastName: 'Kowalski',
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.standardRegister(registerDto);

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/standard-register',
        expect.objectContaining({
          email: 'test@example.com',
          password: 'SecurePass123!',
          firstName: 'Jan',
          lastName: 'Kowalski',
        })
      );
    });

    it('should return success response on valid registration', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const result = await UserApi.standardRegister({
        email: 'new@user.com',
        password: 'ValidPass1!',
        firstName: 'Anna',
        lastName: 'Nowak',
      });

      expect(result.data?.errorMessage).toBe('');
      expect(result.error).toBeNull();
    });

    it('should return error for duplicate email', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Ten e-mail jest już zajęty.' },
        error: null,
      });

      const result = await UserApi.standardRegister({
        email: 'exists@user.com',
        password: 'ValidPass1!',
        firstName: 'Test',
        lastName: 'User',
      });

      expect(result.data?.errorMessage).toBe('Ten e-mail jest już zajęty.');
    });

    it('should return error for weak password', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Hasło jest za krótkie.' },
        error: null,
      });

      const result = await UserApi.standardRegister({
        email: 'test@user.com',
        password: '123',
        firstName: 'Test',
        lastName: 'User',
      });

      expect(result.data?.errorMessage).toContain('Hasło');
    });

    it('should return error for invalid email format', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Niepoprawny format adresu e-mail.' },
        error: null,
      });

      const result = await UserApi.standardRegister({
        email: 'invalid-email',
        password: 'ValidPass1!',
        firstName: 'Test',
        lastName: 'User',
      });

      expect(result.data?.errorMessage).toContain('e-mail');
    });

    it('should omit undefined fields', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const dto = {
        email: 'test@test.com',
        password: 'pass',
        firstName: 'Test',
        lastName: 'User',
      };

      await UserApi.standardRegister(dto);

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/standard-register',
        dto
      );
    });
  });

  describe('standardLogin', () => {
    it('should call correct endpoint with login credentials', async () => {
      const loginDto = {
        login: 'user@example.com',
        password: 'password123',
        rememberMe: false,
      };

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: 'token', rememberMeToken: '', errorMessage: '' },
        error: null,
      });

      await UserApi.standardLogin(loginDto);

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/standard-login',
        loginDto
      );
    });

    it('should return JWT on successful login', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: 'valid.jwt.token', rememberMeToken: '', errorMessage: '' },
        error: null,
      });

      const result = await UserApi.standardLogin({
        login: 'user@test.com',
        password: 'correctpassword',
        rememberMe: false,
      });

      expect(result.data?.jwt).toBe('valid.jwt.token');
      expect(result.data?.errorMessage).toBe('');
    });

    it('should return rememberMeToken when rememberMe is true', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { 
          jwt: 'valid.jwt.token', 
          rememberMeToken: 'remember-token-123', 
          errorMessage: '' 
        },
        error: null,
      });

      const result = await UserApi.standardLogin({
        login: 'user@test.com',
        password: 'correctpassword',
        rememberMe: true,
      });

      expect(result.data?.rememberMeToken).toBe('remember-token-123');
    });

    it('should return error for invalid credentials', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: '', rememberMeToken: '', errorMessage: 'Niepoprawny login lub hasło.' },
        error: null,
      });

      const result = await UserApi.standardLogin({
        login: 'user@test.com',
        password: 'wrongpassword',
        rememberMe: false,
      });

      expect(result.data?.jwt).toBe('');
      expect(result.data?.errorMessage).toContain('Niepoprawny');
    });
  });

  describe('tokenLogin', () => {
    it('should call correct endpoint with token', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: 'new-jwt', rememberMeToken: 'new-token', errorMessage: '' },
        error: null,
      });

      await UserApi.tokenLogin({ token: 'remember-me-token' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/token-login',
        { token: 'remember-me-token' }
      );
    });

    it('should return new JWT and refreshed token on success', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { 
          jwt: 'refreshed-jwt', 
          rememberMeToken: 'refreshed-token', 
          errorMessage: '' 
        },
        error: null,
      });

      const result = await UserApi.tokenLogin({ token: 'valid-token' });

      expect(result.data?.jwt).toBe('refreshed-jwt');
      expect(result.data?.rememberMeToken).toBe('refreshed-token');
    });

    it('should return error for invalid token', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: '', rememberMeToken: '', errorMessage: 'Niepoprawny token.' },
        error: null,
      });

      const result = await UserApi.tokenLogin({ token: 'invalid-token' });

      expect(result.data?.errorMessage).toBe('Niepoprawny token.');
    });

    it('should return error for empty token', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: '', rememberMeToken: '', errorMessage: 'Pusty token.' },
        error: null,
      });

      const result = await UserApi.tokenLogin({ token: '' });

      expect(result.data?.errorMessage).toContain('Pusty');
    });
  });

  describe('updateData', () => {
    it('should call correct endpoint with user data', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: {
          errorMessage: '',
          userFirstNameUpdated: true,
          userSecondNameUpdated: false,
          userLastNameUpdated: true,
          userPhoneUpdated: false,
        },
        error: null,
      });

      await UserApi.updateData({
        jwt: 'valid-jwt',
        userFirstName: 'Jan',
        userLastName: 'Kowalski',
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/update-data',
        expect.objectContaining({
          jwt: 'valid-jwt',
          userFirstName: 'Jan',
          userLastName: 'Kowalski',
        })
      );
    });

    it('should return update flags for each field', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: {
          errorMessage: '',
          userFirstNameUpdated: true,
          userSecondNameUpdated: true,
          userLastNameUpdated: false,
          userPhoneUpdated: true,
        },
        error: null,
      });

      const result = await UserApi.updateData({
        jwt: 'jwt',
        userFirstName: 'Jan',
        userSecondName: 'Stanisław',
        userPhone: '+48111222333',
      });

      expect(result.data?.userFirstNameUpdated).toBe(true);
      expect(result.data?.userSecondNameUpdated).toBe(true);
      expect(result.data?.userPhoneUpdated).toBe(true);
    });

    it('should return error for expired JWT', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: {
          errorMessage: 'Jwt wygasł.',
          userFirstNameUpdated: false,
          userSecondNameUpdated: false,
          userLastNameUpdated: false,
          userPhoneUpdated: false,
        },
        error: null,
      });

      const result = await UserApi.updateData({
        jwt: 'expired-jwt',
        userFirstName: 'Test',
      });

      expect(result.data?.errorMessage).toBe('Jwt wygasł.');
    });

    it('should return error for invalid phone format', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: {
          errorMessage: 'Niepoprawny format numeru telefonu.',
          userFirstNameUpdated: false,
          userSecondNameUpdated: false,
          userLastNameUpdated: false,
          userPhoneUpdated: false,
        },
        error: null,
      });

      const result = await UserApi.updateData({
        jwt: 'jwt',
        userPhone: '123456', // Missing + prefix
      });

      expect(result.data?.errorMessage).toContain('telefonu');
    });
  });

  describe('updateWeights', () => {
    it('should call correct endpoint with weights data', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', vectorUpdated: true },
        error: null,
      });

      await UserApi.updateWeights({
        jwt: 'valid-jwt',
        vector: [0.5, 0.3, 0.2],
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/update-weights',
        expect.objectContaining({
          jwt: 'valid-jwt',
          vector: [0.5, 0.3, 0.2],
        })
      );
    });

    it('should update multiple weight arrays at once', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: {
          errorMessage: '',
          vectorUpdated: true,
          meanDistUpdated: true,
          orderByOptionUpdated: true,
          meanValueIdsUpdated: false,
          meansValueSumUpdated: false,
          meansValueSsumUpdated: false,
          meansValueCountUpdated: false,
          meansWeightSumUpdated: false,
          meansWeightSsumUpdated: false,
          meansWeightCountUpdated: false,
        },
        error: null,
      });

      const result = await UserApi.updateWeights({
        jwt: 'jwt',
        vector: [1, 2, 3],
        meanDist: [0.1, 0.2],
        orderByOption: ['salary', 'date'],
      });

      expect(result.data?.vectorUpdated).toBe(true);
      expect(result.data?.meanDistUpdated).toBe(true);
      expect(result.data?.orderByOptionUpdated).toBe(true);
    });

    it('should return error when JWT is missing', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Wymagane logowanie przed wykonaniem tej akcji.' },
        error: null,
      });

      const result = await UserApi.updateWeights({
        jwt: '',
        vector: [1, 2, 3],
      });

      expect(result.data?.errorMessage).toContain('logowanie');
    });
  });

  describe('changePassword', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.changePassword({
        jwt: 'valid-jwt',
        newPassword: 'NewSecurePass1!',
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/change-password',
        { jwt: 'valid-jwt', newPassword: 'NewSecurePass1!' }
      );
    });

    it('should return success on valid password change', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const result = await UserApi.changePassword({
        jwt: 'jwt',
        newPassword: 'ValidNewPass1!',
      });

      expect(result.data?.errorMessage).toBe('');
    });

    it('should return error for weak new password', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Hasło jest za krótkie.' },
        error: null,
      });

      const result = await UserApi.changePassword({
        jwt: 'jwt',
        newPassword: '123',
      });

      expect(result.data?.errorMessage).toContain('Hasło');
    });
  });

  describe('logout', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.logout({ jwt: 'valid-jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/logout',
        { jwt: 'valid-jwt' }
      );
    });

    it('should return success on logout', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const result = await UserApi.logout({ jwt: 'jwt' });

      expect(result.data?.errorMessage).toBe('');
    });
  });

  describe('deleteUser', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.deleteUser({ jwt: 'valid-jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/delete-user',
        { jwt: 'valid-jwt' }
      );
    });

    it('should return success on user deletion', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const result = await UserApi.deleteUser({ jwt: 'jwt' });

      expect(result.data?.errorMessage).toBe('');
    });
  });

  describe('refreshJwt', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: 'new-jwt', errorMessage: '' },
        error: null,
      });

      await UserApi.refreshJwt({ jwt: 'old-jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/refresh-jwt',
        { jwt: 'old-jwt' }
      );
    });

    it('should return new JWT on success', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { jwt: 'refreshed-jwt', errorMessage: '' },
        error: null,
      });

      const result = await UserApi.refreshJwt({ jwt: 'old-jwt' });

      expect(result.data?.jwt).toBe('refreshed-jwt');
    });
  });

  describe('checkJwt', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { result: true },
        error: null,
      });

      await UserApi.checkJwt({ jwt: 'valid-jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/check-jwt',
        { jwt: 'valid-jwt' }
      );
    });

    it('should return true for valid JWT', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { result: true },
        error: null,
      });

      const result = await UserApi.checkJwt({ jwt: 'valid-jwt' });

      expect(result.data?.result).toBe(true);
    });

    it('should return false for expired JWT', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { result: false },
        error: null,
      });

      const result = await UserApi.checkJwt({ jwt: 'expired-jwt' });

      expect(result.data?.result).toBe(false);
    });
  });

  describe('checkPermission', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.checkPermission({ jwt: 'jwt', permissionName: 'admin' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/check-permission',
        { jwt: 'jwt', permissionName: 'admin' }
      );
    });

    it('should return success for valid permission', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      const result = await UserApi.checkPermission({ 
        jwt: 'jwt', 
        permissionName: 'user' 
      });

      expect(result.data?.errorMessage).toBe('');
    });

    it('should surface 403 when permission denied', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: 'Forbidden' },
        error: 'API error: 403 Forbidden',
      });

      const result = await UserApi.checkPermission({ jwt: 'jwt', permissionName: 'admin' });

      expect(result.data).toBeNull();
      expect(result.error).toContain('403');
    });
  });

  describe('insertSearchHistory', () => {
    it('should call correct endpoint with search data', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.insertSearchHistory({
        jwt: 'jwt',
        keywords: 'developer',
        salaryFrom: 5000,
        salaryTo: 10000,
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/insert-search-history',
        expect.objectContaining({
          jwt: 'jwt',
          keywords: 'developer',
          salaryFrom: 5000,
          salaryTo: 10000,
        })
      );
    });

    it('should handle optional fields', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.insertSearchHistory({
        jwt: 'jwt',
        keywords: 'test',
        isRemote: true,
        employmentTypeIds: [1, 2, 3],
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/insert-search-history',
        expect.objectContaining({
          isRemote: true,
          employmentTypeIds: [1, 2, 3],
        })
      );
    });
  });

  describe('getSearchHistory', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', searchHistory: [] },
        error: null,
      });

      await UserApi.getSearchHistory({ jwt: 'jwt', limit: 10 });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/get-search-history',
        { jwt: 'jwt', limit: 10 }
      );
    });

    it('should return search history entries', async () => {
      const mockHistory = [
        { keywords: 'developer', salary_from: 5000 },
        { keywords: 'tester', salary_from: 4000 },
      ];

      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', searchHistory: mockHistory },
        error: null,
      });

      const result = await UserApi.getSearchHistory({ jwt: 'jwt', limit: 10 });

      expect(result.data?.searchHistory).toHaveLength(2);
    });
  });

  describe('getLastSearches', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', lastSearches: [] },
        error: null,
      });

      await UserApi.getLastSearches({ limit: 5 });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/get-last-searches',
        { limit: 5 }
      );
    });
  });

  describe('getWeights', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', vector: [1, 2, 3] },
        error: null,
      });

      await UserApi.getWeights({ jwt: 'jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/get-weights',
        { jwt: 'jwt' }
      );
    });

    it('should return weight arrays', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { 
          errorMessage: '', 
          vector: [0.5, 0.3, 0.2],
          meanDist: [0.1, 0.2, 0.3],
        },
        error: null,
      });

      const result = await UserApi.getWeights({ jwt: 'jwt' });

      expect(result.data?.vector).toEqual([0.5, 0.3, 0.2]);
    });
  });

  describe('getData', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', userData: {} },
        error: null,
      });

      await UserApi.getData({ jwt: 'jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/get-data',
        { jwt: 'jwt' }
      );
    });
  });


  describe('updatePreferences', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '' },
        error: null,
      });

      await UserApi.updatePreferences({
        jwt: 'jwt',
      });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/update-preferences',
        expect.objectContaining({ jwt: 'jwt' })
      );
    });
  });

  describe('getPreferences', () => {
    it('should call correct endpoint', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: { errorMessage: '', preferences: {} },
        error: null,
      });

      await UserApi.getPreferences({ jwt: 'jwt' });

      expect(apiClient.post).toHaveBeenCalledWith(
        '/users/get-preferences',
        { jwt: 'jwt' }
      );
    });
  });

  describe('Error Handling', () => {
    it('should handle network error', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Network error',
      });

      const result = await UserApi.standardLogin({
        login: 'user@test.com',
        password: 'pass',
        rememberMe: false,
      });

      expect(result.data).toBeNull();
      expect(result.error).toBe('Network error');
    });

    it('should handle timeout error', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'Request timeout',
      });

      const result = await UserApi.standardRegister({
        email: 'test@test.com',
        password: 'pass',
        firstName: 'Test',
        lastName: 'User',
      });

      expect(result.error).toBe('Request timeout');
    });

    it('should handle server error', async () => {
      (apiClient.post as Mock).mockResolvedValueOnce({
        data: null,
        error: 'API error: 500 Internal Server Error',
      });

      const result = await UserApi.logout({ jwt: 'jwt' });

      expect(result.error).toContain('500');
    });
  });
});
