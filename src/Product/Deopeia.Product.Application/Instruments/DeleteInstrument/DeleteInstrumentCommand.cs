namespace Deopeia.Product.Application.Instruments.DeleteInstrument;

public sealed record DeleteInstrumentCommand(Guid Id) : ICommand;
