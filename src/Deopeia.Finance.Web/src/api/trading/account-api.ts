import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import type { Money } from '@/models/trading/money';
import httpClient from '../http-client';

export interface GetAccountsQuery extends PageQuery {
  isEnabled?: boolean;
  currencyCode?: string;
}

export interface AccountRow {
  id: Guid;
  accountNumber: string;
  isEnabled: boolean;
  currencyCode: string;
  currency: string;
  balance: number;
}

export interface Account {
  id: Guid;
  accountNumber: string;
  isEnabled: boolean;
  currencyCode: string;
}

export default {
  getList: (query: GetAccountsQuery) =>
    httpClient.get<PageResult<AccountRow>>('/Accounts', { params: query }),
  get: (id: Guid) => httpClient.get<Account>(`/Accounts/${id}`),
  create: (account: Account) => httpClient.post('/Accounts', account),
  update: (account: Account) =>
    httpClient.put(`/Accounts/${account.id}`, account),
  deposit: (id: Guid, money: Money) =>
    httpClient.post(`/Accounts/${id}/Deposit`, money),
  withdraw: (id: Guid, money: Money) =>
    httpClient.post(`/Accounts/${id}/Withdraw`, money),
};
