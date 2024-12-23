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
    pricePrecision: 0.01,
    tickSize: 0.01,
    contractSizeQuantity: 1,
    contractSizeUnitCode: '',
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
          pricePrecision: x.data.pricePrecision,
          tickSize: x.data.tickSize,
          contractSizeQuantity: x.data.contractSizeQuantity,
          contractSizeUnitCode: x.data.contractSizeUnitCode,
        };
        instrument.value = instruments.value[symbol];
      });
    }
  };

  return { instruments, instrument, getInstrument };
});
