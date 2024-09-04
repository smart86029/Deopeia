import optionApi from '@/api/option-api';
import type { AppLocale } from '@/models/app-locale';
import type { OptionResult } from '@/models/option-result';
import i18n from '@/plugins/i18n';
import { defineStore } from 'pinia';

export const usePreferencesStore = defineStore('preferences', () => {
  const localeKey = 'locale';
  const locales: AppLocale[] = reactive([
    {
      name: 'English',
      key: 'en',
      dayjsCode: 'en',
    },
    {
      name: '繁體中文',
      key: 'zh-Hant',
      dayjsCode: 'zh-TW',
    },
  ]);

  const localLocale =
    locales.find((x) => x.key === localStorage.getItem(localeKey)) ||
    locales[0];
  const locale = ref(localLocale);

  const cultures: Ref<OptionResult<string>[]> = ref([]);
  const timeZones: Ref<OptionResult<string>[]> = ref([]);

  watch(
    locale,
    (appLocale) => {
      localStorage.setItem(localeKey, appLocale.key);
      i18n.global.locale.value = appLocale.key;

      optionApi.getCultures().then((x) => (cultures.value = x.data));
      optionApi.getTimeZones().then((x) => (timeZones.value = x.data));

      document.querySelector('html')!.setAttribute('lang', appLocale.key);
    },
    { immediate: true },
  );

  return { locales, locale, cultures, timeZones };
});
