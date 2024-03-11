using Microsoft.Extensions.Localization;
using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.GetLeaveTypes;

public class GetLeaveTypesQueryHandler(IStringLocalizer localizer)
    : EnumOptionsQueryHandler<GetLeaveTypesQuery, LeaveType>(localizer)
{
}
