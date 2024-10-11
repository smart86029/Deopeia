import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetFuturesContractsQuery extends PageQuery {
  exchangeId?: string;
  assetId?: Guid;
}

export interface FuturesContractRow {
  id: Guid;
  exchange: string;
  symbol: string;
  name: string;
  currency: string;
  underlyingAsset: string;
}

export interface CreateFuturesContractCommand {
  contractSpecificationId: Guid;
  expirationDate: Date;
}

export interface FuturesContract {
  id: Guid;
  symbol: string;
  expirationDate: Date;
  exchange: string;
  underlyingAsset: string;
  currency: string;
  tickSize: number;
  contractSizeQuantity: number;
  contractSizeUnit: string;
  locales: FuturesContractLocale[];
}

export interface FuturesContractLocale {
  culture: string;
  name: string;
}

export default {
  getOptions: (assetId: Guid) =>
    httpClient.get<OptionResult<Guid>[]>('/FuturesContracts/Options', {
      params: { assetId },
    }),
  getList: (query: GetFuturesContractsQuery) =>
    httpClient.get<PageResult<FuturesContractRow>>('/FuturesContracts', {
      params: query,
    }),
  get: (id: Guid) => httpClient.get<FuturesContract>(`/FuturesContracts/${id}`),
  create: (futuresContract: CreateFuturesContractCommand) =>
    httpClient.post('/FuturesContracts', futuresContract),
  update: (futuresContract: FuturesContract) =>
    httpClient.put(`/FuturesContracts/${futuresContract.id}`, futuresContract),
};
