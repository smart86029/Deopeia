import httpClient from '../http-client';

export interface Authenticator {
  isEnabled: boolean;
  imageUrl: string;
  manualEntryKey: string;
}

export const authenticatorApi = {
  get: () => httpClient.get<Authenticator>(`/Me/Authenticator`),
  enable: (verificationCode: string) =>
    httpClient.put(`/Me/Authenticator`, { verificationCode }),
};
