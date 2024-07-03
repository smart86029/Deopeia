import { Guid } from '@/models/guid';
import type { RouteLocationNormalized } from 'vue-router';

export const create = (): {
  default: boolean;
  action: 'create' | 'edit';
  id: Guid;
} => ({
  default: true,
  action: 'create',
  id: Guid.empty,
});

export const edit = (
  route: RouteLocationNormalized,
): {
  default: boolean;
  action: 'create' | 'edit';
  id: Guid;
} => ({
  default: true,
  action: 'edit',
  id: Array.isArray(route.params.id)
    ? Guid.parse(route.params.id[0])
    : Guid.parse(route.params.id),
});

export const symbol = (
  route: RouteLocationNormalized,
): {
  default: boolean;
  symbol: string;
} => ({
  default: true,
  symbol: Array.isArray(route.params.symbol)
    ? route.params.symbol[0]
    : route.params.symbol,
});
