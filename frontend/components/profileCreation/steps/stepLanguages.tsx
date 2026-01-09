"use client";

import { Plus, X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { StepProps } from "@/constants/profileCreation/steps";
import styles from '@/styles/ProfileCreationStyle.module.css';
import { useTranslation } from "react-i18next";
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";

export default function StepLanguages({ data, updateData }: StepProps) {

  const { t } = useTranslation("profileCreation");
  const { fullDictionaries, loading, error } = useProfileCreationDictionaries();

  const updateOtherLanguages = (newList: any[]) => {
    updateData("languages", { ...data.languages, others: newList });
  };

  const addLanguage = () => {
    const newList = [...data.languages.others, { id: crypto.randomUUID(), languageId: null, levelId: null }];
    updateOtherLanguages(newList);
  };

  const removeLanguage = (id: string) => {
    const newList = data.languages.others.filter((lang: any) => lang.id !== id);
    updateOtherLanguages(newList);
  };

  const updateLanguageItem = (id: string, field: string, value: any) => {
    const newList = data.languages.others.map((lang: any) =>
      lang.id === id ? { ...lang, [field]: value } : lang
    );
    updateOtherLanguages(newList);
  };

  if (loading || !fullDictionaries) {
    return (
      <div className="flex flex-col items-center space-y-8 fadeInUp">
        <div className="text-center space-y-4">
          <h2 className={styles.sectionTitle}>
            {t("whatLanguagesDoYouSpeak")}
          </h2>
        </div>
        <div className={styles.inputWrapper}>
          <p className="text-zinc-400">{t("loading") || "Loading..."}</p>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col items-center space-y-8 fadeInUp">
      <div className="text-center space-y-4">
        <h2 className={styles.sectionTitle}>
          {t("whatLanguagesDoYouSpeak")}
        </h2>
      </div>

      <div className={styles.inputWrapper}>
        <div className={styles.inputWrapper + " mx-auto max-w-md"}>
          <label className={styles.inputLabel}>
            {t("nativeLanguage")} <span className={styles.requiredStar}>*</span>
          </label>
          <Select
            value={data.languages.native ? String(data.languages.native) : ""}
            onValueChange={(val) => updateData("languages", { ...data.languages, native: Number(val) })}
          >
            <SelectTrigger className={styles.unifiedInput}>
              <SelectValue placeholder={t("Language")} />
            </SelectTrigger>

            <SelectContent className={styles.unifiedSelectContent}>
              {fullDictionaries.languages.map((language) => (
                <SelectItem key={language.id} value={String(language.id)} className={styles.unifiedSelectItem}>
                  {language.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>
      </div>

      <div className="w-full text-center space-y-6">
        <div>
          <h3 className="text-2xl font-semibold text-white mb-2">{t("otherLanguages")}</h3>
          <p className="text-zinc-400 text-sm">{t("whatOtherLanguagesCanYouUse")}</p>
        </div>

        <div className="space-y-3 max-w-md mx-auto px-4">
          {data.languages.others.map((lang: any) => (
            <div key={lang.id} className="flex items-center justify-center gap-2">
              <Select 
                value={lang.languageId ? String(lang.languageId) : ""} 
                onValueChange={(val) => updateLanguageItem(lang.id, "languageId", Number(val))}
              >
                <SelectTrigger className={styles.unifiedInput}>
                  <SelectValue placeholder={t("Language")} />
                </SelectTrigger>
                <SelectContent className={styles.unifiedSelectContent}>
                  {fullDictionaries.languages.map((language) => (
                    <SelectItem key={language.id} value={String(language.id)} className={styles.unifiedSelectItem}>
                      {language.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>

              <Select 
                value={lang.levelId ? String(lang.levelId) : ""} 
                onValueChange={(val) => updateLanguageItem(lang.id, "levelId", Number(val))}
              >
                <SelectTrigger className={`${styles.unifiedInput} w-32`}>
                  <SelectValue placeholder={t("Level")} />
                </SelectTrigger>
                <SelectContent className={styles.unifiedSelectContent}>
                  {fullDictionaries.language_levels.map((level) => (
                    <SelectItem key={level.id} value={String(level.id)} className={styles.unifiedSelectItem}>
                      {level.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>

              <button
                onClick={() => removeLanguage(lang.id)}
                className="p-2 hover:bg-zinc-800 rounded-lg transition-colors flex-shrink-0"
              >
                <X size={16} className="text-red-500" />
              </button>
            </div>
          ))}
        </div>

        <Button
          variant="outline"
          onClick={addLanguage}
          className="rounded-full border-zinc-800 text-xs mt-4 hover:bg-zinc-900"
        >
          <Plus className="mr-2 h-4 w-4" /> {t("addLanguage")}
        </Button>
      </div>
    </div>
  );
}