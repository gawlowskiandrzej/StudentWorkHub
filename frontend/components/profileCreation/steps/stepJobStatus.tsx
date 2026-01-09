"use client";

import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { Label } from "@/components/ui/label";
import { StepProps } from "@/constants/profileCreation/steps";
import { useTranslation } from "react-i18next";
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";

const JOB_STATUS_MAP: { [key: string]: string } = {
  "urgent": "actively_looking",
  "open": "open_to_offers", 
  "not-looking": "not_looking"
};

export default function StepJobStatus({ data, updateData }: StepProps) {
  const { t } = useTranslation("profileCreation");
  const { fullDictionaries, loading, error } = useProfileCreationDictionaries();

  const jobStatusOptions = [
    { id: "urgent", labelKey: t("lookingForJobUrgently"), backendValue: JOB_STATUS_MAP["urgent"] },
    { id: "open", labelKey: t("openToOffers"), backendValue: JOB_STATUS_MAP["open"] },
    { id: "not-looking", labelKey: t("notLookingForJobNow"), backendValue: JOB_STATUS_MAP["not-looking"], descriptionKey: t("notLookingForJobNowDescription") }
  ];

  if (loading || !fullDictionaries) {
    return (
      <div className="flex flex-col items-center w-full max-w-2xl mx-auto space-y-12">
        <div className="text-center space-y-4">
          <h2 className="text-2xl font-semibold text-white">
            {t("jobStatusQuestion")}
          </h2>
        </div>
        <p className="text-zinc-400">{t("loading") || "Loading..."}</p>
      </div>
    );
  }

  return (
    <div className="flex flex-col items-center w-full max-w-2xl mx-auto space-y-12">
      <div className="text-center space-y-4">
        <h2 className="text-2xl font-semibold text-white">
          {t("jobStatusQuestion")}
        </h2>
        <p className="text-zinc-400">{t("jobStatusDescription")}</p>
      </div>

      <RadioGroup 
        value={data.jobStatus}
        onValueChange={(val) => updateData("jobStatus", val)}
        className="w-full max-w-md space-y-5"
      >
        {jobStatusOptions.map((option) => (
          <div key={option.id} className="flex items-center space-x-4 p-4 rounded-lg border border-zinc-900 hover:border-blue-500/50 transition-colors cursor-pointer">
            <RadioGroupItem value={option.backendValue} id={option.id} className="border-blue-500 text-blue-500" />
            <div className="flex-1">
              <Label htmlFor={option.id} className="text-white cursor-pointer block">
                {option.labelKey}
              </Label>
              {option.descriptionKey && (
                <p className="text-xs text-zinc-500">{option.descriptionKey}</p>
              )}
            </div>
          </div>
        ))}
      </RadioGroup>
    </div>
  );
}