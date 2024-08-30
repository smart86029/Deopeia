import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetExchangesQuery extends PageQuery {}

export interface ExchangeRow {
  mic: string;
  name: string;
  timeZone: string;
  openingTime: string;
  closingTime: string;
}

export interface Exchange {
  mic: string;
  timeZone: string;
  openingTime: string;
  closingTime: string;
  locales: ExchangeLocale[];
}

export interface ExchangeLocale {
  culture: string;
  name: string;
}

export default {
  getList: (query: GetExchangesQuery) =>
    httpClient.get<PageResult<ExchangeRow>>('/Exchanges', { params: query }),
  get: (mic: string) => httpClient.get<Exchange>(`/Exchanges/${mic}`),
  create: (exchange: Exchange) => httpClient.post('/Exchanges', exchange),
  update: (exchange: Exchange) =>
    httpClient.put(`/Exchanges/${exchange.mic}`, exchange),
};
