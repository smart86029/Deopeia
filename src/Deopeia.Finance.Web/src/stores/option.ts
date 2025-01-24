import { optionApi } from '@/api/option-api';
import type { OptionResult } from '@/models/option-result';
import { defineStore } from 'pinia';
import { usePreferencesStore } from './preferences';

export const useOptionStore = defineStore('option', () => {
  const { locale } = storeToRefs(usePreferencesStore());

  const cultures: Ref<OptionResult<string>[]> = ref([]);
  const currencies: Ref<OptionResult<string>[]> = ref([]);
  const timeZones: Ref<OptionResult<string>[]> = ref([]);
  const units: Ref<OptionResult<string>[]> = ref([]);

  watch(
    locale,
    () => {
      optionApi.getCultures().then((x) => (cultures.value = x.data));
      optionApi.getCurrencies().then((x) => (currencies.value = x.data));
      optionApi.getTimeZones().then((x) => (timeZones.value = x.data));
      optionApi.getUnits().then((x) => (units.value = x.data));
    },
    { immediate: true },
  );

  return { cultures, currencies, timeZones, units };
});
