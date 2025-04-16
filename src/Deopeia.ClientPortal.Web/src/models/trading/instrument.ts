import type { Session } from './session';

export interface Instrument {
  symbol: string;
  name: string;
  currencyCode: string;
  pricePrecision: number;
  tickSize: number;
  contractSize: ContractSize;
  volumeRestriction: VolumeRestriction;
  sessions: Session[];
  leverages: number[];
}

export interface ContractSize {
  quantity: number;
  unitCode: string;
}

export interface VolumeRestriction {
  min: number;
  max: number;
  step: number;
}

export interface InstrumentMap {
  [key: string]: Instrument;
}
