import { createI18n } from 'vue-i18n';

import enUS from '../locales/en-US';
import zhTW from '../locales/zh-TW';

const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale')?.replace('-', '') || 'enUS',
  fallbackLocale: 'enUS',
  allowComposition: true,
  globalInjection: true,
  silentTranslationWarn: true,
  messages: {
    enUS,
    zhTW,
  },
});

export default i18n;
