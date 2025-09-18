using Deopeia.AppHost;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();

var jwtIssuer = builder.AddParameter("jwt-issuer");

var dbIdentity = postgres.AddDatabase("identity");
var identityApi = builder
    .AddProject<Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("Jwt__Issuer", jwtIssuer)
    .WithS3()
    .WithReferenceAndWaitFor(dbIdentity);

var dbProduct = postgres.AddDatabase("product");
var productApi = builder
    .AddProject<Deopeia_Product_Api>("deopeia-product-api")
    .WithS3()
    .WithReferenceAndWaitFor(dbProduct);

builder
    .AddProject<Deopeia_AdminPortal_Bff>("deopeia-adminportal-bff")
    .WithReference(identityApi)
    .WithReference(productApi);

builder.Build().Run();
