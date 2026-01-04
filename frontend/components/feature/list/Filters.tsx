import { Checkbox } from "@/components/ui/checkbox";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { ChevronDownIcon } from "lucide-react";
import { useState } from "react";
import dynamicFilterStyles from  "../../../styles/DynamicFilter.module.css";
import { DynamicFilterProps } from "@/types/details/dynamicFilterProps";

export function DynamicFilter({
  header,
  filterKey,
  items,
  selected,
  onChange,
}: DynamicFilterProps) {
  const [open, setOpen] = useState(false);
  return (
    <Collapsible className={`${dynamicFilterStyles["offer-list-filter-section"]} transition-all duration-300`} open={open} onOpenChange={setOpen}>
      <CollapsibleTrigger asChild>
        <div className={`${dynamicFilterStyles["filter-header"]} cursor-pointer`}>
          <img className={dynamicFilterStyles["filter-icon"]} src="/icons/filter-icon0.svg" />
          <div className={dynamicFilterStyles["filter-title"]}>{header}</div>
          <ChevronDownIcon className={`transition-transform ${open ? "rotate-180" : ""}`} />
        </div>
      </CollapsibleTrigger>
      <CollapsibleContent className={`${dynamicFilterStyles["checkboxes"]} overflow-hidden transition-all data-[state=closed]:animate-collapsible-up data-[state=open]:animate-collapsible-down`}>
        {items?.map((item, index) => (
          <label key={index} className={`${dynamicFilterStyles["offer-list-filter-item"]} cursor-pointer`}>
            <Checkbox className={dynamicFilterStyles["frame-20"]}
             checked={selected?.has(item.value) ?? false}
              onCheckedChange={() =>
                onChange(filterKey, item.value)
              }
            />
            <div className={dynamicFilterStyles["option-long-name-name"]}>{item.label}</div>
          </label>
        ))}
      </CollapsibleContent>
    </Collapsible>
  );
}