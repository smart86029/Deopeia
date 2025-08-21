export interface Candle {
  timestamp: Date;
  open: number;
  high: number;
  low: number;
  close: number;
  volume: number;
}

export interface TimeFrameMap {
  [key: number]: Candle[];
}

export interface CandleMap {
  [key: string]: TimeFrameMap;
}
