import { useTranslation } from "react-i18next";
import { TranslationKey } from "./types";

export const useT = () => {
  const { t } = useTranslation();
  return (key: TranslationKey) => t(key);
};