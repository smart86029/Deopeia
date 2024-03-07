import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetJobsQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface Job {
  id: Guid;
  title: string;
  isEnabled: boolean;
  employeeCount: number;
}

export default {
  getOptions: () => httpClient.get<OptionResult<Guid>[]>('/Jobs/Options'),
  getList: (query: GetJobsQuery) => {
    return httpClient.get<PageResult<Job>>('/Jobs', {
      params: query,
    });
  },
};
