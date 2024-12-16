import type { Candle } from '@/models/quote/candle';
import type { TimeFrame } from '@/models/quote/time-frame';
import httpClient from '../http-client';

export default {
  getHistory: (symbol: string, timeFrame: TimeFrame, startedAt?: number) =>
    httpClient.get<Candle[]>(`/Candles/${symbol}/history`, {
      params: {
        timeFrame,
        startedAt,
      },
    }),
};
