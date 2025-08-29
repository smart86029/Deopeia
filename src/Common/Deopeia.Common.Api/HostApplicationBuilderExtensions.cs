using System.Globalization;
using Deopeia.Common.Contracts;
using Google.Protobuf.Collections;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Api;

public static class HostApplicationBuilderExtensions
{
    private static readonly string[] SupportedCultures = ["en", "zh-Hant"];

    public static IHostApplicationBuilder AddApi(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.ConfigureLocalization();
        ConfigureMapster();

        return builder;
    }

    private static void ConfigureLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
            options.SetDefaultCulture(SupportedCultures[0]).AddSupportedCultures(SupportedCultures)
        );

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    private static void ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
        TypeAdapterConfig.GlobalSettings.Default.UseDestinationValue(member =>
            member.SetterModifier == AccessModifier.None
            && member.Type.IsGenericType
            && member.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>)
        );
    }
}
