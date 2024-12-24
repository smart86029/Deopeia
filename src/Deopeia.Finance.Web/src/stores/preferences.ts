import type { AppLocale } from '@/models/app-locale';
import i18n from '@/plugins/i18n';
import { dayjs } from 'element-plus';
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
      dayjsCode: 'zh-tw',
    },
  ]);

  const localLocale =
    locales.find((x) => x.key === localStorage.getItem(localeKey)) ||
    locales[0];
  const locale = ref(localLocale);

  const positive = ref('danger');
  const negative = ref('success');

  watch(
    locale,
    (appLocale) => {
      localStorage.setItem(localeKey, appLocale.key);
      i18n.global.locale.value = appLocale.key;
      dayjs.locale(appLocale.dayjsCode);

      document.querySelector('html')!.setAttribute('lang', appLocale.key);
    },
    { immediate: true },
  );

  watch(
    locale,
    () => {
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

  return { locales, locale, positive, negative };
});
