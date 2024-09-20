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
  symbol: string;
  exchangeId: string;
  underlyingAssetId: Guid;
  currency: string;
  tickSize: number;
  contractSizeQuantity: number;
  contractSizeUnitCode: string;
  locales: FuturesLocale[];
}

export interface FuturesLocale {
  culture: string;
  name: string;
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
