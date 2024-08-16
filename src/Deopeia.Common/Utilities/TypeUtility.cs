using System.Reflection;

namespace Deopeia.Common.Utilities;

public static class TypeUtility
{
    public static IEnumerable<Type> GetTypes(string assemblyName)
    {
        var results = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia.") && x.Name.EndsWith(assemblyName))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);

        return results;
    }
}
