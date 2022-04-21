using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationResultHandler
{
    [Fact]
    public void WhenANonNullOperationResultIsHandledThenItHandlesTheSuccessResult() =>
        GivenA<OperationResultHandler<TestEntity, TestParameters>>
                .AndGiven(new TestEntity(1))
                .AndGiven(new TestParameters())
                .AndGiven(new Result<TestEntity>(true, "success"))
            .WithA<IOperationSuccessResultHandler<TestEntity, TestParameters>>()
                .ThatDoes<Result<TestEntity>>(arrangement => operationSuccessResultHandler => operationSuccessResultHandler.Handle(arrangement.GetThe<TestEntity>(), arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WithA<IOperationFailedResultHandler<TestEntity, TestParameters>>()
            .WhenIt(action => action.Sut.Handle(action.GetThe<TestEntity>(), action.GetThe<TestParameters>()))
            .ThenThe<IOperationFailedResultHandler<TestEntity, TestParameters>>()
                .Should(handler => handler.Handle(It.IsAny<TestParameters>()))
                .Never()
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
    
    [Fact]
    public void WhenANullOperationResultIsHandledThenItHandlesTheFailedResult() =>
        GivenA<OperationResultHandler<TestEntity, TestParameters>>
                .AndGiven(new TestParameters())
                .AndGiven(new Result<TestEntity>(true, "success"))
            .WithA<IOperationSuccessResultHandler<TestEntity, TestParameters>>()
            .WithA<IOperationFailedResultHandler<TestEntity, TestParameters>>()
                .ThatDoes<Result<TestEntity>>(arrangement => operationFailedResultHandler => operationFailedResultHandler.Handle(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WhenIt(action => action.Sut.Handle(null, action.GetThe<TestParameters>()))
            .ThenThe<IOperationSuccessResultHandler<TestEntity, TestParameters>>()
                .Should(handler => handler.Handle(It.IsAny<TestEntity>(), It.IsAny<TestParameters>()))
                .Never()
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
