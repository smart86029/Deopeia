using Viriplaca.Common.Domain;

namespace Viriplaca.Common.UnitTests.Domain;

[TestClass]
public class ValidationExtensionsTests
{
    [DataTestMethod]
    [DataRow("A")]
    public void MustNotBeNullOrWhiteSpace_String_Pass(string value)
    {
        var action = () => value.MustNotBeNullOrWhiteSpace();

        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void MustNotBeNullOrWhiteSpace_Null_Throw(string value)
    {
        var action = () => value.MustNotBeNullOrWhiteSpace();

        action.Should().Throw<DomainException>();
    }
}
