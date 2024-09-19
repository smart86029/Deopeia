import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetFuturessQuery extends PageQuery {}

export interface FuturesRow {
  id: Guid;
  code: string;
  name: string;
  description?: string;
}

export interface Futures {
  id: Guid;
  code: string;
  locales: FuturesLocale[];
}

export interface FuturesLocale {
  culture: string;
  name: string;
  description?: string;
}

export default {
  getList: (query: GetFuturessQuery) =>
    httpClient.get<PageResult<FuturesRow>>('/Futuress', { params: query }),
  get: (id: Guid) => httpClient.get<Futures>(`/Futuress/${id}`),
  create: (futures: Futures) => httpClient.post('/Futuress', futures),
  update: (futures: Futures) =>
    httpClient.put(`/Futuress/${futures.id}`, futures),
};
