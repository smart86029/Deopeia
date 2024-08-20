import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetStocksQuery extends PageQuery {}

export interface Stock {
  symbol: string;
  name: string;
  price: number;
  PriceChange: number;
  volume: number;
  priceToEarningsRatio: number;
  priceBookRatio: number;
  dividendYield: number;
  sector: string;
}

export default {
  getList: (query: GetStocksQuery) =>
    httpClient.get<PageResult<Stock>>(`/Stocks`, { params: query }),
};
