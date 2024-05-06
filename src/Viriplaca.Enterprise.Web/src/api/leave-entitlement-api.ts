import type { Guid } from '@/models/guid';
import httpClient from './http-client';

export interface LeaveEntitlement {
  id: Guid;
  type: number;
  startedOn: Date;
  endedOn: Date;
  reason: string;
  availableHours: number;
  usedHours: number;
}

export default {
  getList: () => httpClient.get<LeaveEntitlement[]>('/LeaveEntitlements'),
};
