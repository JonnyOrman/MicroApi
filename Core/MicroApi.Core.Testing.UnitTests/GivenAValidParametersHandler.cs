using MicroApi.Core.Testing.UnitTests.TestClasses;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Core.Testing.UnitTests;

public class GivenAValidParametersHandler
{
    public class WhenSomeParametersAreHandled
    {
        [Fact]
        public async Task ThenTheOperationResultIsHandled()
        {
            var parameters = new TestParameters();

            var entity = new TestEntity(1);

            var testOperator = new TestOperator(entity);

            var operationResultHandledResult = new Result<TestEntity>(true, "success");

            var operationResultHandlerMock = new Mock<IOperationResultHandler<TestEntity, TestParameters>>();
            operationResultHandlerMock
                .Setup(operationResultHandler => operationResultHandler.Handle(entity, parameters))
                .Returns(operationResultHandledResult);

            var sut = new ValidParametersHandler<TestEntity, TestParameters, TestOperator>(
                testOperator,
                (testOperator, testParameters) => testOperator.Execute(testParameters),
                operationResultHandlerMock.Object
            );

            var result = await sut.HandleAsync(parameters);

            Assert.Equal(operationResultHandledResult, result);
        }
    }
}
