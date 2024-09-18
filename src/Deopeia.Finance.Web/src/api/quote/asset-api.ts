import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetAssetsQuery extends PageQuery {}

export interface AssetRow {
  id: Guid;
  code: string;
  name: string;
  description?: string;
}

export interface Asset {
  id: Guid;
  code: string;
  locales: AssetLocale[];
}

export interface AssetLocale {
  culture: string;
  name: string;
  description?: string;
}

export default {
  getList: (query: GetAssetsQuery) =>
    httpClient.get<PageResult<AssetRow>>('/Assets', { params: query }),
  get: (id: Guid) => httpClient.get<Asset>(`/Assets/${id}`),
  create: (asset: Asset) => httpClient.post('/Assets', asset),
  update: (asset: Asset) => httpClient.put(`/Assets/${asset.id}`, asset),
};
