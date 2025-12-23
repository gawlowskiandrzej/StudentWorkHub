import { ProgressValues } from "@/types/details/ProgressValues";
import { Progress } from "@/components/ui/progress";

export default function ProgressBar({ current, max, date }: ProgressValues) {
    const formattedDate = new Intl.DateTimeFormat("pl-PL", {
        day: "2-digit",
        month: "2-digit",
    }).format(date);

    return (
        <div className="expire-progress">
            <div className="frame-50">
                <div className="frame-49">
                    <Progress value={current} max={max} />
                </div>
                <div className="expires-in-x-days">expires in {max - current} days</div>
            </div>
            <div className="to-dd-mm">(to {formattedDate})</div>
        </div>
    );
}
