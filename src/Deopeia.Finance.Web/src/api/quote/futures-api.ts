import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetFuturessQuery extends PageQuery {}

export interface FuturesRow {
  id: Guid;
  exchange: string;
  symbol: string;
  name: string;
  currency: string;
  underlyingAsset: string;
}

export interface Futures {
  id: Guid;
  code: string;
  locales: FuturesLocale[];
}

export interface FuturesLocale {
  culture: string;
  name: string;
  description?: string;
}

export default {
  getList: (query: GetFuturessQuery) =>
    httpClient.get<PageResult<FuturesRow>>('/FuturesContracts', {
      params: query,
    }),
  get: (id: Guid) => httpClient.get<Futures>(`/FuturesContracts/${id}`),
  create: (futures: Futures) => httpClient.post('/FuturesContracts', futures),
  update: (futures: Futures) =>
    httpClient.put(`/FuturesContracts/${futures.id}`, futures),
};
