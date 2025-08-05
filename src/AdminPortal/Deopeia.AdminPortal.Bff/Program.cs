using Deopeia.Common.Bff;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddBff();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapScalar();

app.Run();
