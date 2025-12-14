import { useState } from "react";
import { FilterProps } from '@/components/feature/search/Filters'
import {
    Select,
    SelectTrigger,
    SelectValue,
    SelectContent,
    SelectItem,
} from "@/components/ui/select";

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
    value,
}: FilterProps) {
    const [openDialog, setOpenDialog] = useState(false);
    const [customFrom, setCustomFrom] = useState("");
    const [customTo, setCustomTo] = useState("");

    return (
        <>
            <div className="flex items-center gap-2">
                <button
                    value={value}
                    onClick={() => {
                        setOpenDialog(true);
                    }}
                >{label}
                </button>
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
