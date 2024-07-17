import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';

export const useQuoteStore = defineStore('quote', async () => {
  const hubConnection = new HubConnectionBuilder()
    .withUrl('/hub/RealTime', {
      accessTokenFactory: () => 'BB',
      transport: HttpTransportType.WebSockets,
    })
    .withAutomaticReconnect()
    .build();

  hubConnection.on('ReceiveQuote', (quote: string) => console.log('A' + quote));

  await hubConnection.start();

  return {};
});
