import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import type { Session } from '@/models/trading/session';
import type { UnderlyingType } from '@/models/underlying-type';
import httpClient from '../http-client';

export interface GetContractsQuery extends PageQuery {
  underlyingType?: number;
}

export interface ContractRow {
  symbol: string;
  name: string;
  currency: string;
  underlyingType: string;
}

export interface Contract {
  symbol: string;
  underlyingType: number;
  currencyCode: string;
  pricePrecision: number;
  tickSize: number;
  contractSizeQuantity: number;
  contractSizeUnitCode: string;
  leverages: string[];
  sessions: Session[];
  locales: ContractLocale[];
}

export interface ContractLocale {
  culture: string;
  name: string;
  description?: string;
}

export const contractApi = {
  getOptions: (underlyingType: UnderlyingType) =>
    httpClient.get<OptionResult<string>[]>('/Contracts/Options', {
      params: { underlyingType },
    }),
  getList: (query: GetContractsQuery) =>
    httpClient.get<PageResult<ContractRow>>('/Contracts', {
      params: query,
    }),
  get: (symbol: string) => httpClient.get<Contract>(`/Contracts/${symbol}`),
  create: (contract: Contract) => httpClient.post('/Contracts', contract),
  update: (contract: Contract) =>
    httpClient.put(`/Contracts/${contract.symbol}`, contract),
};
