var builder = DistributedApplication.CreateBuilder(args);

var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var rabbitMQ = builder.AddRabbitMQ("eventbus");

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();
var dbIdentity = postgres.AddDatabase("identity");
var dbQuote = postgres.AddDatabase("quote");

var identityApi = builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbIdentity);

var quoteApi = builder
    .AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(rabbitMQ)
    .WithReference(dbQuote);

builder
    .AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(rabbitMQ)
    .WithReference(dbQuote);

builder
    .AddProject<Projects.Deopeia_Finance_Bff>("deopeia-finance-bff")
    .WithReference(rabbitMQ)
    .WithReference(identityApi)
    .WithReference(quoteApi);

builder.AddProject<Projects.Deopeia_Trading_Api>("deopeia-trading-api");

builder.Build().Run();
