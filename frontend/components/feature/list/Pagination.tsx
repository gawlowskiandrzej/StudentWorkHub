import { PaginationType } from "@/types/search/pagination";
import { ArrowLeft, ArrowRight } from "lucide-react";

export function Pagination({ offset, limit, count, onChange }: PaginationType) {
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
        <div className="offer-list-pagination">
            <button
                className={`cursor-pointer klasa scale-hover ${!hasPrev ? "hidden" : ""}`}
                onClick={handlePrev}
                aria-label="Previous page"
            >
                <ArrowLeft size={20}/>
            </button>

            
            <div className="offer-count">
                <input className="_100" type="number" maxLength={10} onChange={(e) => onChange(e.target.valueAsNumber)} value={currentPage || 1} />
            </div>
            <div className="from">from</div>
            <div className="_100">{totalPages}</div>

            <button
                className="cursor-pointer klasa scale-hover"
                onClick={handleNext}
                aria-label="Next page"
            >
                <ArrowRight size={20}/>
            </button>
        </div>
    );
}
