using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationResultHandler
{
    public class WhenANonNullOperationResultIsHandled
    {
        [Fact]
        public void ThenItHandlesTheSuccessResult()
        {
            var entity = new TestEntity(1);

            var parameters = new TestParameters();

            var successResult = new Result<TestEntity>(true, "success");

            var operationSuccessResultHandlerMock = new Mock<IOperationSuccessResultHandler<TestEntity, TestParameters>>();
            operationSuccessResultHandlerMock
                .Setup(operationSuccessResultHandler => operationSuccessResultHandler.Handle(entity, parameters))
                .Returns(successResult);

            var operationFailedResultHandlerMock = new Mock<IOperationFailedResultHandler<TestEntity, TestParameters>>();

            var sut = new OperationResultHandler<TestEntity, TestParameters>(
                operationSuccessResultHandlerMock.Object,
                operationFailedResultHandlerMock.Object
                );

            var result = sut.Handle(entity, parameters);

            Assert.Equal(successResult, result);

            operationFailedResultHandlerMock.Verify(operationFailedResultHandler => operationFailedResultHandler.Handle(It.IsAny<TestParameters>()), Times.Never);
        }
    }

    public class WhenANullOperationResultIsHandled
    {
        [Fact]
        public void ThenItHandlesTheFailedResult()
        {
            var parameters = new TestParameters();

            var failedResult = new Result<TestEntity>(false, "failed");

            var operationSuccessResultHandlerMock = new Mock<IOperationSuccessResultHandler<TestEntity, TestParameters>>();

            var operationFailedResultHandlerMock = new Mock<IOperationFailedResultHandler<TestEntity, TestParameters>>();
            operationFailedResultHandlerMock
                .Setup(operationFailedResultHandler => operationFailedResultHandler.Handle(parameters))
                .Returns(failedResult);

            var sut = new OperationResultHandler<TestEntity, TestParameters>(
                operationSuccessResultHandlerMock.Object,
                operationFailedResultHandlerMock.Object
            );

            var result = sut.Handle(null, parameters);

            Assert.Equal(failedResult, result);

            operationSuccessResultHandlerMock.Verify(operationSuccessResultHandler => operationSuccessResultHandler.Handle(It.IsAny<TestEntity>(), It.IsAny<TestParameters>()), Times.Never);
        }
    }
}
