import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetDepartmentQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface Department {
  id: Guid;
  name: string;
  isEnabled: boolean;
  parentId?: Guid;
  headName: string;
  employeeCount: number;
}

export default {
  getOptions: () =>
    httpClient.get<OptionResult<Guid>[]>('/Departments/Options'),
  getList: (query: GetDepartmentQuery) => {
    return httpClient.get<PageResult<Department>>('/Departments', {
      params: query,
    });
  },
};
