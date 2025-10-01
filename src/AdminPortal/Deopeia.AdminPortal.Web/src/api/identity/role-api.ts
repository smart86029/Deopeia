import type { OptionResult } from '@/models/option-result';
import type { PagedRequest, PagedResponse } from '@/models/page';
import httpClient from '../http-client';

export interface GetRolesRequest extends PagedRequest {
  isEnabled?: boolean;
}

export interface RoleRow {
  code: string;
  name: string;
  description?: string;
  isEnabled: boolean;
}

export interface Role {
  code: string;
  isEnabled: boolean;
  localizations: RoleLocalization[];
  permissionCodes: string[];
}

export interface RoleLocalization {
  culture: string;
  name: string;
  description?: string;
}

export const roleApi = {
  getOptions: () =>
    httpClient.get<OptionResult<string>[]>('/Roles/Options').then((response) => response.data),
  getList: (query: GetRolesRequest) =>
    httpClient
      .get<PagedResponse<RoleRow>>(`/Roles`, { params: query })
      .then((response) => response.data),
  get: (code: string) => httpClient.get<Role>(`/Roles/${code}`).then((response) => response.data),
  create: (role: Role) => httpClient.post('/Roles', role),
  update: (role: Role) => httpClient.put(`/Roles/${role.code}`, role),
  delete: (code: string) => httpClient.delete(`/Roles/${code}`),
};
