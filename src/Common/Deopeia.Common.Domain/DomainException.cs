namespace Deopeia.Common.Domain;

public class DomainException : Exception
{
    public DomainException(string code)
        : base(code) { }

    public DomainException(string code, object argument)
        : base(code) { }
}
