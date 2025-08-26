using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Api;

public static class HostApplicationBuilderExtensions
{
    private static readonly string[] SupportedCultures = ["en-US", "zh-TW"];

    public static IHostApplicationBuilder AddApi(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.Configure<RequestLocalizationOptions>(options =>
            options.SetDefaultCulture(SupportedCultures[0]).AddSupportedCultures(SupportedCultures)
        );

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

        return builder;
    }
}
