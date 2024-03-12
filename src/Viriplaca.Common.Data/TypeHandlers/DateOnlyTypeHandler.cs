namespace Viriplaca.Common.Data.TypeHandlers;

internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
    {
        parameter.Value = date.ToDateTime();
    }

    public override DateOnly Parse(object value)
    {
        return ((DateTime)value).ToDateOnly();
    }
}
