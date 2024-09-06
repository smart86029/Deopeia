import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetRolesQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface RoleRow {
  id: Guid;
  name: string;
  isEnabled: boolean;
}

export interface Role {
  id: Guid;
  isEnabled: boolean;
  locales: RoleLocale[];
}

export interface RoleLocale {
  culture: string;
  name: string;
  description?: string;
}

export default {
  getOptions: () =>
    httpClient.get<OptionResult<Guid>[]>('/Identity/Roles/Options'),
  getList: (query: GetRolesQuery) =>
    httpClient.get<PageResult<RoleRow>>(`/Identity/Roles`, { params: query }),
  get: (id: Guid) => httpClient.get<Role>(`/Identity/Roles/${id}`),
  create: (role: Role) => httpClient.post('/Identity/Roles', role),
  update: (role: Role) => httpClient.put(`/Identity/Roles/${role.id}`, role),
};
