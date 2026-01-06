import i18n from "i18next";
import { initReactI18next } from "react-i18next";

// PL
import plCommon from "./locales/pl/common.json";
import plNavbar from "./locales/pl/navigation.json";
import plFooter from "./locales/pl/footer.json";
import plHero from "./locales/pl/hero.json";
import plList from "./locales/pl/list.json";
import plDetails from "./locales/pl/details.json";
import plSearchbar from "./locales/pl/searchbar.json";
import plSearchView from "./locales/pl/searchView.json";
import plLogin from "./locales/pl/login.json";
// EN
import enCommon from "./locales/en/common.json";
import enNavbar from "./locales/en/navigation.json";
import enFooter from "./locales/en/footer.json";
import enHero from "./locales/en/hero.json";
import enList from "./locales/en/list.json";
import enDetails from "./locales/en/details.json";
import enSearchbar from "./locales/en/searchbar.json";
import enSearchView from "./locales/en/searchView.json";

i18n.use(initReactI18next).init({
  lng: "pl",
  fallbackLng: "en",

  ns: ["common", "navigation", "footer", "hero", "list", "details", "searchbar", "searchView", "loginView"],
  defaultNS: "common",

  resources: {
    pl: {
      common: plCommon,
      navigation: plNavbar,
      footer: plFooter,
      hero: plHero,
      list: plList,
      details: plDetails,
      searchbar: plSearchbar,
      searchView: plSearchView,
      loginView: plLogin
    },
    en: {
      common: enCommon,
      navigation: enNavbar,
      footer: enFooter,
      hero: enHero,
      list: enList,
      details: enDetails,
      searchbar: enSearchbar,
      searchView: enSearchView
    }
  },

  interpolation: {
    escapeValue: false
  }
});

export default i18n;
