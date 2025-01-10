namespace Deopeia.Trading.Application.Traders.GetTraders;

public class TraderDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public decimal Balance { get; set; }
}
