using Deopeia.Common.Domain.Files;

namespace Deopeia.Common.Infrastructure.Files;

internal class FileResourceIdConverter()
    : ValueConverter<FileResourceId, Guid>(id => id.Guid, guid => new FileResourceId(guid)) { }
