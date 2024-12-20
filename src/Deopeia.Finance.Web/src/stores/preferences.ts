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
  const positive = ref('danger');
  const negative = ref('success');

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

  watch(
    locale,
    (appLocale) => {
      const style = document.documentElement.style;
      style.setProperty(
        '--el-color-positive',
        useCssVar('--el-color-danger-light-5', ref(null)).value,
      );
      style.setProperty(
        '--el-color-negative',
        useCssVar('--el-color-success-light-5', ref(null)).value,
      );
    },
    { immediate: true },
  );

  return { locales, locale, cultures, timeZones, positive, negative };
});
