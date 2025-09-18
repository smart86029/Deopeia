using Deopeia.Common.Api;
using Deopeia.Product.Api.Services;
using Deopeia.Product.Application;
using Deopeia.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddApi().AddApplication().AddInfrastructure();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
);

app.Run();
