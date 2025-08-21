import { optionApi } from '@/api/option-api';
import type { OptionResult } from '@/models/option-result';
import { usePreferencesStore } from './preferences';

/**
 * Store for managing application options fetched from the API
 */
export const useOptionStore = defineStore('option', () => {
  const { locale } = storeToRefs(usePreferencesStore());

  const cultures = ref<OptionResult<string>[]>([]);
  const currencies = ref<OptionResult<string>[]>([]);
  const timeZones = ref<OptionResult<string>[]>([]);
  const units = ref<OptionResult<string>[]>([]);

  const fetchOptions = async (): Promise<void> => {
    const [culturesResponse, currenciesResponse, timeZonesResponse, unitsResponse] =
      await Promise.all([
        optionApi.getCultures(),
        optionApi.getCurrencies(),
        optionApi.getTimeZones(),
        optionApi.getUnits(),
      ]);

    cultures.value = culturesResponse.data;
    currencies.value = currenciesResponse.data;
    timeZones.value = timeZonesResponse.data;
    units.value = unitsResponse.data;
  };

  watch(
    locale,
    () => {
      fetchOptions();
    },
    { immediate: true },
  );

  return {
    cultures: cultures,
    currencies: readonly(currencies),
    timeZones: readonly(timeZones),
    units: readonly(units),
  };
});
