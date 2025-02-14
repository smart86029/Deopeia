using Deopeia.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var jwtKey = builder.AddParameter("JwtKey");
var jwtIssuer = builder.AddParameter("JwtIssuer");
var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var kafka = builder.AddKafka("kafka").WithKafkaUI(x => x.WithHostPort(9100)).WithDataVolume();

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();
var dbIdentity = postgres.AddDatabase("identity");
var dbQuote = postgres.AddDatabase("quote");
var dbTrading = postgres.AddDatabase("trading");

var identityApi = builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("Jwt__Key", jwtKey)
    .WithEnvironment("Jwt__Issuer", jwtIssuer)
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReferenceAndWaitFor(kafka)
    .WithReferenceAndWaitFor(dbIdentity);
identityApi.WithEnvironment("Proxy", identityApi.GetEndpoint("https"));

var notificationHub = builder
    .AddProject<Projects.Deopeia_Notification_Hub>("deopeia-notification-hub")
    .WithReferenceAndWaitFor(kafka);

var quoteApi = builder
    .AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReferenceAndWaitFor(kafka)
    .WithReferenceAndWaitFor(dbQuote);
quoteApi.WithEnvironment("Proxy", quoteApi.GetEndpoint("http"));

builder
    .AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReferenceAndWaitFor(kafka)
    .WithReferenceAndWaitFor(dbQuote);

var tradingApi = builder
    .AddProject<Projects.Deopeia_Trading_Api>("deopeia-trading-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReferenceAndWaitFor(kafka)
    .WithReferenceAndWaitFor(dbTrading);
tradingApi.WithEnvironment("Proxy", tradingApi.GetEndpoint("http"));

builder
    .AddProject<Projects.Deopeia_Trading_Worker>("deopeia-trading-worker")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReferenceAndWaitFor(kafka)
    .WithReferenceAndWaitFor(dbTrading);

builder
    .AddProject<Projects.Deopeia_Finance_Bff>("deopeia-finance-bff")
    .WithEnvironment("Jwt__Key", jwtKey)
    .WithEnvironment("Jwt__Issuer", jwtIssuer)
    .WithReference(identityApi)
    .WithReference(notificationHub)
    .WithReference(quoteApi)
    .WithReference(tradingApi);

builder.Build().Run();
