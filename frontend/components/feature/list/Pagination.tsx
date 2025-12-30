import { PaginationType } from "@/types/search/pagination";
import { ArrowLeft, ArrowRight } from "lucide-react";
import listStyles from '../../../styles/OfferList.module.css'
import buttonStyles from '../../../styles/ButtonStyle.module.css'
import { useTranslation } from "react-i18next";

export function Pagination({ offset, limit, count, onChange }: PaginationType) {
    const {t} = useTranslation("list");
    const currentPage = Math.floor(offset / limit) + 1;
    const totalPages = Math.ceil(count / limit);

    const hasPrev = offset > 0;
    const hasNext = offset + limit < count;

    const handlePrev = () => {
        if (!hasPrev) return;
        onChange(Math.max(offset - limit, 0));
    };

    const handleNext = () => {
        if (!hasNext) return;
        onChange(offset + limit);
    };

    return (
        <div className={listStyles["offer-list-pagination"]}>
            <button
                className={`${buttonStyles["scale-hover"]} ${!hasPrev ? "hidden" : ""}`}
                onClick={handlePrev}
                aria-label="Previous page"
            >
                <ArrowLeft size={20}/>
            </button>

            
            <div className={listStyles["offer-count"]}>
                <input className={listStyles["_100"]} type="number" maxLength={10} onChange={(e) => onChange(e.target.valueAsNumber)} value={currentPage || 1} />
            </div>
            <div className={listStyles["from"]}>{t("from")}</div>
            <div className={listStyles["_100"]}>{totalPages}</div>

            <button
                className={buttonStyles["scale-hover"]}
                onClick={handleNext}
                aria-label="Next page"
            >
                <ArrowRight size={20}/>
            </button>
        </div>
    );
}
