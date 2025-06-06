using Deopeia.Common.Domain.Finance;

namespace Deopeia.Quote.Domain.Instruments;

public class InstrumentLocale : EntityLocale<Symbol>
{
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }
}
