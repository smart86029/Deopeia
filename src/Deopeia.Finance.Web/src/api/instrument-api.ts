import httpClient from './http-client';

export interface Instrument {
  symbol: string;
  name: string;
  currencyCode: string;
  utcOffset: string;
  exchange: Exchange;
}

export interface Exchange {
  code: string;
  name: string;
}

export default {
  get: (symbol: string) => httpClient.get<Instrument>(`/Instruments/${symbol}`),
};
