import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export default {
  getTimeZones: () =>
    httpClient.get<OptionResult<string>[]>('/Options/TimeZones'),
};
