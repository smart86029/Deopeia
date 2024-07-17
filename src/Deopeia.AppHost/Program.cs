var builder = DistributedApplication.CreateBuilder(args);

var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();
var dbIdentity = postgres.AddDatabase("Identity");
var dbQuote = postgres.AddDatabase("Quote");

builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbIdentity);

builder
    .AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbQuote);

builder
    .AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbQuote);

builder.AddProject<Projects.Deopeia_Finance_Bff>("deopeia-finance-bff");

builder.Build().Run();
