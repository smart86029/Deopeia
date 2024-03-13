import type { ApprovalStatus } from '@/models/approval-status';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import type { dayjs } from 'element-plus';
import httpClient from './http-client';

export interface GetLeavesQuery extends PageQuery {
  startedAt?: dayjs.Dayjs;
  endedAt?: dayjs.Dayjs;
  approvalStatus?: ApprovalStatus;
}

export interface Leave {
  type: number;
}

export default {
  getTypes: () => httpClient.get<OptionResult<number>[]>('/Leaves/Types'),
  getList: (query: GetLeavesQuery) => {
    return httpClient.get<PageResult<Leave>>('/Leaves', {
      params: query,
    });
  },
};
