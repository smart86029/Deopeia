import type { PageResult } from '@/models/page';
import httpClient from './http-client';

export interface Leave {
  type: number;
}

export default {
  getList: (account: string) => {
    return httpClient.get<PageResult<Leave>>('/Leaves', {
      params: { account },
    });
  },
};
