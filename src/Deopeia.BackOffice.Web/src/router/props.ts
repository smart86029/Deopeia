import { emptyGuid, type Guid, parseGuid } from '@/models/guid';
import type { RouteLocationNormalized } from 'vue-router';

export const createId = (): {
  default: boolean;
  action: 'create' | 'edit';
  id: Guid;
} => ({
  default: true,
  action: 'create',
  id: emptyGuid,
});

export const editId = (
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

export const createCode = (): {
  default: boolean;
  action: 'create' | 'edit';
  code: string;
} => ({
  default: true,
  action: 'create',
  code: '',
});

export const editCode = (
  route: RouteLocationNormalized,
): {
  default: boolean;
  action: 'create' | 'edit';
  code: string;
} => ({
  default: true,
  action: 'edit',
  code: Array.isArray(route.params.code)
    ? route.params.code[0]
    : route.params.code,
});
