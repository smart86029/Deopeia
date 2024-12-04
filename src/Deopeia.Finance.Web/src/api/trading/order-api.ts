import type { Guid } from '@/models/guid';
import type { OrderSide } from '@/models/trading/order-side';
import httpClient from '../http-client';

export interface Order {
  side: OrderSide;
  symbol: string;
  volume: number;
  currencyCode: string;
  price?: number;
  leverage: number;
  stopLossPrice?: number;
  takeProfitPrice?: number;
  accountId: Guid;
}

export default {
  create: (order: Order) => httpClient.post('/Orders', order),
};
