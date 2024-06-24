namespace Deopeia.Common.Infrastructure.TypeHandlers;

internal class CultureInfoTypeHandler : SqlMapper.TypeHandler<CultureInfo>
{
    public override void SetValue(IDbDataParameter parameter, CultureInfo culture)
    {
        parameter.Value = culture.Name;
    }

    public override CultureInfo Parse(object value)
    {
        return CultureInfo.GetCultureInfo(value.ToString()!);
    }
}
