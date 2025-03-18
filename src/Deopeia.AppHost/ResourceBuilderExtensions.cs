namespace Deopeia.AppHost;

public static class ResourceBuilderExtensions
{
    private static IResourceBuilder<ParameterResource>? _jwtKey;
    private static IResourceBuilder<ParameterResource>? _jwtIssuer;
    private static IResourceBuilder<ParameterResource>? _minIOEndpoint;
    private static IResourceBuilder<ParameterResource>? _minIOAccessKey;
    private static IResourceBuilder<ParameterResource>? _minIOSecretKey;
    private static IResourceBuilder<KafkaServerResource>? _kafka;

    public static IResourceBuilder<TDestination> WithJwt<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        _jwtKey ??= builder.ApplicationBuilder.AddParameter("JwtKey");
        _jwtIssuer ??= builder.ApplicationBuilder.AddParameter("JwtIssuer");

        return builder
            .WithEnvironment("Jwt__Key", _jwtKey)
            .WithEnvironment("Jwt__Issuer", _jwtIssuer);
    }

    public static IResourceBuilder<TDestination> WithMinIO<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        _minIOEndpoint ??= builder.ApplicationBuilder.AddParameter("MinIOEndpoint");
        _minIOAccessKey ??= builder.ApplicationBuilder.AddParameter("MinIOAccessKey");
        _minIOSecretKey ??= builder.ApplicationBuilder.AddParameter("MinIOSecretKey");

        return builder
            .WithEnvironment("MinIO__Endpoint", _minIOEndpoint)
            .WithEnvironment("MinIO__AccessKey", _minIOAccessKey)
            .WithEnvironment("MinIO__SecretKey", _minIOSecretKey);
    }

    public static IResourceBuilder<TDestination> WithKafka<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        if (_kafka is null)
        {
            _kafka = builder
                .ApplicationBuilder.AddKafka("kafka")
                .WithKafkaUI(x => x.WithHostPort(9100))
                .WithDataVolume();
        }

        return builder.WithReferenceAndWaitFor(_kafka);
    }

    public static IResourceBuilder<TDestination> WithProxyEndpoint<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        return builder.WithEnvironment("Proxy", builder.GetEndpoint("http"));
    }

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
