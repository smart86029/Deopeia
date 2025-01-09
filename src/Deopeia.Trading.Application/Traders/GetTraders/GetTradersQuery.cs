namespace Deopeia.Trading.Application.Traders.GetTraders;

public record GetTradersQuery(bool? IsEnabled) : PageQuery<TraderDto> { }
