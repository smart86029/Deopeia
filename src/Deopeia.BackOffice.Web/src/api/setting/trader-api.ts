import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import type { Money } from '@/models/trading/money';
import httpClient from '../http-client';

export interface GetTradersQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface TraderRow {
  id: Guid;
  name: string;
  isEnabled: boolean;
  currencyCode: string;
  currency: string;
  balance: number;
}

export interface Trader {
  id: Guid;
  name: string;
  isEnabled: boolean;
  currencyCode: string;
}

export const traderApi = {
  getList: (query: GetTradersQuery) =>
    httpClient.get<PageResult<TraderRow>>('/Traders', { params: query }),
  get: (id: Guid) => httpClient.get<Trader>(`/Traders/${id}`),
  create: (trader: Trader) => httpClient.post('/Traders', trader),
  update: (trader: Trader) => httpClient.put(`/Traders/${trader.id}`, trader),
  deposit: (id: Guid, money: Money) =>
    httpClient.post(`/Traders/${id}/Deposit`, money),
  withdraw: (id: Guid, money: Money) =>
    httpClient.post(`/Traders/${id}/Withdraw`, money),
};
