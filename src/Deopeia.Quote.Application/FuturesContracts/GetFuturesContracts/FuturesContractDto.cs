namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContracts;

public class FuturesContractDto
{
    public Guid Id { get; set; }

    public Guid ExchangeId { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public Guid UnderlyingAssetId { get; set; }
}
