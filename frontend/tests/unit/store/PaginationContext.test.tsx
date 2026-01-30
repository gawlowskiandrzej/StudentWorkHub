import React from "react";
import { describe, expect, it } from "vitest";
import { renderHook, act } from "@testing-library/react";
import { PaginationProvider, usePagination } from "@/store/PaginationContext";

const wrapper: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <PaginationProvider>{children}</PaginationProvider>
);

describe("PaginationContext", () => {
  it("provides default offset 0 and limit 10", () => {
    const { result } = renderHook(() => usePagination(), { wrapper });

    expect(result.current.offset).toBe(0);
    expect(result.current.limit).toBe(10);
  });

  it("updates offset", () => {
    const { result } = renderHook(() => usePagination(), { wrapper });

    act(() => {
      result.current.setOffset(30);
    });

    expect(result.current.offset).toBe(30);
  });
});
