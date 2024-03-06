import type { Guid } from '@/models/guid';
import type { PageResult } from '@/models/page';
import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

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
  getList: (isEnabled?: boolean) => {
    return httpClient.get<PageResult<Department>>('/Departments', {
      params: { isEnabled },
    });
  },
};
