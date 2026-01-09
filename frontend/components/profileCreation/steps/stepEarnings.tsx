"use client";
import { Switch } from "@/components/ui/switch";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { StepProps } from "@/constants/profileCreation/steps";
import { useTranslation } from "react-i18next";
import styles from '@/styles/ProfileCreationStyle.module.css';
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";

export default function StepEarnings({ data, updateData }: StepProps) {

  const { t } = useTranslation("profileCreation");
  const { fullDictionaries, loading, error } = useProfileCreationDictionaries();

  const toggleContract = (contractId: number) => {
    const current = data.contracts || [];
    const next = current.includes(contractId) 
      ? current.filter((c: number) => c !== contractId) 
      : [...current, contractId];
    updateData("contracts", next);
  };

  if (loading || !fullDictionaries) {
    return (
      <div className="flex flex-col items-center space-y-8 fadeInUp">
        <div className="text-center space-y-6">
          <h2 className="text-2xl font-semibold text-white">
            {t("howMuchDoYouWantToEarn")}
          </h2>
        </div>
        <p className="text-zinc-400">{t("loading") || "Loading..."}</p>
      </div>
    );
  }

  return (
    <div className="flex flex-col items-center space-y-8 fadeInUp">
      <div className="text-center space-y-6">
        <h2 className="text-2xl font-semibold text-white">
          {t("howMuchDoYouWantToEarn")}
        </h2>
        
        <div className="flex items-center justify-center gap-4">
          <span className="text-sm text-zinc-400">{t("hourly")}</span>
          <Switch 
            checked={data.earnings.isMonthly} 
            onCheckedChange={(val) => updateData("earnings", { ...data.earnings, isMonthly: val })}
          />
          <span className="text-sm text-zinc-400">{t("monthly")}</span>
        </div>

        <div className="flex items-center justify-center gap-4 px-4">
          <Input 
            className={`${styles.earningInput}`}
            value={data.earnings.min}
            onChange={(e) => updateData("earnings", { ...data.earnings, min: parseInt(e.target.value) || 0 })}
            placeholder="Min"
            type="number"
          />
          <span className="text-white">—</span>
          <Input 
            className={`${styles.earningInput}`}
            value={data.earnings.max}
            onChange={(e) => updateData("earnings", { ...data.earnings, max: parseInt(e.target.value) || 0 })}
            placeholder="Max"
            type="number"
          />
          <span className="text-zinc-400 text-sm">{t("currency")}</span>
        </div>
      </div>

      <div className="flex flex-col items-center">
        <h3 className="text-white font-semibold text-white text-center font-medium mb-6">
          {t("contractType")}
        </h3>
        <div className={styles.selectionContainer}>
          {fullDictionaries.employment_types.map((type) => (
            <div key={type.id} className="flex items-center space-x-10">
              <Checkbox 
                id={`contract-${type.id}`}
                checked={data.contracts?.includes(type.id) || false}
                onCheckedChange={() => toggleContract(type.id)}
                className="border-blue-500 data-[state=checked]:bg-blue-500"
              />
              <label htmlFor={`contract-${type.id}`} className="text-white cursor-pointer text-sm">{type.name}</label>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}