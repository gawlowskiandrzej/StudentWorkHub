"use client";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { StepProps } from "@/constants/profileCreation/steps";
import styles from '@/styles/ProfileCreationStyle.module.css';
import { useTranslation } from "react-i18next";
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";

export default function StepMajor({ data, updateData }: StepProps) {

  const { t } = useTranslation("profileCreation");
  const { fullDictionaries, loading, error } = useProfileCreationDictionaries();

  if (loading || !fullDictionaries) {
    return (
      <div className="flex flex-col items-center space-y-8 fadeInUp">
        <div className="text-center space-y-4">
          <h2 className={styles.sectionTitle}>
            {t("majorSelectionTitle")}
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
          {t("majorSelectionTitle")}
        </h2>
      </div>

      <div className={styles.inputWrapper}>
        <div className={styles.inputWrapper + " mx-auto max-w-md"}>
          <label className={styles.inputLabel}>
            {t("selectYourPreferredMajor")} <span className={styles.requiredStar}>*</span>
          </label>
          <Select
            value={data.major ? String(data.major) : ""}
            onValueChange={(val) => updateData("major", Number(val))}
          >
            <SelectTrigger className={styles.unifiedInput}>
              <SelectValue placeholder={t("majorPlaceholder")} />
            </SelectTrigger>

            <SelectContent className={styles.unifiedSelectContent}>
              {fullDictionaries.leading_categories.map((category) => (
                <SelectItem key={category.id} value={String(category.id)} className={styles.unifiedSelectItem}>
                  {category.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

      </div>
    </div>
  );
}