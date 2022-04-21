using MicroApi.Read.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenANotFoundResult
{
    [Fact]
    public void WhenANotFoundResultIsConstructedThenItHasTheCorrectProperties() =>
        GivenA<NotFoundResult<TestEntity, string>>
            .With("def")
            .WhenItIsConstructed()
            .ThenIts(sut => sut.Key).ShouldBe("def")
            .ThenIts(sut => sut.IsSuccessful).ShouldBe(false)
            .ThenIts(sut => sut.Message).ShouldBe("TestEntity with key def not found");
}
