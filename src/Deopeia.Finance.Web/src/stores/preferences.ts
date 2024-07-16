import type { AppLocale } from '@/models/app-locale';
import i18n from '@/plugins/i18n';
import { defineStore } from 'pinia';

export const usePreferencesStore = defineStore('preferences', () => {
  const localeKey = 'locale';
  const locales: AppLocale[] = reactive([
    {
      name: 'English',
      key: 'en-US',
      languageCode: 'en',
      dayjsCode: 'en',
    },
    {
      name: '繁體中文',
      key: 'zh-TW',
      languageCode: 'zh-Hant',
      dayjsCode: 'zh-TW',
    },
  ]);

  const localLocale =
    locales.find((x) => x.key === localStorage.getItem(localeKey)) ||
    locales[0];
  const locale = ref(localLocale);

  watch(locale, (appLocale) => {
    localStorage.setItem(localeKey, appLocale.key);
    i18n.global.locale.value = appLocale.key;

    document
      .querySelector('html')!
      .setAttribute('lang', appLocale.languageCode);
  });

  return { locales, locale };
});
