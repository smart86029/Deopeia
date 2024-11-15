var builder = DistributedApplication.CreateBuilder(args);

var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var kafka = builder
    .AddKafka("kafka")
    .WithKafkaUI(x => x.WithHostPort(9100))
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder
    .AddPostgres("postgres", password: password, port: 59999)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);
var dbIdentity = postgres.AddDatabase("identity");
var dbQuote = postgres.AddDatabase("quote");
var dbTrading = postgres.AddDatabase("trading");

var identityApi = builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbIdentity)
    .WaitFor(dbIdentity);

var quoteApi = builder
    .AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(kafka)
    .WaitFor(kafka)
    .WithReference(dbQuote)
    .WaitFor(dbQuote);

builder
    .AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(kafka)
    .WithReference(dbQuote);

var tradingApi = builder
    .AddProject<Projects.Deopeia_Trading_Api>("deopeia-trading-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(kafka)
    .WaitFor(kafka)
    .WithReference(dbTrading)
    .WaitFor(dbTrading);

builder
    .AddProject<Projects.Deopeia_Finance_Bff>("deopeia-finance-bff")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WithReference(identityApi)
    .WithReference(quoteApi)
    .WithReference(tradingApi);

builder.Build().Run();
