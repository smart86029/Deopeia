import type { RealTimeQuote } from '@/models/quote/real-time-quote';
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';

export const useQuoteStore = defineStore('quote', () => {
  const realTimeQuotes = ref([] as RealTimeQuote[]);
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
    }
  });

  hubConnection.start();

  return { realTimeQuotes };
});
