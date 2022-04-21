using MicroApi.Read.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenAReadSingleFailedResultGenerator
{
    [Fact]
    public void WhenUItGeneratesAReadSingleFailedResultThenItGeneratesANotFoundResult() =>
        GivenA<ReadSingleFailedResultGenerator<TestEntity, string>>
            .WhenIt(sut => sut.Generate("abc"))
            .Then(assertion =>
            {
                Assert.IsType<NotFoundResult<TestEntity, string>>(assertion.Result);
                var notFoundResult = assertion.Result as NotFoundResult<TestEntity, string>;
                Assert.Equal("abc", notFoundResult.Key);
            });
}
