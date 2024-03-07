import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetEmployeesQuery extends PageQuery {
  departmentId?: Guid;
  jobId?: Guid;
}

export interface Employee {
  id: Guid;
  name: string;
  departmentName: string;
  jobTitle: string;
}

export default {
  getList: (query: GetEmployeesQuery) => {
    return httpClient.get<PageResult<Employee>>('/Employees', {
      params: query,
    });
  },
};
