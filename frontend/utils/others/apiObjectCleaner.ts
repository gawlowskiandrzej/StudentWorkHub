type CleanableValue =
  | string
  | number
  | boolean
  | null
  | undefined
  | string[]
  | number[]
  | Set<unknown>;

export function cleanObject<T extends Record<string, CleanableValue>>(
  obj: T
): Partial<T> {
  return Object.fromEntries(
    Object.entries(obj).filter(([_, value]) => {
      if (value === "" || value === null || value === undefined) return false;

      if (Array.isArray(value) && value.length === 0) return false;
      if (value instanceof Set && value.size === 0) return false;

      return true;
    })
  ) as Partial<T>;
}