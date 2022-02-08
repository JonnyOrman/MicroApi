using MicroApi.Read.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenAReadSingleFailedResultGenerator
{
    public class WhenItGeneratesAReadSingleFailedResult
    {
        [Fact]
        public void ThenItGeneratesANotFoundResult()
        {
            var key = "abc";

            var sut = new ReadSingleFailedResultGenerator<TestEntity, string>();

            var result = sut.Generate(key);

            Assert.IsType<NotFoundResult<TestEntity, string>>(result);
            var notFoundResult = result as NotFoundResult<TestEntity, string>;
            Assert.Equal(key, notFoundResult.Key);
        }
    }
}
