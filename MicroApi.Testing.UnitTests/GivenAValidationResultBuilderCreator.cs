using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultBuilderCreator
{
    [Fact]
    public void WhenAValidationResultBuilderIsCreatedThenItCreatesAValidationResultBuilder() =>
        GivenA<ValidationResultBuilderCreator>
            .WhenIt(sut => sut.Create())
            .Then(assertion =>
            {
                Assert.NotNull(assertion.Result);
            });
}
