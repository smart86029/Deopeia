namespace Deopeia.Finance.Bff.Models.Trading;

public class Account
{
    public string CurrencyCode { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public decimal Available { get; set; }

    public decimal Frozen { get; set; }

    public decimal ExchangeRate { get; set; }
}
