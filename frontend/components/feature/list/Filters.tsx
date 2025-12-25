import { Checkbox } from "@/components/ui/checkbox";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { ChevronDownIcon } from "lucide-react";
import { useState } from "react";
import "../../../styles/DynamicFilter.css";
import { dynamicFilter } from "@/types/details/dynamicFilter";

export function DynamicFilter({header, items}: dynamicFilter) {
  const [open, setOpen] = useState(false);
  return (
    <Collapsible className="offer-list-filter-section transition-all duration-300" open={open} onOpenChange={setOpen}>
      <CollapsibleTrigger asChild>
        <div className="filter-header cursor-pointer">
          <img className="filter-icon" src="/icons/filter-icon0.svg" />
          <div className="filter-title">{header}</div>
          <ChevronDownIcon className={`transition-transform ${open ? "rotate-180" : ""}`} />
        </div>
      </CollapsibleTrigger>
      <CollapsibleContent className="checkboxes overflow-hidden transition-all data-[state=closed]:animate-collapsible-up data-[state=open]:animate-collapsible-down">
        {items.map((item, index) => (
          <label key={index} className="offer-list-filter-item cursor-pointer">
            <Checkbox className="frame-20" checked={item.checked} />
            <div className="option-long-name-name">{item.label}</div>
          </label>
        ))}
      </CollapsibleContent>
    </Collapsible>
  );
}