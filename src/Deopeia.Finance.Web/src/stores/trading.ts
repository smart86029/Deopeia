import contractApi from '@/api/trading/contract-api';
import type { InstrumentMap } from '@/models/trading/instrument';
import { usePreferencesStore } from './preferences';

export const useTradingStore = defineStore('trading', () => {
  const { locale } = storeToRefs(usePreferencesStore());
  const instruments: Ref<InstrumentMap> = ref({});

  const getInstrument = async (symbol: string) => {
    if (!instruments.value[symbol]) {
      const contract = await contractApi.get(symbol);
      instruments.value[symbol] = {
        symbol: contract.data.symbol,
        name: contract.data.locales.find((x) => x.culture === locale.value.key)!
          .name,
        currencyCode: contract.data.currencyCode,
      };
    }

    return instruments.value[symbol];
  };

  return { instruments, getInstrument };
});
