import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetFuturesQuery extends PageQuery {
  exchangeId?: string;
  assetId?: Guid;
}

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
  currencyCode: string;
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
  getOptions: (assetId: Guid) =>
    httpClient.get<OptionResult<Guid>[]>('/FuturesContracts/Options', {
      params: { assetId },
    }),
  getList: (query: GetFuturesQuery) =>
    httpClient.get<PageResult<FuturesRow>>('/FuturesContracts', {
      params: query,
    }),
  get: (id: Guid) => httpClient.get<Futures>(`/FuturesContracts/${id}`),
  create: (futures: Futures) => httpClient.post('/FuturesContracts', futures),
  update: (futures: Futures) =>
    httpClient.put(`/FuturesContracts/${futures.id}`, futures),
};
