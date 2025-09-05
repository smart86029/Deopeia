namespace Deopeia.AppHost;

public static class ResourceBuilderExtensions
{
    private static IResourceBuilder<MinIOResource>? s_minIO;
    private static IResourceBuilder<ParameterResource>? s_minIOAccessKey;
    private static IResourceBuilder<ParameterResource>? s_minIOSecretKey;
    private static IResourceBuilder<KafkaServerResource>? s_kafka;

    public static IResourceBuilder<TDestination> WithS3<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        s_minIO ??= builder.ApplicationBuilder.AddMinIO("minio").WithDataVolume();
        s_minIOAccessKey ??= builder.ApplicationBuilder.AddParameter("minio-access-key");
        s_minIOSecretKey ??= builder.ApplicationBuilder.AddParameter("minio-secret-key");

        var parts = builder.Resource.Name.Split('-');
        var bucketName = parts.Length > 2 ? string.Join("-", parts.Take(2)) : builder.Resource.Name;

        return builder
            .WithReferenceAndWaitFor(s_minIO)
            .WithEnvironment("S3__BucketName", bucketName)
            .WithEnvironment("S3__ServiceUrl", s_minIO.GetEndpoint("api"))
            .WithEnvironment("S3__AccessKeyId", s_minIOAccessKey)
            .WithEnvironment("S3__SecretAccessKey", s_minIOSecretKey);
    }

    public static IResourceBuilder<TDestination> WithKafka<TDestination>(
        this IResourceBuilder<TDestination> builder
    )
        where TDestination : ProjectResource
    {
        s_kafka ??= builder
            .ApplicationBuilder.AddKafka("kafka")
            .WithKafkaUI(x => x.WithHostPort(9100))
            .WithDataVolume();

        return builder.WithReferenceAndWaitFor(s_kafka);
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
