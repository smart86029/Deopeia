import type { Image } from '@/models/image';
import httpClient from './http-client';

export interface UploadImageCommand {
  file: File;
}

export default {
  upload: (command: UploadImageCommand) =>
    httpClient.postForm<Image>('/Images', command),
};
