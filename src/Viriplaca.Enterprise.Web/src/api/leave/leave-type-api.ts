import type { Guid } from '@/models/guid';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './../http-client';

export interface GetLeaveTypesQuery extends PageQuery {}

export interface LeaveTypeRow {
  id: Guid;
  name: string;
  description: string;
  canCarryForward: boolean;
}

export interface LeaveType {
  id: Guid;
  canCarryForward: boolean;
  locales: LeaveTypeLocale[];
}

export interface LeaveTypeLocale {
  culture: string;
  name: string;
  description: string;
}

export default {
  getList: (query: GetLeaveTypesQuery) =>
    httpClient.get<PageResult<LeaveTypeRow>>('/LeaveTypes', { params: query }),
  get: (id: Guid) => httpClient.get<LeaveType>(`/LeaveTypes/${id}`),
  create: (leaveType: LeaveType) => httpClient.post('/LeaveTypes', leaveType),
  update: (leaveType: LeaveType) =>
    httpClient.put(`/LeaveTypes/${leaveType.id}`, leaveType),
};
