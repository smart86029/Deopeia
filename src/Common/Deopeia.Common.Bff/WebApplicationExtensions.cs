using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace Deopeia.Common.Bff;

public static class WebApplicationExtensions
{
    public static WebApplication MapScalar(this WebApplication app)
    {
        if (app.Environment.IsProduction())
        {
            return app;
        }

        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .AddPreferredSecuritySchemes("Basic")
                .AddHttpAuthentication(
                    "Basic",
                    auth =>
                    {
                        auth.Username = "admin1";
                        auth.Password = "123fff";
                    }
                );
        });

        return app;
    }
}
