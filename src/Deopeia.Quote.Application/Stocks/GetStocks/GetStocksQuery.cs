using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Application.Stocks.GetStocks;

public record GetStocksQuery(Industry? Industry) : PageQuery<StockDto> { }
