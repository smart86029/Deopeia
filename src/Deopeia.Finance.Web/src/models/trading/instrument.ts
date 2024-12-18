export interface Instrument {
  symbol: string;
  name: string;
  currencyCode: string;
}

export interface InstrumentMap {
  [key: string]: Instrument;
}
