import { createI18n } from 'vue-i18n';

import enUS from '../locales/en-US';
import zhTW from '../locales/zh-TW';

const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale') || 'en-US',
  fallbackLocale: 'en-US',
  allowComposition: true,
  globalInjection: true,
  silentTranslationWarn: true,
  messages: {
    'en-US': enUS,
    'zh-TW': zhTW,
  },
});

export default i18n;
