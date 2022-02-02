using MicroApi.Read.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenANotFoundResult
{
    public class WhenANotFoundResultIsConstructed
    {
        [Fact]
        public void ThenItHasTheCorrectProperties()
        {
            var key = "def";

            var sut = new NotFoundResult<TestEntity, string>(key);

            Assert.Equal(key, sut.Key);
            Assert.False(sut.IsSuccessful);
            Assert.Equal("TestEntity with key def not found", sut.Message);
        }
    }
}
