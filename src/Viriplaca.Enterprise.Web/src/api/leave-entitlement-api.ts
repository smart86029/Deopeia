import type { Guid } from '@/models/guid';
import type { WorkingTime } from '@/models/leave/working-time';
import httpClient from './http-client';

export interface LeaveEntitlement {
  id: Guid;
  startedOn: Date;
  endedOn: Date;
  reason: string;
  grantedTime: WorkingTime;
  usedTime: WorkingTime;
  leaveType: LeaveType;
}

export interface LeaveType {
  id: Guid;
  name: string;
  description: string;
}

export default {
  getList: () => httpClient.get<LeaveEntitlement[]>('/LeaveEntitlements'),
};
