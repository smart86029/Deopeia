import type { Guid } from '@/models/guid';
import httpClient from '../http-client';

export interface GetInstrumentsRequest extends PagedRequest {
  keyword?: string;
  type?: number;
}

export interface InstrumentRow {
  id: Guid;
  symbol: string;
  name: string;
  type: InstrumentType;
}

export interface Instrument {
  id: Guid;
  type: InstrumentType;
  symbol: string;
  baseAsset: string;
  quoteAsset: string;
  pricePrecision: number;
  quantityPrecision: number;
  minQuantity: number;
  minNotional: number;
  localizations: InstrumentLocalization[];
}

export interface InstrumentLocalization {
  culture: string;
  name: string;
}

export const instrumentApi = {
  getOptions: (type: InstrumentType) =>
    httpClient.get<OptionResult<string>[]>('/Instruments/Options', {
      params: { type },
    }),
  getList: (request: GetInstrumentsRequest) =>
    httpClient
      .get<PagedResponse<InstrumentRow>>('/Instruments', {
        params: request,
      })
      .then((response) => response.data),
  get: (id: Guid) =>
    httpClient.get<Instrument>(`/Instruments/${id}`).then((response) => response.data),
  create: (instrument: Instrument) => httpClient.post('/Instruments', instrument),
  update: (instrument: Instrument) => httpClient.put(`/Instruments/${instrument.id}`, instrument),
};
