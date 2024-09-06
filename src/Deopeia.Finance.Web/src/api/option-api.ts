import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export default {
  getCultures: () =>
    httpClient.get<OptionResult<string>[]>('/Quote/Options/Cultures'),
  getTimeZones: () =>
    httpClient.get<OptionResult<string>[]>('/Quote/Options/TimeZones'),
};
