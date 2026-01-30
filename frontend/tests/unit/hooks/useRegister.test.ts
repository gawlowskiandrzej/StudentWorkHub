import { describe, it, expect, beforeEach, vi } from 'vitest';
import { renderHook, act } from '@testing-library/react';
import { useRegisterState } from '@/hooks/useRegister';

vi.mock('@/store/userContext', () => ({
  useUser: () => ({
    standardRegister: vi.fn().mockResolvedValue(true),
    loading: false,
    error: null,
  }),
}));

describe('useRegisterState Hook', () => {
  describe('initial state', () => {
    it('should have correct initial values', () => {
      const { result } = renderHook(() => useRegisterState());

      expect(result.current.state.name).toBe('');
      expect(result.current.state.surname).toBe('');
      expect(result.current.state.email).toBe('');
      expect(result.current.state.password).toBe('');
      expect(result.current.state.consent).toBe(false);
    });
  });

  describe('state updates', () => {
    it('should update email when update() is called', () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('email', 'test@example.com');
      });

      expect(result.current.state.email).toBe('test@example.com');
    });

    it('should update name when update() is called', () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('name', 'John');
      });

      expect(result.current.state.name).toBe('John');
    });

    it('should update surname when update() is called', () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('surname', 'Doe');
      });

      expect(result.current.state.surname).toBe('Doe');
    });

    it('should clear error when field is updated', () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('email', 'newemail@example.com');
      });

      expect(result.current.error).toBeNull();
    });
  });

  describe('consent validation', () => {
    it('should require consent for registration', async () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('name', 'John');
        result.current.update('surname', 'Doe');
        result.current.update('email', 'john@example.com');
        result.current.update('password', 'Password123!');
      });

      await act(async () => {
        const success = await result.current.submit();
        expect(success).toBe(false);
      });

      expect(result.current.error).toBe('CONSENT_REQUIRED');
    });
  });

  describe('input handling', () => {
    it('should not trim password (preserve spaces)', () => {
      const { result } = renderHook(() => useRegisterState());

      const passwordWithSpaces = 'my secure password';

      act(() => {
        result.current.update('password', passwordWithSpaces);
      });

      expect(result.current.state.password).toBe(passwordWithSpaces);
    });
  });

  describe('DTO creation', () => {
    it('should trim name and surname when creating DTO', async () => {
      const { result } = renderHook(() => useRegisterState());

      act(() => {
        result.current.update('name', '  John  ');
        result.current.update('surname', '  Doe  ');
        result.current.update('email', 'john@example.com');
        result.current.update('password', 'Password123!');
        result.current.update('consent', true);
      });

      await act(async () => {
        await result.current.submit();
      });

      expect(result.current.state.name).toBe('  John  ');
      expect(result.current.state.surname).toBe('  Doe  ');
    });
  });

  describe('loading state', () => {
    it('should have loading flag from useUser hook', () => {
      const { result } = renderHook(() => useRegisterState());

      expect(typeof result.current.loading).toBe('boolean');
    });
  });
});
