namespace Deopeia.Quote.Application.Instruments.GetInstrument;

public record GetInstrumentQuery(string Symbol) : IRequest<GetInstrumentViewModel> { }
