import httpClient from '../http-client';

export interface Authenticator {
  isBound: boolean;
  imageUrl: string;
  manualEntryKey: string;
}

export const authenticatorApi = {
  get: () => httpClient.get<Authenticator>(`/Me/Authenticator`),
  bind: (authenticationCode: string) =>
    httpClient.put(`/Me/Authenticator`, { authenticationCode }),
};
