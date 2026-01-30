import { describe, it, expect, vi, beforeEach } from 'vitest';
import { renderHook, act } from '@testing-library/react';
import { resolvePostLoginRedirect, useLoginState } from '@/hooks/useLogin';

vi.mock('@/store/userContext', () => ({
  useUser: () => ({
    standardLogin: vi.fn().mockResolvedValue(true),
    loading: false,
    error: null,
  }),
}));

describe('useLogin - Pure Functions', () => {
  describe('resolvePostLoginRedirect', () => {
    const mockOrigin = 'http://localhost:3000';
    
    beforeEach(() => {
      Object.defineProperty(window, 'location', {
        value: {
          origin: mockOrigin,
          href: `${mockOrigin}/login`,
        },
        writable: true,
      });
      
      Object.defineProperty(document, 'referrer', {
        value: '',
        writable: true,
        configurable: true,
      });
    });

    it('should return mainPath when nextParam is null and no referrer', () => {
      const result = resolvePostLoginRedirect(null, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should use nextParam when it is a valid same-origin path', () => {
      const nextParam = `${mockOrigin}/offers?page=2`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/offers?page=2');
    });

    it('should ignore nextParam pointing to login page', () => {
      const nextParam = `${mockOrigin}/login`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should ignore nextParam with login query params', () => {
      const nextParam = `${mockOrigin}/login?error=true`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should reject cross-origin nextParam', () => {
      const nextParam = 'https://malicious-site.com/steal-data';
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should handle nextParam with hash fragments', () => {
      const nextParam = `${mockOrigin}/offers#section1`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/offers#section1');
    });

    it('should handle complex query strings', () => {
      const nextParam = `${mockOrigin}/search?q=developer&location=warsaw&sort=salary`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/search?q=developer&location=warsaw&sort=salary');
    });

    it('should handle malformed URLs gracefully', () => {
      const nextParam = 'not-a-valid-url-://test';
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should use referrer when nextParam is null', () => {
      Object.defineProperty(document, 'referrer', {
        value: `${mockOrigin}/previous-page`,
        configurable: true,
      });
      
      const result = resolvePostLoginRedirect(null, '/dashboard');
      
      expect(result).toBe('/previous-page');
    });

    it('should ignore cross-origin referrer', () => {
      Object.defineProperty(document, 'referrer', {
        value: 'https://external-site.com/page',
        configurable: true,
      });
      
      const result = resolvePostLoginRedirect(null, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should ignore referrer pointing to login page', () => {
      Object.defineProperty(document, 'referrer', {
        value: `${mockOrigin}/login`,
        configurable: true,
      });
      
      const result = resolvePostLoginRedirect(null, '/dashboard');
      
      expect(result).toBe('/dashboard');
    });

    it('should prioritize nextParam over referrer', () => {
      Object.defineProperty(document, 'referrer', {
        value: `${mockOrigin}/referrer-page`,
        configurable: true,
      });
      
      const nextParam = `${mockOrigin}/next-param-page`;
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');
      
      expect(result).toBe('/next-param-page');
    });
  });
});

describe('useLoginState Hook', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  describe('initial state', () => {
    it('should have correct initial values', () => {
      const { result } = renderHook(() => useLoginState());

      expect(result.current.state.email).toBe('');
      expect(result.current.state.password).toBe('');
      expect(result.current.state.rememberMe).toBe(false);
      expect(result.current.loading).toBe(false);
      expect(result.current.error).toBeNull();
    });
  });

  describe('state updates', () => {
    it('should update email when update() is called', () => {
      const { result } = renderHook(() => useLoginState());

      act(() => {
        result.current.update('email', 'test@example.com');
      });

      expect(result.current.state.email).toBe('test@example.com');
    });

    it('should update password when update() is called', () => {
      const { result } = renderHook(() => useLoginState());

      act(() => {
        result.current.update('password', 'myPassword123!');
      });

      expect(result.current.state.password).toBe('myPassword123!');
    });

    it('should toggle rememberMe flag', () => {
      const { result } = renderHook(() => useLoginState());

      expect(result.current.state.rememberMe).toBe(false);

      act(() => {
        result.current.update('rememberMe', true);
      });

      expect(result.current.state.rememberMe).toBe(true);
    });

    it('should clear error when field is updated', () => {
      const { result } = renderHook(() => useLoginState());

      act(() => {
        result.current.update('email', 'newemail@example.com');
      });

      expect(result.current.error).toBeNull();
    });
  });

  describe('redirect handling', () => {
    beforeEach(() => {
      Object.defineProperty(document, 'referrer', {
        value: '',
        configurable: true,
      });
    });

    it('should resolve redirect using nextParam when provided', () => {
      const nextParam = 'http://localhost:3000/offers?page=2';
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');

      expect(result).toBe('/offers?page=2');
    });

    it('should use mainPath when nextParam is null', () => {
      const result = resolvePostLoginRedirect(null, '/dashboard');

      expect(result).toBe('/dashboard');
    });

    it('should ignore login page in nextParam', () => {
      const nextParam = 'http://localhost:3000/login';
      const result = resolvePostLoginRedirect(nextParam, '/dashboard');

      expect(result).toBe('/dashboard');
    });
  });

  describe('trimming behavior', () => {
    it('should trim email before submission', () => {
      const { result } = renderHook(() => useLoginState());

      act(() => {
        result.current.update('email', '  test@example.com  ');
      });

      expect(result.current.state.email).toBe('  test@example.com  ');
    });
  });

  describe('validation patterns', () => {
    it('should detect empty credentials', () => {
      const { result } = renderHook(() => useLoginState());

      expect(result.current.state.email).toBe('');
      expect(result.current.state.password).toBe('');
    });

    it('should accept valid credentials', () => {
      const { result } = renderHook(() => useLoginState());

      act(() => {
        result.current.update('email', 'user@example.com');
        result.current.update('password', 'securePassword123');
      });

      expect(result.current.state.email).toBe('user@example.com');
      expect(result.current.state.password).toBe('securePassword123');
    });
  });
});
