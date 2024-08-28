using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Application.Industries.GetIndustryOptions;

internal class GetIndustryOptionsQueryHandler(IStringLocalizer stringLocalizer)
    : EnumOptionsQueryHandler<GetIndustryOptionsQuery, Industry>(stringLocalizer) { }
