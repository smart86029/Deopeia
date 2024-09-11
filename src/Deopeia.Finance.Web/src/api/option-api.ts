import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export default {
  getCultures: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Cultures'),
  getTimeZones: () =>
    httpClient.get<OptionResult<string>[]>('/Options/TimeZones'),
};
