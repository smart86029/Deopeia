namespace Deopeia.Finance.Bff.Models.Markets;

public class Contract
{
    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsFavorite { get; set; }
}
