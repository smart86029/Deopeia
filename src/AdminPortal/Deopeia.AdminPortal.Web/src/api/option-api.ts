import type { OptionResult } from '@/models/option-result';
import httpClient from './http-client';

export const optionApi = {
  getCultures: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Cultures').then((response) => response.data),
  getCurrencies: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Currencies').then((response) => response.data),
  getTimeZones: () =>
    httpClient.get<OptionResult<string>[]>('/Options/TimeZones').then((response) => response.data),
  getUnits: () =>
    httpClient.get<OptionResult<string>[]>('/Options/Units').then((response) => response.data),
};
