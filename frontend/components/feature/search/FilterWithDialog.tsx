import { useState } from "react";
import { FilterProps } from "@/components/feature/search/Filters";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";

import { Button } from "@/components/ui/button";
import { ChevronDownIcon } from "lucide-react";
import { ExtraFiltersState } from "@/store/SearchContext";
import { Input } from "@/components/ui/Input";

export type salaryFilter = {
    label: string;
    value: string[];
    onChange: (key: keyof ExtraFiltersState, value?: string) => void,
}

export function FilterWithDialog({
    label = "Options",
    value = ["", ""],
    onChange
}: salaryFilter) {
    const [openDialog, setOpenDialog] = useState(false);
    const [localFrom, setLocalFrom] = useState("");
    const [localTo, setLocalTo] = useState("");

    const [customFrom, customTo] = value;

    const open = () => {
        setLocalFrom(customFrom ?? "");
        setLocalTo(customTo ?? "");
        setOpenDialog(true);
    };

    const clear = () => {
        setLocalFrom("");
        setLocalTo("");
        onChange("salaryFrom", "");
        onChange("salaryTo", "");
        setOpenDialog(false);
    };

    const apply = () => {
        onChange("salaryFrom", localFrom);
        onChange("salaryTo", localTo);
        setOpenDialog(false);
    };

    return (
        <>
            <div
                className="flex cursor-pointer items-center gap-2"
                onClick={open}
            >
                <div>
                    {(customFrom || customTo)
                        ? `${customFrom} - ${customTo} zł`
                        : label}
                </div>
                <ChevronDownIcon className="size-4 opacity-50" />
            </div>

            <Dialog open={openDialog} onOpenChange={setOpenDialog}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>
                            Wpisz wartość miesięcznej wypłaty
                        </DialogTitle>
                    </DialogHeader>

                    <div className="grid grid-cols-[1fr_auto_1fr_auto] gap-2 items-center">
                        <Input
                            placeholder="3500"
                            value={localFrom}
                            onChange={(e) => setLocalFrom(e.target.value)}
                        />
                        <span>-</span>
                        <Input
                            placeholder="6000"
                            value={localTo}
                            onChange={(e) => setLocalTo(e.target.value)}
                        />
                        <span>zł</span>
                    </div>

                    <DialogFooter>
                        <Button variant="outline" onClick={clear}>
                            Wyczyść
                        </Button>
                        <Button onClick={apply}>
                            Zastosuj
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </>
    );
}
