using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Infrastructure.Companies;

internal class CompanyIdConverter()
    : ValueConverter<CompanyId, Guid>(id => id.Guid, guid => new CompanyId(guid)) { }
