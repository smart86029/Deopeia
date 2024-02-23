using Viriplaca.HR.App.Leaves.GetLeaves;

namespace Viriplaca.HR.Data.Leaves.GetLeaves;

public class GetLeavesQueryHandler : IRequestHandler<GetLeavesQuery, PageResult<LeaveDto>>
{
    public Task<PageResult<LeaveDto>> Handle(GetLeavesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}