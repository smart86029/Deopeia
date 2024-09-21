import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import type { OrderSide } from '@/models/trading/order-side';
import httpClient from '../http-client';

export interface GetStrategiesQuery extends PageQuery {}

export interface StrategyRow {
  id: Guid;
  name: string;
  description?: string;
  isEnabled: boolean;
}

export interface Strategy {
  id: Guid;
  isEnabled: boolean;
  openExpression: string;
  closeExpression: string;
  locales: StrategyLocale[];
  legs: StrategyLeg[];
}

export interface StrategyLocale {
  culture: string;
  name: string;
  description?: string;
}

export interface StrategyLeg {
  serialNumber: number;
  side: OrderSide;
  instrumentId: Guid;
  ticks: number;
  volume: number;
}

export default {
  getList: (query: GetStrategiesQuery) =>
    httpClient.get<PageResult<StrategyRow>>('/Strategies', { params: query }),
  get: (id: Guid) => httpClient.get<Strategy>(`/Strategies/${id}`),
  create: (strategy: Strategy) => httpClient.post('/Strategies', strategy),
  update: (strategy: Strategy) =>
    httpClient.put(`/Strategies/${strategy.id}`, strategy),
};
