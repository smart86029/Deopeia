namespace Deopeia.Trading.Application.Traders.GetTraders;

public class TraderDto
{
    public Guid Id { get; set; }

    public bool IsEnabled { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
