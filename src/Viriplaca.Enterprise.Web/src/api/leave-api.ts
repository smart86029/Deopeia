import httpClient from './http-client';

export interface Leave {
  type: number;
}

export default {
  getList: (account: string) => {
    return httpClient.get<Leave>('/Leave', {
      params: { account },
    });
  },
};
