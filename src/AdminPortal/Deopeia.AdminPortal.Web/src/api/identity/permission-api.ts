import type { OptionResult } from '@/models/option-result';
import type { PagedRequest, PagedResponse } from '@/models/page';
import httpClient from '../http-client';

export interface GetPermissionsQuery extends PagedRequest {
  code?: string;
  isEnabled?: boolean;
}

export interface PermissionRow {
  code: string;
  name: string;
  description?: string;
  isEnabled: boolean;
}

export interface Permission {
  code: string;
  isEnabled: boolean;
  localizations: PermissionLocalization[];
}

export interface PermissionLocalization {
  culture: string;
  name: string;
  description?: string;
}

export const permissionApi = {
  getOptions: () =>
    httpClient
      .get<OptionResult<string>[]>('/Permissions/Options')
      .then((response) => response.data),
  getList: (query: GetPermissionsQuery) =>
    httpClient
      .get<PagedResponse<PermissionRow>>(`/Permissions`, {
        params: query,
      })
      .then((response) => response.data),
  get: (code: string) =>
    httpClient.get<Permission>(`/Permissions/${code}`).then((response) => response.data),
  create: (permission: Permission) => httpClient.post('/Permissions', permission),
  update: (permission: Permission) => httpClient.put(`/Permissions/${permission.code}`, permission),
};
