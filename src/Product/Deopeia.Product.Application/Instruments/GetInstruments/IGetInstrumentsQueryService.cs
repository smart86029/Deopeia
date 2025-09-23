namespace Deopeia.Product.Application.Instruments.GetInstruments;

public interface IGetInstrumentsQueryService
{
    Task<PagedResult<InstrumentDto>> GetAsync(GetInstrumentsQuery query);
}
