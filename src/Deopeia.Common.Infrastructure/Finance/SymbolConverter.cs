using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Finance;

internal class SymbolConverter()
    : ValueConverter<Symbol, string>(symbol => symbol.Value, value => new Symbol(value)) { }
