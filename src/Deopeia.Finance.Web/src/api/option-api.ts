import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export const optionApi = {
  getCultures: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Cultures'),
  getCurrencies: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Currencies'),
  getTimeZones: () =>
    httpClient.get<OptionResult<string>[]>('/Options/TimeZones'),
  getUnits: () => httpClient.get<OptionResult<string>[]>('/Options/Units'),
};
