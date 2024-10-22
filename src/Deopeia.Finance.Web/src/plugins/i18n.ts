import { createI18n } from 'vue-i18n';

import en from '../locales/en';
import zhHant from '../locales/zh-Hant';

const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale') || 'en',
  fallbackLocale: 'en',
  allowComposition: true,
  globalInjection: true,
  silentTranslationWarn: true,
  messages: {
    en: en,
    'zh-Hant': zhHant,
  },
  numberFormats: {
    en: {
      decimal: {
        style: 'decimal',
        maximumFractionDigits: 2,
        minimumFractionDigits: 2,
      },
      integer: {
        style: 'decimal',
        maximumFractionDigits: 0,
      },
      percent: {
        style: 'percent',
      },
    },
    'zh-Hant': {
      decimal: {
        style: 'decimal',
        maximumFractionDigits: 2,
        minimumFractionDigits: 2,
      },
      integer: {
        style: 'decimal',
        maximumFractionDigits: 0,
      },
      percent: {
        style: 'percent',
      },
    },
  },
});

export default i18n;
