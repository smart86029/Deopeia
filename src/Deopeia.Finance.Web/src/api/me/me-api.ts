import httpClient from '../http-client';

export interface Authenticator {
  isEnabled: boolean;
  imageUrl: string;
  manualEntryKey: string;
}

export interface ChangePasswordCommand {
  currentPassword: string;
  newPassword: string;
}

export const meApi = {
  getAuthenticator: () => httpClient.get<Authenticator>(`/Me/Authenticator`),
  enableAuthenticator: (verificationCode: string) =>
    httpClient.put(`/Me/Authenticator`, { verificationCode }),
  uploadAvatar: (file: File) => httpClient.putForm(`/Me/Avatar`, { file }),
  changePassword: (command: ChangePasswordCommand) =>
    httpClient.put(`/Me/Password`, command),
};
