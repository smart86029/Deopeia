namespace Deopeia.Quote.Application.Instruments.GetInstrument;

public record GetInstrumentQuery(string IdOrSymbol) : IRequest<GetInstrumentViewModel> { }
