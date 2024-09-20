import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetExchangesQuery extends PageQuery {}

export interface ExchangeRow {
  mic: string;
  name: string;
  abbreviation?: string;
  timeZone: string;
}

export interface Exchange {
  mic: string;
  timeZone: string;
  locales: ExchangeLocale[];
}

export interface ExchangeLocale {
  culture: string;
  name: string;
  abbreviation?: string;
}

export default {
  getOptions: () =>
    httpClient.get<OptionResult<string>[]>('/Exchanges/Options'),
  getList: (query: GetExchangesQuery) =>
    httpClient.get<PageResult<ExchangeRow>>('/Exchanges', { params: query }),
  get: (mic: string) => httpClient.get<Exchange>(`/Exchanges/${mic}`),
  create: (exchange: Exchange) => httpClient.post('/Exchanges', exchange),
  update: (exchange: Exchange) =>
    httpClient.put(`/Exchanges/${exchange.mic}`, exchange),
};
