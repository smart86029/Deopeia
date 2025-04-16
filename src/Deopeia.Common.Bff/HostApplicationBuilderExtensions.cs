using System.Text;
using Deopeia.Common.Bff;
using Deopeia.Common.Bff.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Deopeia.Common.Bff;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddBff(this IHostApplicationBuilder builder)
    {
        var jwtOptions = new JwtOptions();
        builder.Configuration.Bind("Jwt", jwtOptions);

        var services = builder.Services;
        services.AddOptions<ConnectionStringOptions>().BindConfiguration("ConnectionStrings");
        services.AddControllers();
        services.AddAuthentication(jwtOptions);
        services.AddOpenApi();
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionHandler>();

        services.AddHttpContextAccessor();
        services.AddScoped(x =>
        {
            var httpContextAccessor = x.GetRequiredService<IHttpContextAccessor>();
            var context = httpContextAccessor.HttpContext;
            if (context is null)
            {
                return new CurrentUser();
            }

            return new CurrentUser(context.User.GetUserId(), context.Request.GetAddress());
        });

        return builder;
    }

    public static AuthenticationBuilder AddAuthentication(
        this IServiceCollection services,
        JwtOptions jwtOptions
    )
    {
        var builder = services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7099/";
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Key)
                    ),
                };

                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                };
                options.Backchannel = new HttpClient(handler);
            });

        return builder;
    }
}
