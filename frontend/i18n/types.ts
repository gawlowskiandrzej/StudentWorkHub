import hero from "@/i18n/locales/pl/hero.json";
import common from "@/i18n/locales/pl/common.json";
import { useTranslation } from "react-i18next";

type Resources = {
  hero: typeof hero;
  common: typeof common;
};
export const useT = <NS extends keyof Resources>(ns: NS) => {
  const { t } = useTranslation<NS>();

  return <K extends keyof Resources[NS] & string>(key: K) => t(key);
};
