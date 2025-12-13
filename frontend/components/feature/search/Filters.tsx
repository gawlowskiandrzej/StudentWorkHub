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

interface FilterProps {
  label?: string;
  className?: string;
  variant?: "primary" | "secondary";
  items: FilterItem[];
  value: string | undefined;
  onChange: (value: string) => void;
}

export function Filter({ label = "Options",className,variant, items,value, onChange }: FilterProps) {
  return (
    <Select value={value} onValueChange={onChange}>
      <SelectTrigger color={variant} className={`w-auto border-0 m-0 ${className}`}>
        <SelectValue placeholder={`Select ${label.toLowerCase()}`} />
      </SelectTrigger>

      <SelectContent>
        <SelectGroup>
          <SelectLabel>{label}</SelectLabel>

          {items.map((item) => (
            <SelectItem key={item.value} value={item.value}>
              {item.label}
            </SelectItem>
          ))}

        </SelectGroup>
      </SelectContent>
    </Select>
  );
}
