import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetStockQuery extends PageQuery {}

export interface GetIndexQuery extends PageQuery {}

export interface GetCommodityQuery extends PageQuery {}

export interface GetForexQuery extends PageQuery {}

export interface GetCryptocurrencyQuery extends PageQuery {}

interface Contract {
  symbol: string;
  name: string;
  isFavorite: boolean;
}

export interface Stock extends Contract {}

export interface Index extends Contract {}

export interface Commodity extends Contract {}

export interface Forex extends Contract {}

export interface Cryptocurrency extends Contract {}

export default {
  getStock: (query: GetStockQuery) =>
    httpClient.get<PageResult<Stock>>('/Markets/Stock', {
      params: query,
    }),

  getIndex: (query: GetIndexQuery) =>
    httpClient.get<PageResult<Index>>('/Markets/Index', {
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
