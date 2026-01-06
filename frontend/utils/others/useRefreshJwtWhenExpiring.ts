import { useEffect } from "react";
import { useUser } from "@/store/UserContext";

/**
 * Refresh JWT automatically when it is close to expiring.
 * Use in a top-level component (layout/app shell).
 */
export function useRefreshJwtWhenExpiring(): void {
  const { isJwtExpiringSoon, maybeRefreshJwt, isAuthenticated } = useUser();

  useEffect(() => {
    if (!isAuthenticated) return;
    if (!isJwtExpiringSoon) return;

    void maybeRefreshJwt();
  }, [isAuthenticated, isJwtExpiringSoon, maybeRefreshJwt]);
}
