import type { Candle } from '@/models/quote/candle';
import type { Order } from '@/models/quote/order';
import type { Tick } from '@/models/quote/tick';
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';

export const useQuoteStore = defineStore('quote', () => {
  const symbol = ref('XAU');
  const ticks = ref(new Map<string, Tick>());
  const candles = ref(new Map<[string, number], Candle[]>());

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
      const key: [string, number] = [symbol, timeFrame];
      if (!candles.value.has(key)) {
        candles.value.set(key, [candle]);
      }
      candles.value.get(key)?.push(candle);
    },
  );

  hubConnection.on('ReceiveOrderBook', (newBids: Order[], newAsks: Order[]) => {
    bids.value = newBids;
    asks.value = newAsks;
  });

  hubConnection
    .start()
    .then(() => hubConnection.invoke('ChangeSymbol', symbol.value));

  const lastTraded = computed(() => ticks.value.get(symbol.value));

  const lastTradedPrice = computed(() => lastTraded.value?.price || 0);

  const previousClose = computed(() => lastTraded.value?.price || 0);

  const priceChange = computed(
    () => lastTradedPrice.value - previousClose.value,
  );

  const priceRateOfChange = computed(() =>
    previousClose.value === 0
      ? 0
      : Math.round((priceChange.value / previousClose.value) * 10000) / 100,
  );

  watch(symbol, (symbol) => {
    hubConnection.invoke('ChangeSymbol', symbol);
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
