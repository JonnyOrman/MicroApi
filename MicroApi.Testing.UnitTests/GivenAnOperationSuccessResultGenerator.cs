using MicroApi.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessResultGenerator
{
    public class WhenItGeneratesAnOperationSuccessResult
    {
        [Fact]
        public void ThenASuccessResultIsGenerated()
        {
            var entity = new TestEntity(1);

            var sut = new OperationSuccessResultGenerator<TestEntity>();

            var result = sut.Generate(entity);

            Assert.IsType<SuccessResult<TestEntity>>(result);
            var successResult = result as SuccessResult<TestEntity>;
            Assert.Equal(entity, successResult.Entity);
        }
    }
}
