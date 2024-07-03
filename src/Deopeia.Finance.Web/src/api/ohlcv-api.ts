import httpClient from './http-client';

export interface HistoricalData {
  symbol?: string;
  quotes: Ohlcv[];
}

export interface Ohlcv {
  date: string;
  open: number;
  high: number;
  low: number;
  close: number;
  volume: number;
}

export default {
  getHistory: (symbol: string) =>
    httpClient.get<HistoricalData>(`/Ohlcvs/${symbol}/history`),
};
