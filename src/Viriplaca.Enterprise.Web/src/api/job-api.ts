import type { Guid } from '@/models/guid';
import type { PageResult } from '@/models/page';
import httpClient from './http-client';

export interface Job {
  id: Guid;
  title: string;
  isEnabled: boolean;
  employeeCount: number;
}

export default {
  getList: (isEnabled?: boolean) => {
    return httpClient.get<PageResult<Job>>('/Jobs', {
      params: { isEnabled },
    });
  },
};
