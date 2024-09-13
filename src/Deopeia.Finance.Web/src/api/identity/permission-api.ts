import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetPermissionsQuery extends PageQuery {
  code?: string;
  isEnabled?: boolean;
}

export interface PermissionRow {
  id: Guid;
  code: string;
  name: string;
  description?: string;
  isEnabled: boolean;
}

export interface Permission {
  id: Guid;
  code: string;
  isEnabled: boolean;
  locales: PermissionLocale[];
}

export interface PermissionLocale {
  culture: string;
  name: string;
  description?: string;
}

export default {
  getOptions: () =>
    httpClient.get<OptionResult<Guid>[]>('/Permissions/Options'),
  getList: (query: GetPermissionsQuery) =>
    httpClient.get<PageResult<PermissionRow>>(`/Permissions`, {
      params: query,
    }),
  get: (id: Guid) => httpClient.get<Permission>(`/Permissions/${id}`),
  create: (permission: Permission) =>
    httpClient.post('/Permissions', permission),
  update: (permission: Permission) =>
    httpClient.put(`/Permissions/${permission.id}`, permission),
};
