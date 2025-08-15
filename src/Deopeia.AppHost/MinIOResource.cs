namespace Deopeia.AppHost;

public class MinIOResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    internal const string ApiEndpointName = "api";
    internal const string ConsoleEndpointName = "console";
    internal const int DefaultApiPort = 9000;
    internal const int DefaultConsolePort = 9001;

    private EndpointReference? _apiReference;
    private EndpointReference? _consoleReference;

    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create(
            $"{ApiEndpoint.Property(EndpointProperty.Host)}:{ApiEndpoint.Property(EndpointProperty.Port)}"
        );

    private EndpointReference ApiEndpoint =>
        _apiReference ??= new EndpointReference(this, ApiEndpointName);

    private EndpointReference ConsoleEndpoint =>
        _consoleReference ??= new EndpointReference(this, ConsoleEndpointName);
}
