using System.Reflection;

namespace Deopeia.Common.Utilities;

public static class AssemblyUtility
{
    private const string Prefix = "Deopeia.";

    public static string ServiceName => Assembly.GetEntryAssembly()!.FullName!.Split('.')[1];

    public static IReadOnlyList<Assembly> GetAssemblies()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
        {
            return [];
        }

        var loaded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var toLoad = new Queue<AssemblyName>(entryAssembly.GetReferencedAssemblies());
        var result = new List<Assembly> { entryAssembly };

        loaded.Add(entryAssembly.FullName!);

        while (toLoad.TryDequeue(out var assemblyName))
        {
            if (
                assemblyName.Name is null
                || !assemblyName.Name.StartsWith(Prefix, StringComparison.Ordinal)
            )
            {
                continue;
            }

            if (!loaded.Add(assemblyName.FullName!))
            {
                continue;
            }

            try
            {
                var assembly = Assembly.Load(assemblyName);
                result.Add(assembly);

                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                {
                    toLoad.Enqueue(referencedAssembly);
                }
            }
            catch
            {
                continue;
            }
        }

        return result;
    }

    public static IEnumerable<Type> GetTypes()
    {
        var assemblies = GetAssemblies();
        return assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType);
    }

    public static Type? GetType(string typeName)
    {
        var a = GetAssemblies();
        return a.SelectMany(x => x.GetTypes())
            .FirstOrDefault(x => x.Name == typeName || x.FullName == typeName);
    }
}
