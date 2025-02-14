using System.Reflection;

namespace Deopeia.Common.Utilities;

public static class AssemblyUtility
{
    private const string Prefix = "Deopeia.";

    public static string ServiceName => Assembly.GetEntryAssembly()!.FullName!.Split('.')[1];

    public static IEnumerable<Assembly> GetAssemblies()
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith(Prefix))
            .Select(Assembly.Load);
    }

    public static IEnumerable<Type> GetTypes()
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith(Prefix))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);
    }

    public static IEnumerable<Type> GetTypes(string assemblyName)
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith(Prefix) && x.Name.EndsWith(assemblyName))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);
    }

    public static Type? GetType(string typeName)
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith(Prefix))
            .Select(Assembly.Load)
            .Select(x => x.GetType(typeName))
            .FirstOrDefault(x => x is not null);
    }
}
