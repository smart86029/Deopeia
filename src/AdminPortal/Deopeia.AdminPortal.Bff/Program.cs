using Deopeia.Common.Bff;

var builder = WebApplication.CreateBuilder(args);
builder.AddBff();

var app = builder.Build();

app.MapControllers();
app.MapScalar();

app.Run();
