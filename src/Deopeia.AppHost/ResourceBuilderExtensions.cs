namespace Deopeia.AppHost;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<TDestination> WithReferenceAndWaitFor<TDestination>(
        this IResourceBuilder<TDestination> builder,
        IResourceBuilder<IResourceWithConnectionString> source,
        string? connectionName = null,
        bool optional = false
    )
        where TDestination : ProjectResource
    {
        return builder.WithReference(source, connectionName, optional).WaitFor(source);
    }
}
