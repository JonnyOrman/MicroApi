using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationFailedResultHandler
{
    public class WhenAFailedOperationResultIsHandled
    {
        [Fact]
        public void ThenItRunsTheEventsAndGeneratesTheResult()
        {
            var parameters = new TestParameters();

            var generatedResult = new Result<TestEntity>(true, "successful");

            var operationFailedEventsRunnerMock = new Mock<IOperationFailedEventsRunner<TestParameters>>();

            var operationFailedResultGeneratorMock = new Mock<IOperationFailedResultGenerator<TestEntity, TestParameters>>();
            operationFailedResultGeneratorMock
                .Setup(operationFailedResultGenerator => operationFailedResultGenerator.Generate(parameters))
                .Returns(generatedResult);

            var sut = new OperationFailedResultHandler<TestEntity, TestParameters>(
                operationFailedEventsRunnerMock.Object,
                operationFailedResultGeneratorMock.Object
                );

            var result = sut.Handle(parameters);

            operationFailedEventsRunnerMock.Verify(operationFailedEventsRunner => operationFailedEventsRunner.Run(parameters), Times.Once);

            Assert.Equal(generatedResult, result);
        }
    }
}
