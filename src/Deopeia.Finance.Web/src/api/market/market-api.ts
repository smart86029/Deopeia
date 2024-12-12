import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetStockQuery extends PageQuery {}

export interface GetCommodityQuery extends PageQuery {}

export interface GetForexQuery extends PageQuery {}

export interface GetCryptocurrencyQuery extends PageQuery {}

export interface Stock {
  symbol: string;
  name: string;
}

export interface Commodity {
  symbol: string;
  name: string;
}

export interface Forex {
  symbol: string;
  name: string;
}

export interface Cryptocurrency {
  symbol: string;
  name: string;
}

export default {
  getStock: (query: GetStockQuery) =>
    httpClient.get<PageResult<Stock>>('/Markets/Stock', {
      params: query,
    }),

  getCommodity: (query: GetCommodityQuery) =>
    httpClient.get<PageResult<Commodity>>('/Markets/Commodity', {
      params: query,
    }),

  getForex: (query: GetForexQuery) =>
    httpClient.get<PageResult<Forex>>('/Markets/Forex', {
      params: query,
    }),

  getCryptocurrency: (query: GetCryptocurrencyQuery) =>
    httpClient.get<PageResult<Cryptocurrency>>('/Markets/Cryptocurrency', {
      params: query,
    }),
};
