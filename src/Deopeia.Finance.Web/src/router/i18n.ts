import { usePreferencesStore } from '@/stores/preferences';
import type { RouteLocationNormalized } from 'vue-router';

export const switchLocale = (key: string) => {
  const { locale, locales } = storeToRefs(usePreferencesStore());
  const newLocale = locales.value.find((x) => x.key === key)!;
  if (newLocale) {
    locale.value = newLocale;
  }
};

export const setLocale = (to: RouteLocationNormalized) => {
  const { locale, locales } = storeToRefs(usePreferencesStore());
  const newLocale = locales.value.find(
    (x) => x.key === (to.params.locale as string),
  );
  if (newLocale) {
    switchLocale(locale.value.key);
    return;
  }

  return { ...to, params: { ...to.params, locale: locale.value.key } };
};
