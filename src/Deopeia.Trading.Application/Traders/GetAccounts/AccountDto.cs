namespace Deopeia.Trading.Application.Traders.GetAccounts;

public class AccountDto
{
    public string CurrencyCode { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public decimal ExchangeRate { get; set; }
}
