import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import type { PositionType } from '@/models/trading/position-type';
import httpClient from '../http-client';

export interface GetPositionsQuery extends PageQuery {
  type?: PositionType;
}

export interface PositionRow {
  id: Guid;
  type: PositionType;
  name: string;
  openPrice: number;
  price: number;
  unrealisedPnL: number;
}

export interface Position {
  id: Guid;
  accountNumber: string;
  type: PositionType;
  name: string;
  orderType: number;
  price: number;
  volume: number;
}

export default {
  getList: (query: GetPositionsQuery) =>
    httpClient.get<PageResult<PositionRow>>('/Positions', { params: query }),
  get: (id: Guid) => httpClient.get<Position>(`/Positions/${id}`),
  update: (position: Position) =>
    httpClient.put(`/Positions/${position.id}`, position),
};
