using MicroApi.Create.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Create.Testing.UnitTests;

public class GivenACreateResultHandler
{
    public class WhenAResultIsHandled
    {
        [Fact]
        public void ThenASuccessResultIsCreated()
        {
            var entity = new TestEntity("abc");

            var parameters = new TestParameters();

            var sut = new CreateResultHandler<TestEntity, TestParameters>();

            var result = sut.Handle(entity, parameters);

            var successResult = result as SuccessResult<TestEntity>;
            Assert.Equal(entity, successResult.Entity);
        }
    }
}
