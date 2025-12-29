import { ProgressValues } from "@/types/details/ProgressValues";
import { Progress } from "@/components/ui/progress";
import detailsStyles from '../../../styles/OfferDetails.module.css'

function daysUntil(date: Date) {
    const now = new Date();
    const diff = date.getTime() - now.getTime();
    return Math.max(0, Math.ceil(diff / (1000 * 60 * 60 * 24)));
}

export default function ProgressBar({ progress, expiresAt }: ProgressValues) {
    const formattedDate = new Intl.DateTimeFormat("pl-PL", {
        day: "2-digit",
        month: "2-digit",
    }).format(expiresAt);

    const daysLeft = daysUntil(expiresAt);

    return (
        <div className={detailsStyles["expire-progress"]}>
            <div className={detailsStyles["frame-50"]}>
                <div className={detailsStyles["frame-49"]}>
                    <Progress value={progress} max={100} />
                </div>
                <div className={detailsStyles["expires-in-x-days"]}>
                    expires in {daysLeft} days
                </div>
            </div>
            <div className={detailsStyles["to-dd-mm"]}>(to {formattedDate})</div>
        </div>
    );
}
