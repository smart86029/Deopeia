using System.ComponentModel.DataAnnotations;

namespace Deopeia.Common.Extensions;

public static class TypeExtensions
{
    public static bool IsAssignableToGenericType(this Type type, Type genericType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        if (
            type.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType)
        )
        {
            return true;
        }

        return type.BaseType?.IsAssignableToGenericType(genericType) ?? false;
    }

    public static string GetDisplayName(this Type type)
    {
        var attributeType = typeof(DisplayAttribute);
        var attributes = (DisplayAttribute[])type.GetCustomAttributes(attributeType, false);
        if (attributes is not null && attributes.Length > 0)
        {
            return attributes[0].Name!;
        }

        return type.Name;
    }
}
