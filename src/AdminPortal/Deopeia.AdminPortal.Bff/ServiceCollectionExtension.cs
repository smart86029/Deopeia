namespace Deopeia.AdminPortal.Bff;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGrpcIdentity<TClient>(this IServiceCollection services)
        where TClient : class
    {
        services
            .AddGrpcClient<TClient>(options =>
            {
                options.Address = new Uri("http://deopeia-identity-api");
            })
            .ConfigureHttpClient(options =>
            {
                options.DefaultRequestHeaders.AcceptLanguage.Clear();
                options.DefaultRequestHeaders.AcceptLanguage.ParseAdd(
                    CultureInfo.CurrentCulture.Name
                );
            });
        return services;
    }
}
