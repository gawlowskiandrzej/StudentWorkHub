import "i18next";

import common from "./locales/pl/common.json";
import hero from "./locales/pl/hero.json";
import footer from "./locales/pl/footer.json";
import navigation from "./locales/pl/navigation.json";
import list from "./locales/pl/list.json";
import details from "./locales/pl/details.json";
import searchbar from "./locales/pl/searchbar.json";
import searchView from "./locales/pl/searchView.json";
import loginView from "./locales/pl/login.json";
import register from "./locales/pl/register.json";
import profileCreation from "./locales/pl/profileCreation.json";
import userInfoCard from "./locales/pl/userInfoCard.json";

declare module "i18next" {
  interface CustomTypeOptions {
    defaultNS: "common";

    resources: {
      common: typeof common;
      hero: typeof hero;
      footer: typeof footer;
      navigation: typeof navigation;
      list: typeof list;
      details: typeof details;
      searchbar: typeof searchbar;
      searchView: typeof searchView;
      loginView: typeof loginView;
      register: typeof register;
      profileCreation: typeof profileCreation;
      userInfoCard: typeof userInfoCard;
    };
  }
}