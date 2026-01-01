"use client";

import { Button } from "@/components/ui/button";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectGroup,
  SelectLabel,
  SelectItem,
} from "@/components/ui/select";
import { X } from "lucide-react";

type FilterItem = {
  label: string;
  value: string;
};

export interface FilterProps {
  label?: string;
  className?: string;
  variant?: "primary" | "secondary";
  clearable?: boolean;
  items?: FilterItem[];
  value?: string | undefined;
  onChange: (value: string) => void;
}

export function Filter({ label = "Options", className, variant, clearable = false, items, value, onChange }: FilterProps) {
  // const itemsWithEmpty: FilterItem[] = [
  //   ...items,
  //   ...(clearable ? [{ label: "Wyczyść", value: "__clear__" }] : []),
  // ];

  return (
    <Select value={value} onValueChange={(val) =>
      onChange(val === "__clear__" ? "" : val)
    }>
      <SelectTrigger color={variant} className={`w-auto cursor-pointer border-0 m-0 ${className}`}>
          <SelectValue placeholder={`${label}`} />
      </SelectTrigger>
       {(value !="" && value != undefined && clearable)? <X className="text-red-500 p-0 mr-2 cursor-pointer" size={16} onClick={() => onChange("")} /> : ""}

      <SelectContent>
        <SelectGroup>
          <SelectLabel>{label}</SelectLabel>

          {items?.map((item) => (
            <SelectItem key={item.value} value={item.value}>
              {item.label}
            </SelectItem>
          ))}

        </SelectGroup>
      </SelectContent>
    </Select>
  );
}
