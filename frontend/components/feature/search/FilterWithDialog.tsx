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
    const [customFrom, customTo] = value ? value.split("&") : ["", ""];

    return (
        <>
            <div className="flex cursor-pointer items-center gap-2">
                <div
                    onClick={() => {
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
                            value={customFrom}
                            onChange={(e) =>
                                onChange(`${e.target.value}&${customTo}`)
                            }
                        />

                        <span className="text-center">-</span>

                        <Input
                            placeholder="6000"
                            value={customTo}
                            onChange={(e) =>
                                onChange(`${customFrom}&${e.target.value}`)
                            }
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
                                onChange(`${customFrom}&${customTo}`)
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
