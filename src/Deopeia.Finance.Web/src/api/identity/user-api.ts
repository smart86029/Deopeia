import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from '../http-client';

export interface GetUsersQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface UserRow {
  id: Guid;
  userName: string;
  isEnabled: boolean;
  roleIds: Guid[];
}

export interface User {
  id: Guid;
  userName: string;
  password?: string;
  isEnabled: boolean;
  roleIds: Guid[];
}

export default {
  getList: (query: GetUsersQuery) =>
    httpClient.get<PageResult<UserRow>>(`/Identity/Users`, { params: query }),
  get: (id: Guid) => httpClient.get<User>(`/Identity/Users/${id}`),
  create: (user: User) => httpClient.post('/Identity/Users', user),
  update: (user: User) => httpClient.put(`/Identity/Users/${user.id}`, user),
};
