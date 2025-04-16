import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetStocksQuery extends PageQuery {
  industry?: number;
}

export interface Stock {
  symbol: string;
  name: string;
  price: number;
  PriceChange: number;
  volume: number;
  priceToEarningsRatio: number;
  priceBookRatio: number;
  dividendYield: number;
  industry: string;
}

export default {
  getList: (query: GetStocksQuery) =>
    httpClient.get<PageResult<Stock>>(`/Stocks`, { params: query }),
};
