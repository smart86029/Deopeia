namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

public class GetFuturesContractViewModel
{
    public Guid Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public DateOnly ExpirationDate { get; set; }

    public string Exchange { get; set; } = string.Empty;

    public string UnderlyingAsset { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal TickSize { get; set; }

    public decimal ContractSizeQuantity { get; set; }

    public string ContractSizeUnit { get; set; } = string.Empty;

    public ICollection<FuturesContractLocaleDto> Locales { get; set; } = [];
}
