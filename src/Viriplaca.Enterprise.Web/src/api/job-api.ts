import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetJobsQuery extends PageQuery {
  isEnabled?: boolean;
}

export interface JobRow {
  id: Guid;
  title: string;
  isEnabled: boolean;
  employeeCount: number;
}

export interface Job {
  id: Guid;
  title: string;
  isEnabled: boolean;
}

export default {
  getOptions: () => httpClient.get<OptionResult<Guid>[]>('/Jobs/Options'),
  getList: (query: GetJobsQuery) =>
    httpClient.get<PageResult<JobRow>>('/Jobs', { params: query }),
  get: (id: Guid) => httpClient.get<Job>(`/Jobs/${id}`),
  create: (job: Job) => httpClient.post('/Jobs', job),
  update: (job: Job) => httpClient.put(`/Jobs/${job.id}`, job),
};
