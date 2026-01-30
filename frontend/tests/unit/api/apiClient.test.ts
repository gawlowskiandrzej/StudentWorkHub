import { describe, it, expect, vi, beforeEach, afterEach, Mock } from 'vitest';
import { ApiClient, ApiResponse } from '@/lib/api/apiClient';

const mockFetch = vi.fn() as Mock;
global.fetch = mockFetch;

describe('ApiClient', () => {
  let apiClient: ApiClient;

  beforeEach(() => {
    vi.clearAllMocks();
    apiClient = new ApiClient('https://api.test.com');
  });

  afterEach(() => {
    vi.restoreAllMocks();
  });

  describe('Constructor', () => {
    it('should use default baseUrl when not provided', () => {
      const client = new ApiClient();
      expect(client).toBeDefined();
    });

    it('should accept custom baseUrl', () => {
      const client = new ApiClient('https://custom.api.com');
      expect(client).toBeDefined();
    });
  });

  describe('GET Requests', () => {
    it('should make successful GET request', async () => {
      const mockData = { id: 1, name: 'Test' };
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(mockData),
      });

      const result = await apiClient.get<typeof mockData>('/users/1');

      expect(mockFetch).toHaveBeenCalledWith(
        'https://api.test.com/users/1',
        expect.objectContaining({
          method: 'GET',
          headers: { 'Content-Type': 'application/json' },
        })
      );
      expect(result.data).toEqual(mockData);
      expect(result.error).toBeNull();
    });

    it('should handle GET request with query parameters in path', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve([]),
      });

      await apiClient.get('/offers?page=1&limit=10');

      expect(mockFetch).toHaveBeenCalledWith(
        'https://api.test.com/offers?page=1&limit=10',
        expect.any(Object)
      );
    });

    it('should return error for failed GET request (4xx)', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 404,
        text: () => Promise.resolve('Not Found'),
      });

      const result = await apiClient.get('/users/999');

      expect(result.data).toBeNull();
      expect(result.error).toBe('API error: 404 Not Found');
    });

    it('should return error for server error (5xx)', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 500,
        text: () => Promise.resolve('Internal Server Error'),
      });

      const result = await apiClient.get('/users');

      expect(result.data).toBeNull();
      expect(result.error).toContain('500');
    });
  });

  describe('POST Requests', () => {
    it('should make successful POST request with body', async () => {
      const requestBody = { email: 'test@example.com', password: 'secret' };
      const responseData = { jwt: 'token123', errorMessage: '' };

      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(responseData),
      });

      const result = await apiClient.post('/users/login', requestBody);

      expect(mockFetch).toHaveBeenCalledWith(
        'https://api.test.com/users/login',
        expect.objectContaining({
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(requestBody),
        })
      );
      expect(result.data).toEqual(responseData);
      expect(result.error).toBeNull();
    });

    it('should handle POST with empty body', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ success: true }),
      });

      const result = await apiClient.post('/action', {});

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          body: '{}',
        })
      );
      expect(result.data).toEqual({ success: true });
    });

    it('should handle POST request returning 401 Unauthorized', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 401,
        text: () => Promise.resolve('{"errorMessage": "Invalid credentials"}'),
      });

      const result = await apiClient.post('/users/login', { email: 'bad', password: 'bad' });

      expect(result.data).toBeNull();
      expect(result.error).toContain('401');
    });

    it('should handle POST request returning 400 Bad Request', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 400,
        text: () => Promise.resolve('Validation failed'),
      });

      const result = await apiClient.post('/users/register', { email: 'invalid' });

      expect(result.data).toBeNull();
      expect(result.error).toContain('400');
    });

    it('should handle POST request returning 409 Conflict', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 409,
        text: () => Promise.resolve('Email already exists'),
      });

      const result = await apiClient.post('/users/register', { email: 'exists@test.com' });

      expect(result.data).toBeNull();
      expect(result.error).toContain('409');
    });
  });

  describe('PUT Requests', () => {
    it('should make successful PUT request', async () => {
      const updateData = { name: 'Updated Name' };
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ ...updateData, id: 1 }),
      });

      const result = await apiClient.put('/users/1', updateData);

      expect(mockFetch).toHaveBeenCalledWith(
        'https://api.test.com/users/1',
        expect.objectContaining({
          method: 'PUT',
          body: JSON.stringify(updateData),
        })
      );
      expect(result.data).toEqual({ name: 'Updated Name', id: 1 });
    });

    it('should handle PUT returning 404', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 404,
        text: () => Promise.resolve('Resource not found'),
      });

      const result = await apiClient.put('/users/999', { name: 'Test' });

      expect(result.data).toBeNull();
      expect(result.error).toContain('404');
    });
  });

  describe('DELETE Requests', () => {
    it('should make successful DELETE request', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ deleted: true }),
      });

      const result = await apiClient.delete('/users/1');

      expect(mockFetch).toHaveBeenCalledWith(
        'https://api.test.com/users/1',
        expect.objectContaining({
          method: 'DELETE',
        })
      );
      expect(result.data).toEqual({ deleted: true });
    });

    it('should handle DELETE returning 403 Forbidden', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 403,
        text: () => Promise.resolve('Access denied'),
      });

      const result = await apiClient.delete('/admin/resource/1');

      expect(result.data).toBeNull();
      expect(result.error).toContain('403');
    });
  });

  describe('Error Handling', () => {
    it('should handle network error', async () => {
      mockFetch.mockRejectedValueOnce(new Error('Network error'));

      const result = await apiClient.get('/users');

      expect(result.data).toBeNull();
      expect(result.error).toBe('Network error');
    });

    it('should handle timeout (AbortError)', async () => {
      const abortError = new Error('Aborted');
      abortError.name = 'AbortError';
      mockFetch.mockRejectedValueOnce(abortError);

      const result = await apiClient.get('/slow-endpoint');

      expect(result.data).toBeNull();
      expect(result.error).toBe('Request timeout');
    });

    it('should handle unknown error type', async () => {
      mockFetch.mockRejectedValueOnce('string error');

      const result = await apiClient.get('/users');

      expect(result.data).toBeNull();
      expect(result.error).toBe('Unknown error');
    });

    it('should handle JSON parse error in response', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.reject(new Error('Invalid JSON')),
      });

      const result = await apiClient.get('/malformed');

      expect(result.data).toBeNull();
      expect(result.error).toBe('Invalid JSON');
    });
  });

  describe('Custom Headers and Options', () => {
    it('should merge custom headers with default headers', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.get('/protected', {
        headers: { 'Authorization': 'Bearer token123' },
      });

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer token123',
          },
        })
      );
    });

    it('should allow overriding Content-Type header', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.post('/upload', {}, {
        headers: { 'Content-Type': 'multipart/form-data' },
      });

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          headers: expect.objectContaining({
            'Content-Type': 'multipart/form-data',
          }),
        })
      );
    });
  });

  describe('Complex Response Types', () => {
    it('should handle array response', async () => {
      const users = [{ id: 1, name: 'User 1' }, { id: 2, name: 'User 2' }];
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(users),
      });

      const result = await apiClient.get<typeof users>('/users');

      expect(result.data).toEqual(users);
      expect(result.data?.length).toBe(2);
    });

    it('should handle nested object response', async () => {
      const response = {
        pagination: { page: 1, total: 100 },
        items: [{ id: 1 }],
        meta: { timestamp: Date.now() },
      };
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(response),
      });

      const result = await apiClient.get<typeof response>('/offers');

      expect(result.data?.pagination).toEqual({ page: 1, total: 100 });
      expect(result.data?.items).toHaveLength(1);
    });

    it('should handle empty response', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(null),
      });

      const result = await apiClient.get('/empty');

      expect(result.data).toBeNull();
      expect(result.error).toBeNull();
    });
  });

  describe('ApiResponse Type', () => {
    it('should return correct type structure on success', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ value: 42 }),
      });

      const result: ApiResponse<{ value: number }> = await apiClient.get('/test');

      expect(result).toHaveProperty('data');
      expect(result).toHaveProperty('error');
      expect(result.data?.value).toBe(42);
    });

    it('should return correct type structure on error', async () => {
      mockFetch.mockResolvedValueOnce({
        ok: false,
        status: 500,
        text: () => Promise.resolve('Server error'),
      });

      const result: ApiResponse<unknown> = await apiClient.get('/error');

      expect(result.data).toBeNull();
      expect(result.error).not.toBeNull();
    });
  });

  describe('Request Body Serialization', () => {
    it('should serialize complex objects correctly', async () => {
      const complexBody = {
        user: { name: 'Test', age: 30 },
        filters: [1, 2, 3],
        settings: { enabled: true },
      };

      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.post('/complex', complexBody);

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          body: JSON.stringify(complexBody),
        })
      );
    });

    it('should handle arrays in request body', async () => {
      const arrayBody = { ids: [1, 2, 3, 4, 5] };

      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.post('/batch', arrayBody);

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          body: JSON.stringify(arrayBody),
        })
      );
    });

    it('should handle null values in body', async () => {
      const bodyWithNulls = { name: 'Test', optional: null };

      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.post('/nullable', bodyWithNulls);

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          body: JSON.stringify(bodyWithNulls),
        })
      );
    });

    it('should handle special characters in body', async () => {
      const bodyWithSpecialChars = { 
        message: 'Hello <script>alert("xss")</script>',
        polish: 'Żółć',
        emoji: '🎉',
      };

      mockFetch.mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({}),
      });

      await apiClient.post('/special', bodyWithSpecialChars);

      expect(mockFetch).toHaveBeenCalledWith(
        expect.any(String),
        expect.objectContaining({
          body: JSON.stringify(bodyWithSpecialChars),
        })
      );
    });
  });
});
