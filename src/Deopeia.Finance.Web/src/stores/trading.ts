import contractApi from '@/api/trading/contract-api';
import type { Instrument, InstrumentMap } from '@/models/trading/instrument';
import { usePreferencesStore } from './preferences';

export const useTradingStore = defineStore('trading', () => {
  const { locale } = storeToRefs(usePreferencesStore());
  const instruments: Ref<InstrumentMap> = ref({});

  const instrument: Ref<Instrument> = ref({
    symbol: '',
    name: '',
    currencyCode: '',
  });

  const getInstrument = (symbol: string) => {
    if (!instruments.value[symbol]) {
      contractApi.get(symbol).then((x) => {
        instruments.value[symbol] = {
          symbol: x.data.symbol,
          get name() {
            return x.data.locales.find((y) => y.culture === locale.value.key)!
              .name;
          },
          currencyCode: x.data.currencyCode,
        };
        instrument.value = instruments.value[symbol];
      });
    }
  };

  return { instruments, instrument, getInstrument };
});
