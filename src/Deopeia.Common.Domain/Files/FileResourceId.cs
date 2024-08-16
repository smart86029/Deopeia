namespace Deopeia.Common.Domain.Files;

public readonly record struct FileResourceId(Guid Guid) : IEntityId
{
    public FileResourceId()
        : this(GuidUtility.NewGuid()) { }
}
