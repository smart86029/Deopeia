namespace Deopeia.Common.Domain.Finance;

public readonly record struct AssetCode : IEntityId
{
    public AssetCode(string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("Asset code required.", nameof(value));
        }

        Value = value.Trim().ToUpperInvariant();
    }

    public string Value { get; private init; }

    public override string ToString() => Value;
}
