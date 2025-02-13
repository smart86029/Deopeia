using Deopeia.Service.Application.Module.Name;

namespace Deopeia.Service.Infrastructure.Module.Name;

internal class QueryHandler : IRequestHandler<Query, QueryResult>
{
    public override Task<QueryResult> Handle(Query request, CancellationToken cancellationToken) { }
}
