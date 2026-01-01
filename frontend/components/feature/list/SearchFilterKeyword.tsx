import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible";
import { ChevronDownIcon } from "lucide-react";
import { useState } from "react";
import dynamicFilterStyles from  "../../../styles/DynamicFilter.module.css";
import { searchFilterKeywords } from "@/types/list/searchFilterKeywords";

type searchKeywordProps = {
    header: string;
    value: string;
    filterKey: keyof searchFilterKeywords;
    onChange: (key: keyof searchFilterKeywords, value?: string) => void;
}
export function SearchFilterKeyword({
  header,
  value,
  filterKey,
  onChange,
}: searchKeywordProps) {
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
        <input
            type="text"
            placeholder="Wpisz słowo kluczowe"
            value={value}
            onChange={(e) =>
              onChange(filterKey, e.target.value)
            }
          />
      </CollapsibleContent>
    </Collapsible>
  );
}