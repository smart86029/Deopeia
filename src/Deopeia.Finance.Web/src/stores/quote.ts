import type { RealTimeQuote } from '@/models/quote/real-time-quote';
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';

export const useQuoteStore = defineStore('quote', () => {
  const symbol = ref('');
  const realTimeQuotes = ref([] as RealTimeQuote[]);
  realTimeQuotes.value.push({
    symbol: 'GCZ2024',
    lastTradedAt: new Date(),
    lastTradedPrice: 960,
    previousClose: 934,
  });
  const hubConnection = new HubConnectionBuilder()
    .withUrl('/hub/RealTime', {
      accessTokenFactory: () => 'BB',
      transport: HttpTransportType.WebSockets,
    })
    .withAutomaticReconnect()
    .build();

  hubConnection.on('ReceiveQuote', (quote: RealTimeQuote) => {
    if (
      !realTimeQuotes.value.find(
        (x) => x.symbol == quote.symbol && x.lastTradedAt == quote.lastTradedAt,
      )
    ) {
      realTimeQuotes.value.push(quote);
      quotes.get(quote.symbol).value = quote.lastTradedPrice;
    }
  });

  hubConnection.start();

  const quotes = reactive(new Map());
  quotes.set('GCZ2024', ref(0));

  const lastTraded = computed(() =>
    realTimeQuotes.value.findLast((x) => x.symbol == symbol.value),
  );

  const lastTradedPrice = computed(
    () => lastTraded.value?.lastTradedPrice || 0,
  );

  const previousClose = computed(() => lastTraded.value?.previousClose || 0);

  const priceChange = computed(
    () => lastTradedPrice.value - previousClose.value,
  );

  const priceRateOfChange = computed(() =>
    previousClose.value === 0
      ? 0
      : Math.round((priceChange.value / previousClose.value) * 10000) / 100,
  );

  return {
    quotes,
    symbol,
    realTimeQuotes,
    lastTradedPrice,
    priceChange,
    priceRateOfChange,
  };
});
