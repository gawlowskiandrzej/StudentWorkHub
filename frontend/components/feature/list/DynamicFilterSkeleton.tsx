import { Skeleton } from "@/components/ui/skeleton";
import dynamicFilterStyles from "../../../styles/DynamicFilter.module.css";

interface DynamicFilterSkeletonProps {
  itemsCount?: number;
}

export function DynamicFilterSkeleton({
}: DynamicFilterSkeletonProps) {
  return (
    <div
      className={`${dynamicFilterStyles["offer-list-filter-section"]} border border-muted animate-pulse`}
    >
      <div
        className={`${dynamicFilterStyles["filter-header"]} flex items-center py-1 gap-2`}
      >
        <Skeleton className="h-4 w-5 rounded" />
        <Skeleton className="h-4 w-32" />
        <Skeleton className="ml-auto h-4 w-4 rounded" />
      </div>
    </div>
  );
}
