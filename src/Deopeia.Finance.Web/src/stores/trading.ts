import instrumentApi from '@/api/trading/instrument-api';
import type { Instrument, InstrumentMap } from '@/models/trading/instrument';
import { usePreferencesStore } from './preferences';

export const useTradingStore = defineStore('trading', () => {
  const { locale } = storeToRefs(usePreferencesStore());
  const instruments: Ref<InstrumentMap> = ref({});

  const instrument: Ref<Instrument> = ref({
    symbol: '',
    name: '',
    currencyCode: '',
    pricePrecision: 0.01,
    tickSize: 0.01,
    contractSize: {
      quantity: 1,
      unitCode: '',
    },
    volumeRestriction: {
      min: 0,
      max: 0,
      step: 0,
    },
    sessions: [],
    leverages: [],
  });

  const getInstrument = (symbol: string) => {
    if (!instruments.value[symbol]) {
      instrumentApi.get(symbol).then((x) => {
        instruments.value[symbol] = x.data;
        instrument.value = instruments.value[symbol];
      });
    } else {
      instrument.value = instruments.value[symbol];
    }
  };

  watch(locale, () => {
    instruments.value = {};
  });

  return { instruments, instrument, getInstrument };
});
