import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import type { OrderSide } from '@/models/trading/order-side';
import httpClient from '../http-client';

export interface GetPositionsQuery extends PageQuery {}

export interface PositionRow {
  id: Guid;
  name: string;
  description?: string;
  isEnabled: boolean;
}

export interface Position {
  id: Guid;
  isEnabled: boolean;
  openExpression: string;
  closeExpression: string;
  locales: PositionLocale[];
  legs: PositionLeg[];
}

export interface PositionLocale {
  culture: string;
  name: string;
  description?: string;
}

export interface PositionLeg {
  serialNumber: number;
  side: OrderSide;
  instrumentId: Guid;
  ticks: number;
  volume: number;
}

export default {
  getList: (query: GetPositionsQuery) =>
    httpClient.get<PageResult<PositionRow>>('/Positions', { params: query }),
  get: (id: Guid) => httpClient.get<Position>(`/Positions/${id}`),
  create: (position: Position) => httpClient.post('/Positions', position),
  update: (position: Position) =>
    httpClient.put(`/Positions/${position.id}`, position),
};
