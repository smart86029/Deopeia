namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContracts;

public class FuturesContractDto
{
    public Guid Id { get; set; }

    public string Exchange { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public string UnderlyingAsset { get; set; } = string.Empty;
}
