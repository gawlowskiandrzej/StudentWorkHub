import { Checkbox } from "@/components/ui/checkbox";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { ChevronDownIcon } from "lucide-react";
import { useState } from "react";
import "../../../styles/DynamicFilter.css";

export function DynamicFilter() {
  const [open, setOpen] = useState(false);
  return (
    <Collapsible className="offer-list-filter-section transition-all duration-300" open={open} onOpenChange={setOpen}>
      <CollapsibleTrigger asChild>
        <div className="filter-header cursor-pointer">
          <img className="filter-icon" src="/icons/filter-icon0.svg" />
          <div className="filter-title">FilterTitle</div>
          <ChevronDownIcon className={`transition-transform ${open ? "rotate-180" : ""}`} />
        </div>
      </CollapsibleTrigger>
      <CollapsibleContent className="checkboxes overflow-hidden transition-all data-[state=closed]:animate-collapsible-up data-[state=open]:animate-collapsible-down">
        <label className="offer-list-filter-item cursor-pointer">
          <Checkbox className="frame-20"></Checkbox>
          <div className="option-long-name-name">OptionLongNameName</div>
        </label>
        <label className="offer-list-filter-item cursor-pointer">
          <Checkbox className="frame-20"></Checkbox>
          <div className="option-long-name-name">OptionLongNameName</div>
      </label>
      </CollapsibleContent>
    </Collapsible>
  );
}