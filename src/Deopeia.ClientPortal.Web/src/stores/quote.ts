import type { Candle, CandleMap } from '@/models/quote/candle';
import type { Order } from '@/models/quote/order';
import type { Tick } from '@/models/quote/tick';
import { TimeFrame } from '@/models/quote/time-frame';
import {
  HttpTransportType,
  HubConnectionBuilder,
  HubConnectionState,
} from '@microsoft/signalr';

export const useQuoteStore = defineStore('quote', () => {
  const symbol = ref('XAU');
  const ticks = ref(new Map<string, Tick>());
  const candles = ref({} as CandleMap);

  const bids = ref([] as Order[]);
  const asks = ref([] as Order[]);

  const hubConnection = new HubConnectionBuilder()
    .withUrl('/hub/RealTime', {
      accessTokenFactory: () => 'BB',
      transport: HttpTransportType.WebSockets,
    })
    .withAutomaticReconnect()
    .build();

  hubConnection.on('ReceiveTick', (symbol: string, tick: Tick) => {
    ticks.value.set(symbol, tick);
  });

  hubConnection.on(
    'ReceiveCandle',
    (symbol: string, timeFrame: number, candle: Candle) => {
      if (!candles.value[symbol]) {
        candles.value[symbol] = {};
      }
      if (!candles.value[symbol][timeFrame]) {
        candles.value[symbol][timeFrame] = [];
      }
      const array = candles.value[symbol][timeFrame];
      const index = array.findIndex((x) => x.timestamp === candle.timestamp);
      if (index > -1) {
        array[index] = candle;
      } else {
        array.push(candle);
      }
    },
  );

  hubConnection.on(
    'ReceiveOrderBook',
    (orderBookSymbol: string, newBids: Order[], newAsks: Order[]) => {
      if (orderBookSymbol !== symbol.value) {
        return;
      }
      bids.value = newBids;
      asks.value = newAsks;
    },
  );

  hubConnection
    .start()
    .then(() => hubConnection.invoke('ChangeSymbol', symbol.value));

  const lastTraded = computed(() => ticks.value.get(symbol.value));

  const lastTradedPrice = computed(() => lastTraded.value?.price || 0);

  const previousClose = computed(() => {
    if (!candles.value[symbol.value]) {
      return 0;
    }
    const array = candles.value[symbol.value][TimeFrame.M1];
    return array.length === 0 ? 0 : array.slice(-1)[0].close;
  });

  const priceChange = computed(
    () => lastTradedPrice.value - previousClose.value,
  );

  const priceRateOfChange = computed(() =>
    previousClose.value === 0
      ? 0
      : Math.round((priceChange.value / previousClose.value) * 10000) / 100,
  );

  watch(symbol, (symbol) => {
    if (hubConnection.state === HubConnectionState.Connected) {
      hubConnection.invoke('ChangeSymbol', symbol);
    }
  });

  return {
    symbol,
    ticks,
    candles,
    bids,
    asks,
    lastTradedPrice,
    priceChange,
    priceRateOfChange,
  };
});
