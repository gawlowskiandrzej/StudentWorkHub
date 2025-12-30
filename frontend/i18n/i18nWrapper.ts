import { useTranslation } from "react-i18next";

export const useT = (namespace?: string) => {
  const { t } = useTranslation(namespace);
  return t;
};
