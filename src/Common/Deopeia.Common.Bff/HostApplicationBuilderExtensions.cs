using System.Globalization;
using System.Net.Mime;
using System.Text.Encodings.Web;
using Deopeia.Common.Bff.OpenApi;
using Deopeia.Common.Contracts;
using Google.Protobuf.Collections;
using Mapster;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
        services.AddAuthentication(configuration);
        services.AddAuthorization();
        services.AddOpenApi(options =>
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>()
        );
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
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            );
    }

    private static void AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:7058";
                options.ClientId = configuration.GetValue<string>("OpenIdConnect:ClientId");
                options.ClientSecret = "secret";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;

                options.Events.OnRedirectToIdentityProvider = context =>
                {
                    if (IsApiRequest(context.HttpContext.Request))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }

                    return Task.CompletedTask;
                };
            });

        services.AddSingleton<CookieOidcRefresher>();
        services
            .AddOptions<CookieAuthenticationOptions>()
            .Configure<CookieOidcRefresher>(
                (options, refresher) =>
                    options.Events.OnValidatePrincipal = context =>
                        refresher.ValidateOrRefreshCookieAsync(
                            context,
                            OpenIdConnectDefaults.AuthenticationScheme
                        )
            );
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

        TypeAdapterConfig<decimal, DecimalValue>.NewConfig().MapWith(src => (DecimalValue)src);
        TypeAdapterConfig<DecimalValue, decimal>.NewConfig().MapWith(src => (decimal)src);
        TypeAdapterConfig<Guid, Uuid>.NewConfig().MapWith(src => (Uuid)src);
        TypeAdapterConfig<Uuid, Guid>.NewConfig().MapWith(src => (Guid)src);
    }

    private static bool IsApiRequest(HttpRequest request)
    {
        if (request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (
            request.Headers.TryGetValue("X-Requested-With", out var xrw)
            && string.Equals(xrw.ToString(), "XMLHttpRequest", StringComparison.OrdinalIgnoreCase)
        )
        {
            return true;
        }

        if (
            request.Headers.TryGetValue("Accept", out var accept)
            && accept
                .ToString()
                .Contains(MediaTypeNames.Application.Json, StringComparison.OrdinalIgnoreCase)
        )
        {
            return true;
        }

        return false;
    }
}
