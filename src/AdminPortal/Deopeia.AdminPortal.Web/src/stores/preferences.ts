import type { AppLocale } from '@/models/app-locale';
import i18n from '@/plugins/i18n';
import { dayjs } from 'element-plus';
import { defineStore } from 'pinia';

/**
 * Store for managing user preferences including locale and UI theme settings
 */
export const usePreferencesStore = defineStore('preferences', () => {
  // Constants
  const LOCALE_STORAGE_KEY = 'locale';

  // Available locales configuration
  const availableLocales: AppLocale[] = reactive([
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

  const getInitialLocale = (): AppLocale => {
    const storedLocaleKey = localStorage.getItem(LOCALE_STORAGE_KEY);
    return availableLocales.find((locale) => locale.key === storedLocaleKey) || availableLocales[0];
  };

  const currentLocale = ref<AppLocale>(getInitialLocale());

  watch(
    currentLocale,
    (newLocale: AppLocale) => {
      try {
        // Persist locale preference
        localStorage.setItem(LOCALE_STORAGE_KEY, newLocale.key);

        // Update i18n
        i18n.global.locale.value = newLocale.key;

        // Update dayjs locale
        dayjs.locale(newLocale.dayjsCode);

        // Update document language attribute for accessibility
        const htmlElement = document.querySelector('html');
        if (htmlElement) {
          htmlElement.setAttribute('lang', newLocale.key);
        }
      } catch (error) {
        console.error('Failed to update locale:', error);
      }
    },
    { immediate: true },
  );

  return {
    locales: readonly(availableLocales),
    locale: currentLocale,
  };
});
