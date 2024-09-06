import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetUsersQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface UserRow {
  id: Guid;
  userName: string;
  isEnabled: boolean;
}

export default {
  getList: (query: GetUsersQuery) =>
    httpClient.get<PageResult<UserRow>>(`/Identity/Users`, { params: query }),
};
