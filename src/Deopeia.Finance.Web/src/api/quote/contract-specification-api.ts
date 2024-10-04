import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetContractSpecificationsQuery extends PageQuery {
  exchangeId?: string;
  assetId?: Guid;
}

export interface ContractSpecificationRow {
  id: Guid;
  exchange: string;
  symbol: string;
  name: string;
  currency: string;
  underlyingAsset: string;
}

export interface ContractSpecification {
  id: Guid;
  symbol: string;
  symbolTemplate: string;
  exchangeId: string;
  underlyingAssetId: Guid;
  currencyCode: string;
  tickSize: number;
  contractSizeQuantity: number;
  contractSizeUnitCode: string;
  locales: ContractSpecificationLocale[];
}

export interface ContractSpecificationLocale {
  culture: string;
  name: string;
  nameTemplate: string;
}

export default {
  getOptions: (assetId: Guid) =>
    httpClient.get<OptionResult<Guid>[]>('/ContractSpecifications/Options', {
      params: { assetId },
    }),
  getList: (query: GetContractSpecificationsQuery) =>
    httpClient.get<PageResult<ContractSpecificationRow>>(
      '/ContractSpecifications',
      {
        params: query,
      },
    ),
  get: (id: Guid) =>
    httpClient.get<ContractSpecification>(`/ContractSpecifications/${id}`),
  create: (futures: ContractSpecification) =>
    httpClient.post('/ContractSpecifications', futures),
  update: (futures: ContractSpecification) =>
    httpClient.put(`/ContractSpecifications/${futures.id}`, futures),
};
