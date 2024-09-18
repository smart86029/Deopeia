namespace Deopeia.Quote.Application.Exchanges;

public class ExchangeLocaleDto
{
    public string Culture { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Abbreviation { get; set; }
}
