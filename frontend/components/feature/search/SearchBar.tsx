"use client";
import { useTranslation } from "react-i18next";
import searchStyles from "../../../styles/SearchView.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import { Filter } from "./Filters";
import { leading_categories } from '@/store/data/Dictionaries.json';
import { toLabelValueFormat } from "@/utils/others/toLabelValueFormat";
import { search } from "@/types/search/search";
import { FloatingLabelInput } from "@/components/ui/floatingInput";

// Typ całego lokalnego searcha

// Props dla SearchBar
export type SearchBarProps = {
    value: search; // cały obiekt search
    onChange: (newValue: search) => void; // aktualizacja całego obiektu
}

// Typ funkcji handleChange
export type HandleChange = <K extends keyof search>(
    field: K,
    value: search[K]
) => void;

export function SearchBar({ value, onChange }: SearchBarProps) {
    const { t } = useTranslation("searchbar"); 
    const items = toLabelValueFormat(leading_categories);

    // Funkcja pomocnicza do aktualizacji lokalnego stanu
    const handleChange: HandleChange = (field, fieldValue) => {
        onChange({
            ...value,        // <-- value to cały obiekt Search z props
            [field]: fieldValue  // <-- fieldValue to nowa wartość dla pola
        });
    }

    return (
        <div className={searchStyles["search-section"]}>
            <div className={searchStyles["searchbar"]}>
                {/* <div className={searchStyles["phrase-search"]}> */}
                    <FloatingLabelInput
                        // className={`${searchStyles["city"]} px-3`}
                        value={value?.keyword || ""}
                        onChange={(e) => handleChange("keyword", e.target.value)}
                        label={t("searchKeywordPlaceholder")}
                    />
                {/* </div> */}

                <div className={`${searchStyles["major-study-search"]} ${buttonStyles["floating-input"]}`}>
                    <Filter
                        clearable={true}
                        className="w-full"
                        label={t("searchbarCombobox")}
                        variant="secondary"
                        items={items}
                        value={value?.category || undefined}
                        onChange={(e) => handleChange("category", e)}
                    />
                </div>

                <div className={searchStyles["city-search"]}>
                    <FloatingLabelInput
                        label={t("searchCityPlaceholder")}
                        value={value?.localization || ""}
                        onChange={(e) => handleChange("localization", e.target.value)}
                        // className={`${searchStyles["city"]} px-3`}
                    />
                </div>
            </div>
        </div>
    );
}
