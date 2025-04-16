import type { Instrument } from '@/models/trading/instrument';
import httpClient from '../http-client';

export const instrumentApi = {
  get: (symbol: string) => httpClient.get<Instrument>(`/Instruments/${symbol}`),
};
