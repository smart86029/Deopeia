namespace Deopeia.Common.Infrastructure.Dapper;

internal class CultureInfoTypeHandler : SqlMapper.TypeHandler<CultureInfo>
{
    public override void SetValue(IDbDataParameter parameter, CultureInfo? culture)
    {
        parameter.Value = culture?.Name;
    }

    public override CultureInfo? Parse(object value)
    {
        return value is null || value.ToString().IsNullOrWhiteSpace()
            ? null
            : CultureInfo.GetCultureInfo(value.ToString()!);
    }
}
