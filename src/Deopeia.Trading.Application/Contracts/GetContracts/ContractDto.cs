namespace Deopeia.Trading.Application.Contracts.GetContracts;

public class ContractDto
{
    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public UnderlyingType UnderlyingType { get; set; }
}
