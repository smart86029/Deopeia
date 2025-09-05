using Deopeia.AppHost;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();
var dbIdentity = postgres.AddDatabase("identity");

var jwtIssuer = builder.AddParameter("jwt-issuer");
var identityApi = builder
    .AddProject<Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("Jwt__Issuer", jwtIssuer)
    .WithS3()
    .WithReferenceAndWaitFor(dbIdentity);

builder.AddProject<Deopeia_AdminPortal_Bff>("deopeia-adminportal-bff").WithReference(identityApi);

builder.Build().Run();
