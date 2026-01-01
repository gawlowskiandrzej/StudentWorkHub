import { FilterItem, FilterKey, FilterValue } from "./dynamicFilter";

export interface DynamicFilterProps {
  header: string;
  filterKey: FilterKey;
  items: FilterItem[];
  selected?: Set<FilterValue>;
  onChange: (key: FilterKey, value: FilterValue) => void;
}