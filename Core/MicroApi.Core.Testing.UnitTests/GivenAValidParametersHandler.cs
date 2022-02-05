using MicroApi.Core.Testing.UnitTests.TestClasses;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Core.Testing.UnitTests;

public class GivenAValidParametersHandler
{
    public class WhenSomeParametersAreHandled
    {
        [Fact]
        public async Task ThenTheOperationIsInvoked()
        {
            var parameters = new TestParameters();

            var entity = new TestEntity(1);

            var testOperator = new TestOperator(entity);
            
            var sut = new ValidParametersHandler<TestEntity, TestParameters, TestOperator>(
                testOperator,
                (testOperator, testParameters) => testOperator.Execute(testParameters)
            );

            var result = await sut.HandleAsync(parameters);

            var successResult = result as SuccessResult<TestEntity>;
            Assert.Equal(entity, successResult.Entity);
        }
    }
}
