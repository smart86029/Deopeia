import httpClient from '../http-client';

export interface HistoricalData {
  symbol?: string;
  quotes: Candle[];
}

export interface Candle {
  date: Date;
  open: number;
  high: number;
  low: number;
  close: number;
  volume: number;
}

export default {
  getHistory: (symbol: string) =>
    httpClient.get<HistoricalData>(`/Candles/${symbol}/history`),
};
