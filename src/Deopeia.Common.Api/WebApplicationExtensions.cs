using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

namespace Deopeia.Common.Api;

public static class WebApplicationExtensions
{
    public static WebApplication UseScalar(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
            options.Servers = [new ScalarServer(app.Configuration.GetValue<string>("Proxy")!)]
        );

        return app;
    }
}
