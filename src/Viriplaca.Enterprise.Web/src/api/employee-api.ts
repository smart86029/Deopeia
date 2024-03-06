import type { Guid } from '@/models/guid';
import type { PageResult } from '@/models/page';
import httpClient from './http-client';

export interface Employee {
  id: Guid;
  name: string;
  departmentName: string;
  jobTitle: string;
}

export default {
  getList: (departmentId?: Guid, jobId?: Guid) => {
    return httpClient.get<PageResult<Employee>>('/Employees', {
      params: { departmentId, jobId },
    });
  },
};
