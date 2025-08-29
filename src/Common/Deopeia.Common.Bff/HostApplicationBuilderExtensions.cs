using System.Globalization;
using System.Text.Encodings.Web;
using Deopeia.Common.Bff.OpenApi;
using Google.Protobuf.Collections;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Bff;

public static class HostApplicationBuilderExtensions
{
    private static readonly string[] SupportedCultures = ["en", "zh-Hant"];

    public static IHostApplicationBuilder AddBff(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        var services = builder.Services;

        services.AddControllers();
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        services.AddProblemDetails();

        services.ConfigureLocalization();
        ConfigureMapster();

        return builder;
    }

    private static void AddControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options => { })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
    }

    private static void ConfigureLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
            options.SetDefaultCulture(SupportedCultures[0]).AddSupportedCultures(SupportedCultures)
        );

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en");
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
