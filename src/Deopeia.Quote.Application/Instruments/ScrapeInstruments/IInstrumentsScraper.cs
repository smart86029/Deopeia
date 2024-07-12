namespace Deopeia.Quote.Application.Instruments.ScrapeInstruments;

public interface IInstrumentsScraper
{
    Task<ICollection<InstrumentDto>> GetInstrumentsAsync();
}
