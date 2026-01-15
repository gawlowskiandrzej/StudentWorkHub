"use client";
import { Checkbox } from "@/components/ui/checkbox";
import { StepProps } from "@/constants/profileCreation/steps";
import { useTranslation } from "react-i18next";
import styles from '@/styles/ProfileCreationStyle.module.css';
import { FloatingLabelInput } from "@/components/ui/floatingInput";

export default function StepLocation({ data, updateData }: StepProps) {

    const { t } = useTranslation("profileCreation");

    const WORK_TYPES = [
        { id: "office", label: t("office") },
        { id: "hybrid", label: t("hybrid") },
        { id: "remote", label: t("remote") },
    ];

    const handleTypeToggle = (id: string) => {
        const current = data.location.workplaceTypes || [];
        const next = current.includes(id)
            ? current.filter((t: string) => t !== id)
            : [...current, id];
        updateData("location", { ...data.location, workplaceTypes: next });
    };

    return (

        <div className="flex flex-col items-center space-y-8 fadeInUp">
            <div className="text-center space-y-6">
                <h2 className={styles.sectionTitle}>
                    {t("whereAreYouLocated")}
                </h2>
            </div>

            <div className={styles.inputWrapper}>
                <div className={styles.inputWrapper + " mx-auto max-w-md"}>

                    <label className={styles.inputLabel}>
                        {t("enterYourCity")}
                        <span className={styles.requiredStar}>*</span>
                    </label>
                    <FloatingLabelInput
                        value={data.location.city}
                        onChange={(e) => updateData("location", { ...data.location, city: e.target.value })}
                        label={t("cityPlaceholder")}
                        className={`mt-5`}
                    />
                </div>
            </div>

            <div className="flex flex-col items-center">
                <h3 className="text-white font-semibold text-white text-center font-medium mb-6">
                    {t("yourPreferredWorkType")}
                </h3>
                <div className={styles.selectionContainer}>
                    {WORK_TYPES.map((type) => (
                        <div key={type.id} className="flex items-center space-x-10">
                            <Checkbox
                                id={type.id}
                                checked={data.location?.workplaceTypes?.includes(type.id)}
                                onCheckedChange={() => handleTypeToggle(type.id)}
                                className="border-blue-500 data-[state=checked]:bg-blue-500"
                            />
                            <label htmlFor={type.id} className="text-white cursor-pointer text-sm">{type.label}</label>
                        </div>
                    ))}
                </div>
            </div>

        </div>
    );
}