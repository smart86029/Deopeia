import httpClient from '../http-client';

export interface Authenticator {
  isEnabled: boolean;
  qrCodeImageUrl: string;
  manualEntryKey: string;
}

export interface Profile {
  name: string;
  avatarUrl: string;
}

export interface ChangePasswordCommand {
  currentPassword: string;
  newPassword: string;
}

export const meApi = {
  getAuthenticator: () => httpClient.get<Authenticator>(`/Me/2fa`),
  enableAuthenticator: (verificationCode: string) =>
    httpClient.put(`/Me/2fa`, { verificationCode }),
  getProfile: () => httpClient.get<Profile>(`/Me/Profile`).then((response) => response.data),
  uploadAvatar: (file: File) => httpClient.putForm(`/Me/Avatar`, { file }),
  changePassword: (command: ChangePasswordCommand) => httpClient.put(`/Me/Password`, command),
};
