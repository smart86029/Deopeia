import { createI18n } from 'vue-i18n';

import en from '../locales/en';
import zhHant from '../locales/zh-Hant';

// Type-safe locale keys
export type SupportedLocales = 'en' | 'zh-Hant';

// Constants for better maintainability
const DEFAULT_LOCALE: SupportedLocales = 'en';
const FALLBACK_LOCALE: SupportedLocales = 'en';
const LOCALE_STORAGE_KEY = 'locale';

// Get saved locale from localStorage with type safety
const getSavedLocale = (): SupportedLocales => {
  const saved = localStorage.getItem(LOCALE_STORAGE_KEY) as SupportedLocales;
  return saved && ['en', 'zh-Hant'].includes(saved) ? saved : DEFAULT_LOCALE;
};

// Number format configuration for better reusability
const createNumberFormats = () => ({
  decimal: {
    style: 'decimal' as const,
    maximumFractionDigits: 2,
    minimumFractionDigits: 2,
  },
  integer: {
    style: 'decimal' as const,
    maximumFractionDigits: 0,
  },
  number: {
    style: 'decimal' as const,
  },
  percent: {
    style: 'percent' as const,
  },
});

const i18n = createI18n({
  legacy: false,
  locale: getSavedLocale(),
  fallbackLocale: FALLBACK_LOCALE,
  allowComposition: true,
  globalInjection: true,
  silentTranslationWarn: import.meta.env.PROD, // Only silence in production
  messages: {
    en,
    'zh-Hant': zhHant,
  },
  numberFormats: {
    en: createNumberFormats(),
    'zh-Hant': createNumberFormats(),
  },
});

export default i18n;
