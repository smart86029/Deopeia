import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export default {
  getOptions: () =>
    httpClient.get<OptionResult<number>[]>('/Industries/Options'),
};
