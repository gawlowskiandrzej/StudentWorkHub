"use client";
import React, { createContext, useContext, useState } from "react";

type PaginationContextType = {
  offset: number;
  limit: number;
  setOffset: (offset: number) => void;
};

const PaginationContext = createContext<PaginationContextType | null>(null);

export const PaginationProvider = ({ children }: { children: React.ReactNode }) => {
  const [offset, setOffset] = useState(0);
  const limit = 10;

  return (
    <PaginationContext.Provider value={{ offset, limit, setOffset }}>
      {children}
    </PaginationContext.Provider>
  );
};

export const usePagination = () => {
  const ctx = useContext(PaginationContext);
  if (!ctx) throw new Error("usePagination must be used inside PaginationProvider");
  return ctx;
};