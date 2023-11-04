import { ref, reactive, watch } from 'vue';
import { defineStore } from 'pinia';
import type { AppLocale } from '@/models/app-locale';
import { useI18n } from 'vue-i18n';

export const usePreferencesStore = defineStore('preferences', () => {
  const localeKey = 'locale';
  const locales: AppLocale[] = reactive([
    {
      name: 'English',
      key: 'en-US',
      value: 'enUS',
      languageCode: 'en',
    },
    {
      name: '繁體中文',
      key: 'zh-TW',
      value: 'zhTW',
      languageCode: 'zh-Hant',
    },
  ]);

  const localLocale =
    locales.find((x) => x.key === localStorage.getItem(localeKey)) ||
    locales[0];
  const locale = ref(localLocale);

  const { locale: vueLocale } = useI18n();
  watch(locale, (appLocale) => {
    localStorage.setItem(localeKey, appLocale.key);
    vueLocale.value = appLocale.value;

    document
      .querySelector('html')!
      .setAttribute('lang', appLocale.languageCode);
  });

  return { locales, locale };
});
