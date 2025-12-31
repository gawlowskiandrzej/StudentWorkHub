import { useEffect, useState } from "react";
import { FilterProps } from '@/components/feature/search/Filters'
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";

import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { ChevronDownIcon } from "lucide-react";


export function FilterWithDialog({
    label = "Options",
    value = "",
    onChange,
}: FilterProps) {
    const [openDialog, setOpenDialog] = useState(false);
    const [localFrom, setLocalFrom] = useState("");
    const [localTo, setLocalTo] = useState("");
    const [customFrom, customTo] = value ? value.split("&") : ["", ""];

    return (
        <>
            <div className="flex cursor-pointer items-center gap-2">
                <div
                    onClick={() => {
                         const [customFrom, customTo] = value ? value.split("&") : ["", ""];
                        setLocalFrom(customFrom);
                        setLocalTo(customTo);
                        setOpenDialog(true);
                    }}>
                    {(customFrom || customTo) ? `${customFrom} - ${customTo} zł` : label}
                </div>
                <ChevronDownIcon className={`size-4 opacity-50`} />
            </div>

            <Dialog open={openDialog} onOpenChange={setOpenDialog}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Wpisz wartość miesięcznej wypłaty</DialogTitle>
                    </DialogHeader>
                    <div className="grid grid-cols-[1fr_auto_1fr_auto] gap-2 w-full items-center">
                        <Input
                            placeholder="3500"
                            value={localFrom}
                            onChange={(e) => setLocalFrom(e.target.value)}
                        />

                        <span className="text-center">-</span>

                        <Input
                            placeholder="6000"
                            value={localTo}
                            onChange={(e) => setLocalTo(e.target.value)}
                        />
                        <span className="text-center">zł</span>
                    </div>
                    <DialogFooter>
                        <Button className="cursor-pointer" variant="outline"
                            onClick={() => onChange("")}
                        >
                            Wyczyść
                        </Button>
                        <Button className="cursor-pointer"
                            onClick={() => {
                                onChange(`${localFrom}&${localTo}`)
                                setOpenDialog(false);
                            }}
                        >
                            Zastosuj
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </>
    );
}
