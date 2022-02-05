using MicroApi.Core;
using MicroApi.Read.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenASingleReadResultHandler
{
    public class WhenANotNullResultIsHandled
    {
        [Fact]
        public void ThenASuccessResultIsCreated()
        {
            var key = "abc";

            var entity = new TestEntity(key);

            var sut = new SingleReadResultHandler<TestEntity, string>();

            var result = sut.Handle(entity, key);

            var successResult = result as SuccessResult<TestEntity>;
            Assert.Equal(entity, successResult.Entity);
        }
    }

    public class WhenANullResultIsHandled
    {
        [Fact]
        public void ThenANotFoundResultIsCreated()
        {
            var key = "abc";

            TestEntity entity = null;

            var sut = new SingleReadResultHandler<TestEntity, string>();

            var result = sut.Handle(entity, key);

            var notFoundResult = result as NotFoundResult<TestEntity, string>;
            Assert.Equal(key, notFoundResult.Key);
        }
    }
}
