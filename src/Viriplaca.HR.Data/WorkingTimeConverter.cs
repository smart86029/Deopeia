using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Viriplaca.Common.Data.Converters;

internal class WorkingTimeConverter()
    : ValueConverter<WorkingTime, decimal>(
        workingTime => workingTime.Amount,
        amount => new WorkingTime(amount))
{
}
