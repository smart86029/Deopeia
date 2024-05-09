import type { Guid } from '@/models/guid';
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

export interface WorkingTime {
  amount: number;
  days: number;
  hours: number;
}

export interface LeaveType {
  id: Guid;
  name: string;
  description: string;
}

export default {
  getList: () => httpClient.get<LeaveEntitlement[]>('/LeaveEntitlements'),
};
