using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessResultHandler
{
    [Fact]
    public void WhenASuccessOperationResultIsHandledThenItRunsTheEventsAndGeneratesTheResult() =>
        GivenA<OperationSuccessResultHandler<TestEntity, TestParameters>>
                .AndGiven(new TestEntity(1))
                .AndGiven(new TestParameters())
                .AndGiven(new Result<TestEntity>(true, "successful"))
            .WithA<IOperationSuccessEventsRunner<TestEntity, TestParameters>>()
            .WithA<IOperationSuccessResultGenerator<TestEntity>>()
            .ThatDoes<Result<TestEntity>>(arrangement => operationSuccessResultGenerator =>
                operationSuccessResultGenerator.Generate(arrangement.GetThe<TestEntity>()))
            .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WhenIt(action => action.Sut.Handle(action.GetThe<TestEntity>(), action.GetThe<TestParameters>()))
            .ThenThe<IOperationSuccessEventsRunner<TestEntity, TestParameters>>()
                .Should(arrangement => runner =>
                runner.Run(arrangement.GetThe<TestEntity>(), arrangement.GetThe<TestParameters>()))
                .Once()
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
