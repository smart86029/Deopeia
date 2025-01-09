namespace Deopeia.Trading.Application.Traders.GetTrader;

public class GetTraderViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;
}
