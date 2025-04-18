import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetDepositsQuery extends PageQuery {
  code?: string;
  isEnabled?: boolean;
}

export interface DepositRow {
  id: Guid;
  code: string;
  isEnabled: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface Deposit {
  code: string;
  isEnabled: boolean;
}

export const fundApi = {
  getDeposits: (query: GetDepositsQuery) =>
    httpClient.get<PageResult<DepositRow>>(`/Fund/Deposits`, { params: query }),
  get: (id: Guid) => httpClient.get<Deposit>(`/Fund/Deposits/${id}`),
};
