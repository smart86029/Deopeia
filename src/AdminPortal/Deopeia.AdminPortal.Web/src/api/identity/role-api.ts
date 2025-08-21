import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetRolesQuery extends PageQuery {
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
  locales: RoleLocale[];
  permissionCodes: string[];
}

export interface RoleLocale {
  culture: string;
  name: string;
  description?: string;
}

export const roleApi = {
  getOptions: () => httpClient.get<OptionResult<string>[]>('/Roles/Options'),
  getList: (query: GetRolesQuery) =>
    httpClient.get<PageResult<RoleRow>>(`/Roles`, { params: query }),
  get: (code: string) => httpClient.get<Role>(`/Roles/${code}`),
  create: (role: Role) => httpClient.post('/Roles', role),
  update: (role: Role) => httpClient.put(`/Roles/${role.code}`, role),
};
