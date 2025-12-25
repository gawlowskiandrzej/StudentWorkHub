import { dynamicFilter } from "@/types/details/dynamicFilter";

export function mapToDynamicFilter(
  header: string,
  items: string[] | number[]
): dynamicFilter {
  return {
    header,
    items: items.map(label => ({
      label,
      checked: false,
    })),
  };
}