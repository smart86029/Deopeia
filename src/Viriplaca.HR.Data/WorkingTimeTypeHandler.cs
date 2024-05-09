namespace Viriplaca.HR.Data;

internal class WorkingTimeTypeHandler : SqlMapper.TypeHandler<WorkingTime>
{
    public override void SetValue(IDbDataParameter parameter, WorkingTime workingTime)
    {
        parameter.Value = workingTime.Amount;
    }

    public override WorkingTime Parse(object value)
    {
        return new WorkingTime(value.ToDecimal());
    }
}
