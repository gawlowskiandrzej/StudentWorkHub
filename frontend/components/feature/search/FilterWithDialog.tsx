import { useState } from "react";
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
}: FilterProps) {
    const [openDialog, setOpenDialog] = useState(false);
    const [customFrom, setCustomFrom] = useState("");
    const [customTo, setCustomTo] = useState("");

    return (
        <>
            <div className="flex cursor-pointer items-center gap-2">
                <div
                    onClick={() => {
                        setOpenDialog(true);
                    }}
                >{label}
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
                            value={customFrom}
                            onChange={(e) => setCustomFrom(e.target.value)}
                        />

                        <span className="text-center">-</span>

                        <Input
                            placeholder="6000"
                            value={customTo}
                            onChange={(e) => setCustomTo(e.target.value)}
                        />
                        <span className="text-center">zł</span>
                    </div>
                    <DialogFooter>
                        <Button
                            onClick={() => {
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
