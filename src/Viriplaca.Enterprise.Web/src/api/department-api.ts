import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetDepartmentQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface DepartmentRow {
  id: Guid;
  name: string;
  isEnabled: boolean;
  parentId?: Guid;
  headName: string;
  employeeCount: number;
}

export interface Department {
  id: Guid;
  name: string;
  isEnabled: boolean;
  parentId?: Guid;
}

export default {
  getOptions: () =>
    httpClient.get<OptionResult<Guid>[]>('/Departments/Options'),
  getList: (query: GetDepartmentQuery) =>
    httpClient.get<PageResult<DepartmentRow>>('/Departments', {
      params: query,
    }),
  get: (id: Guid) => httpClient.get<Department>(`/Departments/${id}`),
  create: (command: DepartmentRow) => httpClient.post('/Departments'),
};
