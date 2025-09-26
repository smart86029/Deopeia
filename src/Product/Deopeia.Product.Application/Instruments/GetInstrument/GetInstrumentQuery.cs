namespace Deopeia.Product.Application.Instruments.GetInstrument;

public sealed record GetInstrumentQuery(Guid Id) : IQuery<GetInstrumentResult>;
