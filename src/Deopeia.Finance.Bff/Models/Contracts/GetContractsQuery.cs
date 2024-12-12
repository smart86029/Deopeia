namespace Deopeia.Finance.Bff.Models.Contracts;

public record GetContractsQuery(UnderlyingType? UnderlyingType) : PageQuery { }
