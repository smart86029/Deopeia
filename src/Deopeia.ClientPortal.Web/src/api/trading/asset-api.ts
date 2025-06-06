import httpClient from '../http-client';

export interface Account {
  currencyCode: string;
  balance: number;
  frozen: number;
  exchangeRate: number;
}

export const assetApi = {
  getList: () => httpClient.get<Account[]>('/Assets'),
};
