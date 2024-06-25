import { createI18n } from 'vue-i18n';
import enUS from './en-US';
import zhTW from './zh-TW';

export default createI18n({
  locale: localStorage.getItem('locale') || 'zh-TW',
  fallbackLocale: 'en-US',
  legacy: false,
  globalInjection: true,
  messages: { 'en-US': enUS, 'zh-TW': zhTW },
  runtimeOnly: false,
});
