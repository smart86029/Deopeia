namespace Deopeia.Trading.Application.Traders.GetFavorites;

public record GetFavoritesQuery(Guid TraderId) : IRequest<ICollection<string>> { }
