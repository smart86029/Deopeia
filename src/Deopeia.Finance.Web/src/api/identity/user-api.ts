import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetUsersQuery extends PageQuery {
  userName?: string;
  isEnabled?: boolean;
  roleCode?: string;
}

export interface UserRow {
  id: Guid;
  userName: string;
  isEnabled: boolean;
  roleCodes: string[];
}

export interface User {
  id: Guid;
  userName: string;
  password?: string;
  isEnabled: boolean;
  roleCodes: string[];
}

export const userApi = {
  getList: (query: GetUsersQuery) =>
    httpClient.get<PageResult<UserRow>>(`/Users`, { params: query }),
  get: (id: Guid) => httpClient.get<User>(`/Users/${id}`),
  create: (user: User) => httpClient.post('/Users', user),
  update: (user: User) => httpClient.put(`/Users/${user.id}`, user),
};
