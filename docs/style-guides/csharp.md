# C#

Based on following style guides:

-   [C# identifier naming rules and conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
-   [Framework design guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/)
-   [Google JavaScript Style Guide](https://google.github.io/styleguide/csharp-style.html)

## Naming guidelines

### Capitalization conventions

#### Pascal case

-   Namespaces
-   Type names (`class`, `struct`, `record`)
-   Positional parameters of `record` type
    ```csharp
    // Because they are property definitions
    public record Money(string CurrencyCode, decimal Amount);
    ```
-   Constant names, both fields and local constants
-   All public members

#### Camel case

-   Local variables
-   Parameters
-   `_camelCase` for private fields
