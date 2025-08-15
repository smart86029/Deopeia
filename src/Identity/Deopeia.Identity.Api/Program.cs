using Deopeia.Identity.Api.Services;
using Deopeia.Identity.Application;
using Deopeia.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddApplication().AddInfrastructure();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<UserService>();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
);

app.Run();
