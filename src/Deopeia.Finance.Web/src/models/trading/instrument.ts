export interface Instrument {
  symbol: string;
  name: string;
  currencyCode: string;
  pricePrecision: number;
  tickSize: number;
  contractSizeQuantity: number;
  contractSizeUnitCode: string;
}

export interface InstrumentMap {
  [key: string]: Instrument;
}
