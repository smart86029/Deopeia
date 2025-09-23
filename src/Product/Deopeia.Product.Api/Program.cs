using Deopeia.Common.Api;
using Deopeia.Common.Infrastructure;
using Deopeia.Product.Api.Services;
using Deopeia.Product.Application;
using Deopeia.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddApi().AddApplication().AddInfrastructure();

var services = builder.Services;
services.AddGrpc();

var app = builder.Build();
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.Migrate<ProductContext>();
}

app.MapDefaultEndpoints();
app.MapGrpcService<InstrumentService>();

app.Run();
