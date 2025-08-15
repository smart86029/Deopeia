namespace Deopeia.AppHost;

internal static class MinIOResourceBuilderExtensions
{
    internal static IResourceBuilder<MinIOResource> AddMinIO(
        this IDistributedApplicationBuilder builder,
        [ResourceName] string name,
        int? apiPort = 9000,
        int? consolePort = 9001
    )
    {
        var minIO = new MinIOResource(name);

        return builder
            .AddResource(minIO)
            .WithImage("minio/minio", "latest")
            .WithImageRegistry("docker.io")
            .WithHttpEndpoint(
                port: apiPort,
                targetPort: MinIOResource.DefaultApiPort,
                name: MinIOResource.ApiEndpointName
            )
            .WithEndpoint(
                port: consolePort,
                targetPort: MinIOResource.DefaultConsolePort,
                name: MinIOResource.ConsoleEndpointName
            )
            .WithEnvironment("MINIO_ROOT_USER", "minioadmin")
            .WithEnvironment("MINIO_ROOT_PASSWORD", "minioadmin")
            .WithArgs(
                "server",
                "/data",
                "--console-address",
                $":{MinIOResource.DefaultConsolePort}"
            );
    }

    internal static IResourceBuilder<MinIOResource> WithDataVolume(
        this IResourceBuilder<MinIOResource> builder,
        string? name = null
    )
    {
        builder.WithVolume(name ?? VolumeNameGenerator.Generate(builder, "data"), "/data");

        return builder;
    }
}
