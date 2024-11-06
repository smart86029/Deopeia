namespace Deopeia.Finance.Bff.Models.Quotes;

public class GetInstrumentViewModel
{
    public Guid Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
