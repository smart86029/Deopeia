import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetExchangesQuery extends PageQuery {}

export interface ExchangeRow {
  id: Guid;
  name: string;
  code: string;
  timeZone: string;
  openingTime: string;
  closingTime: string;
}

export interface Exchange {
  id: Guid;
  code: string;
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
  get: (id: Guid) => httpClient.get<Exchange>(`/Exchanges/${id}`),
  create: (exchange: Exchange) => httpClient.post('/Exchanges', exchange),
  update: (exchange: Exchange) =>
    httpClient.put(`/Exchanges/${exchange.id}`, exchange),
};
