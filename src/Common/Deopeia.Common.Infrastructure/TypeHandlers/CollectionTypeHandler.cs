namespace Deopeia.Common.Infrastructure.TypeHandlers;

internal class CollectionTypeHandler<TItem> : SqlMapper.TypeHandler<ICollection<TItem>>
{
    public override void SetValue(IDbDataParameter parameter, ICollection<TItem>? items)
    {
        parameter.Value = items?.ToJson();
    }

    public override ICollection<TItem>? Parse(object value)
    {
        return value.ToString()?.ToObject<List<TItem>>() ?? [];
    }
}
