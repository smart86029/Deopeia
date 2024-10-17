using System.Reflection;

namespace Deopeia.Common.Utilities;

public static class AssemblyUtility
{
    public static string ServiceName => Assembly.GetEntryAssembly()!.FullName!.Split('.')[1];

    public static IEnumerable<Assembly> GetAssemblies()
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia."))
            .Select(Assembly.Load);
    }

    public static IEnumerable<Type> GetTypes()
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia."))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);
    }

    public static IEnumerable<Type> GetTypes(string assemblyName)
    {
        return Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia.") && x.Name.EndsWith(assemblyName))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);
    }
}
