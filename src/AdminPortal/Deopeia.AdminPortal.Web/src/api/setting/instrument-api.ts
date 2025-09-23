import httpClient from '../http-client';

export interface GetInstrumentsRequest extends PagedRequest {
  keyword?: string;
  type?: number;
}

export interface InstrumentRow {
  symbol: string;
  name: string;
  currency: string;
  underlyingType: string;
}

export interface Instrument {
  symbol: string;
  underlyingType: number;
  currencyCode: string;
  pricePrecision: number;
  tickSize: number;
  instrumentSizeQuantity: number;
  instrumentSizeUnitCode: string;
  leverages: string[];
  sessions: Session[];
  locales: InstrumentLocale[];
}

export interface InstrumentLocale {
  culture: string;
  name: string;
  description?: string;
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
  get: (symbol: string) => httpClient.get<Instrument>(`/Instruments/${symbol}`),
  create: (instrument: Instrument) => httpClient.post('/Instruments', instrument),
  update: (instrument: Instrument) =>
    httpClient.put(`/Instruments/${instrument.symbol}`, instrument),
};
