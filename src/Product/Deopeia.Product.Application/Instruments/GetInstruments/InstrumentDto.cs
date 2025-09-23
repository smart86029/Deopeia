using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Application.Instruments.GetInstruments;

public class InstrumentDto
{
    public Guid Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public InstrumentType Type { get; set; }
}
