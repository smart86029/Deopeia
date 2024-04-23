using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace Viriplaca.Common.Data.Converters;

internal class TypeConverter()
    : ValueConverter<Type, string?>(
        type => type.FullName,
        typeName => GetType(typeName)!)
{
    private static Type? GetType(string? typeName)
    {
        if (typeName.IsNullOrWhiteSpace())
        {
            return null;
        }

        var type = Type.GetType(typeName);
        if (type is not null)
        {
            return type;
        }

        var assemblies = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Select(Assembly.Load);
        foreach (var assembly in assemblies)
        {
            type = assembly.GetType(typeName);
            if (type is not null)
            {
                return type;
            }
        }

        return null;
    }
}
