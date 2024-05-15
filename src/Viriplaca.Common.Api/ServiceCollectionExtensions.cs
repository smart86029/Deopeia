using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Viriplaca.Common.Api.Extensions;
using Viriplaca.Common.Domain;

namespace Viriplaca.Common.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>().BindConfiguration("Jwt");
        services.AddOptions<ConnectionStringOptions>().BindConfiguration("ConnectionStrings");
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

        var assemblies = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Viriplaca."))
            .Select(Assembly.Load)
            .ToArray();
        services.AddMediatR(options => options.RegisterServicesFromAssemblies(assemblies));

        return services;
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
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Key)
                    ),
                };
            });

        return builder;
    }
}
