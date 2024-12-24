import type { Instrument } from '@/models/trading/instrument';
import httpClient from '../http-client';

export default {
  get: (symbol: string) => httpClient.get<Instrument>(`/Instruments/${symbol}`),
};
