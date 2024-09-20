import { emptyGuid, type Guid, parseGuid } from '@/models/guid';
import { useQuoteStore } from '@/stores/quote';
import type { RouteLocationNormalized } from 'vue-router';

export const create = (): {
  default: boolean;
  action: 'create' | 'edit';
  id: Guid;
} => ({
  default: true,
  action: 'create',
  id: emptyGuid,
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
    ? parseGuid(route.params.id[0])
    : parseGuid(route.params.id),
});

export const symbol = (
  route: RouteLocationNormalized,
): {
  default: boolean;
  symbol: string;
} => {
  const routeSymbol = Array.isArray(route.params.symbol)
    ? route.params.symbol[0]
    : route.params.symbol;
  const { symbol } = storeToRefs(useQuoteStore());
  symbol.value = routeSymbol;
  return {
    default: true,
    symbol: routeSymbol,
  };
};
