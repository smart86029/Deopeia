import type { ApprovalStatus } from '@/models/approval-status';
import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetLeavesQuery extends PageQuery {
  startedAt?: Date;
  endedAt?: Date;
  approvalStatus?: ApprovalStatus;
}

export interface LeaveRow {
  leaveTypeId: Guid;
  employee: Employee;
}

export interface Leave {
  leaveTypeId: Guid;
  startedAt: Date;
  endedAt: Date;
  reason: string;
  employee: Employee;
}

export interface Employee {
  id: Guid;
  firstName: string;
  lastName?: string;
}

export default {
  getTypes: () => httpClient.get<OptionResult<Guid>[]>('/LeaveTypes/Options'),
  getList: (query: GetLeavesQuery) =>
    httpClient.get<PageResult<LeaveRow>>('/Leaves', { params: query }),
  get: (id: Guid) => httpClient.get<Leave>(`/Leaves/${id}`),
  apply: (leave: Leave) => httpClient.post('/Leaves', leave),
  updateApprovalStatus: (id: Guid, approvalStatus: ApprovalStatus) =>
    httpClient.put(`/Leaves/${id}/ApprovalStatus`, { approvalStatus }),
  cancel: (id: Guid) => httpClient.delete(`/Leaves/Types/${id}`),
};
