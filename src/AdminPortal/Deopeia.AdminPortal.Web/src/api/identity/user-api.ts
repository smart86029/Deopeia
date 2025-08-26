import httpClient from '../http-client';

const basePath = '/Users';

export interface GetUsersQuery extends PagedRequest {
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
  getList: async (query: GetUsersQuery) =>
    httpClient
      .get<PagedResponse<UserRow>>(basePath, { params: query })
      .then((response) => response.data),
  get: (id: Guid) => httpClient.get<User>(`${basePath}/${id}`).then((response) => response.data),
  create: (user: User) => httpClient.post(basePath, user),
  update: (user: User) => httpClient.put(`${basePath}/${user.id}`, user),
};
