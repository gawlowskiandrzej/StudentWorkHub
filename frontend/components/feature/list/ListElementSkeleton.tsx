import { Skeleton } from "@/components/ui/skeleton";
import listStyles from "../../../styles/OfferList.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";

export function ListElementSkeleton() {
  return (
    <div
      className={`${listStyles["offer-list-header"]} ${buttonStyles["scale-hover"]} min-h-[110px] border border-muted animate-pulse`}
    >
      <div>
        <Skeleton className="w-[75px] h-[75px] rounded-md" />
      </div>

      <div className={listStyles["offer-header"]}>
        <div className={listStyles["offer-header-info"]}>
          <div className={listStyles["offer-header-content"]}>
            <div className={listStyles["offer-header2"]}>
              <Skeleton className="h-4 w-48 mb-2" />
              <Skeleton className="h-3 w-32" />
            </div>

            <div className={listStyles["offer-header-desc"]}>
              <div className="flex items-center gap-2">
                <Skeleton className="w-4 h-4 rounded" />
                <Skeleton className="h-3 w-24" />
              </div>
              <div className="flex items-center gap-2">
                <Skeleton className="w-4 h-4 rounded" />
                <Skeleton className="h-3 w-32" />
              </div>
              <div className="flex items-center gap-2">
                <Skeleton className="w-4 h-4 rounded" />
                <Skeleton className="h-3 w-28" />
              </div>
            </div>
          </div>

          <div className={listStyles["offer-header-salary-date"]}>
            <Skeleton className="h-4 w-40 mb-2" />
            <div className="flex items-center gap-3">
              <Skeleton className="h-4 w-20" />
              <Skeleton className="h-4 w-[100px]" />
              <Skeleton className="h-4 w-20" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}