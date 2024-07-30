export interface RealTimeQuote {
  symbol: string;
  lastTradedAt: Date;
  lastTradedPrice: number;
  previousClose: number;
}
