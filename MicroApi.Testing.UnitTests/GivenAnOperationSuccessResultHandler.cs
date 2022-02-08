using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessResultHandler
{
    public class WhenASuccessOperationResultIsHandled
    {
        [Fact]
        public void ThenItRunsTheEventsAndGeneratesTheResult()
        {
            var entity = new TestEntity(1);

            var parameters = new TestParameters();

            var generatedResult = new Result<TestEntity>(true, "successful");

            var operationSuccessEventsRunnerMock = new Mock<IOperationSuccessEventsRunner<TestEntity, TestParameters>>();

            var operationSuccessResultGeneratorMock = new Mock<IOperationSuccessResultGenerator<TestEntity>>();
            operationSuccessResultGeneratorMock
                .Setup(operationSuccessResultGenerator => operationSuccessResultGenerator.Generate(entity))
                .Returns(generatedResult);

            var sut = new OperationSuccessResultHandler<TestEntity, TestParameters>(
                operationSuccessEventsRunnerMock.Object,
                operationSuccessResultGeneratorMock.Object
            );

            var result = sut.Handle(entity, parameters);

            operationSuccessEventsRunnerMock.Verify(operationSuccessEventsRunner => operationSuccessEventsRunner.Run(entity, parameters), Times.Once);

            Assert.Equal(generatedResult, result);
        }
    }
}
