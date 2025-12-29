"use client";

import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectGroup,
  SelectLabel,
  SelectItem,
} from "@/components/ui/select";

type FilterItem = {
  label: string;
  value: string;
};

export interface FilterProps {
  label?: string;
  className?: string;
  variant?: "primary" | "secondary";
  clearable?: boolean;
  items: FilterItem[];
  value?: string | undefined;
  onChange: (value: string) => void;
}

export function Filter({ label = "Options",className,variant,clearable = true, items,value, onChange }: FilterProps) {
  const itemsWithEmpty: FilterItem[] = [
    ...items,
    ...(clearable ? [{ label: "Wyczyść", value: "__clear__" }] : []),
  ];

  return (
    <Select value={value} onValueChange={(val) =>
        onChange(val === "__clear__" ? "" : val)
      }>
      <SelectTrigger color={variant} className={`w-auto cursor-pointer border-0 m-0 ${className}`}>
        <SelectValue placeholder={`${label}`} />
      </SelectTrigger>

      <SelectContent>
        <SelectGroup>
          <SelectLabel>{label}</SelectLabel>

          {itemsWithEmpty.map((item) => (
            <SelectItem key={item.value} value={item.value}>
              {item.label}
            </SelectItem>
          ))}

        </SelectGroup>
      </SelectContent>
    </Select>
  );
}
