export function omitUndefined<T extends Record<string, unknown>>(obj: T): Partial<T> {
  // Keep null and empty strings - those can be meaningful for the backend.
  return Object.fromEntries(
    Object.entries(obj).filter(([, value]) => value !== undefined)
  ) as Partial<T>;
}
