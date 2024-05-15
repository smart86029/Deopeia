namespace Viriplaca.Common.Domain;

public class CurrentUser
{
    public CurrentUser() { }

    public CurrentUser(Guid id, IPAddress address)
    {
        Id = id;
        Address = address;
    }

    public Guid Id { get; private init; }

    public IPAddress Address { get; private init; } = IPAddress.Any;
}
