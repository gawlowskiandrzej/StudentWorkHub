"use client";

import { User, Wallet, Briefcase, Globe, MapPin, Star, Laptop } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import { StepProps } from "@/constants/profileCreation/steps";
import { useTranslation } from "react-i18next";
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";

export default function StepReview({ data }: StepProps) {

  const { t } = useTranslation("profileCreation");
  const { fullDictionaries } = useProfileCreationDictionaries();

  const getJobStatusLabel = (status: string) => {
    const labels: Record<string, string> = {
      "actively_looking": t("lookingForJobUrgently"),
      "open_to_offers": t("openToOffers"),
      "not_looking": t("notLookingForJobNow")
    };
    return labels[status] || status;
  };

  const ReviewCard = ({ icon: Icon, title, children, className = "" }: any) => (
    <div className={`p-5 rounded-xl border border-zinc-800 bg-zinc-900/40 hover:border-blue-500/50 transition-all duration-300 ${className}`}>
      <div className="flex items-center gap-3 mb-3">
        <div className="p-2 rounded-lg bg-blue-500/10">
          <Icon size={18} className="text-blue-400" />
        </div>
        <span className="text-[10px] font-bold uppercase tracking-wider text-blue-400">{title}</span>
      </div>
      {children}
    </div>
  );

  return (
    <div className="flex flex-col items-center w-full max-w-4xl mx-auto space-y-8 animate-in fade-in zoom-in duration-700">
      <div className="text-center space-y-2">
        <h2 className="text-3xl font-bold text-white">{t("reviewYourProfile")}</h2>
        <p className="text-zinc-400 text-sm">{t("reviewProfileDescription")}</p>
      </div>

      <div className="w-full px-4 grid grid-cols-1 md:grid-cols-6 gap-4">
        
        {/* Major & Job Status */}
        <ReviewCard icon={Briefcase} title={t("major")} className="md:col-span-4">
          <p className="text-white font-semibold text-lg capitalize">
            {data.major && fullDictionaries
              ? fullDictionaries.leading_categories.find(c => c.id === data.major)?.name || t("notSpecified")
              : t("notSpecified")}
          </p>
        </ReviewCard>

        <ReviewCard icon={Star} title={t("jobStatus")} className="md:col-span-2">
          <p className="text-white text-sm italic">{getJobStatusLabel(data.jobStatus)}</p>
        </ReviewCard>

        {/* Location & Salary */}
        <ReviewCard icon={MapPin} title={t("whereAreYouLocated")} className="md:col-span-3">
          <p className="text-white font-semibold mb-3">{data.location.city || t("notSpecified")}</p>
          <div className="flex flex-wrap gap-2">
            {data.location.workplaceTypes.length > 0 ? (
              data.location.workplaceTypes.map((t: string) => (
                <Badge key={t} variant="secondary" className="bg-blue-600/20 text-blue-400 border-blue-500/30 text-[10px] uppercase">{t}</Badge>
              ))
            ) : <span className="text-zinc-500 text-xs">{t("noPreferences")}</span>}
          </div>
        </ReviewCard>

        <ReviewCard icon={Wallet} title={t("salary")} className="md:col-span-3">
          <div className="flex items-baseline gap-1">
            <span className="text-white font-bold text-xl">{data.earnings.min || "0"}</span>
            <span className="text-zinc-500">—</span>
            <span className="text-white font-bold text-xl">{data.earnings.max || "0"}</span>
            <span className="text-blue-400 font-medium ml-2 text-sm uppercase">
               zł / {data.earnings.isMonthly ? t("monthly") : t("hourly")}
            </span>
          </div>
        </ReviewCard>

        {/* Contracts & Languages */}
        <ReviewCard icon={Laptop} title={t("contractType")} className="md:col-span-3">
          <div className="flex flex-wrap gap-2">
            {data.contracts.length > 0 && fullDictionaries ? (
              data.contracts.map((contractId: number) => {
                const contractName = fullDictionaries.employment_types.find(e => e.id === contractId)?.name;
                return contractName ? (
                  <Badge key={contractId} variant="outline" className="border-zinc-700 text-zinc-300 text-[10px]">{contractName}</Badge>
                ) : null;
              })
            ) : <span className="text-zinc-500 text-xs">{t("noSelection")}</span>}
          </div>
        </ReviewCard>

        <ReviewCard icon={Globe} title={t("whatLanguagesDoYouSpeak")} className="md:col-span-3">
          <div className="flex flex-wrap gap-2">
            {data.languages.native && fullDictionaries && (
              <Badge className="bg-blue-600 text-white text-[10px]">
                {t("nativeLanguage")}: {fullDictionaries.languages.find(l => l.id === data.languages.native)?.name || ""}
              </Badge>
            )}
            {data.languages.others.length > 0 && fullDictionaries && (
              data.languages.others.map((l: any) => {
                const langName = fullDictionaries.languages.find(lang => lang.id === l.languageId)?.name;
                const levelName = fullDictionaries.language_levels.find(lv => lv.id === l.levelId)?.name;
                return langName && levelName ? (
                  <Badge key={l.id} variant="secondary" className="bg-zinc-800 text-zinc-300 text-[10px] border-zinc-700">{langName} ({levelName})</Badge>
                ) : null;
              })
            )}
          </div>
        </ReviewCard>

        {/* Skills */}
        {data.skills && data.skills.length > 0 && (
          <ReviewCard icon={User} title={t("skills")} className="md:col-span-6">
            <div className="flex flex-wrap gap-3">
              {data.skills.map((s: any) => (
                <div key={s.id} className="flex items-center bg-zinc-900/80 border border-zinc-800 rounded-lg px-3 py-1.5">
                  <span className="text-blue-400 font-semibold text-sm mr-2">{s.name}</span>
                  <span className="text-zinc-500 text-xs">{s.months} {t("monthsShort")}</span>
                </div>
              ))}
            </div>
          </ReviewCard>
        )}

      </div>
    </div>
  );
}