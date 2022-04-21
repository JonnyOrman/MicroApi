using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationFailedResultHandler
{
    [Fact]
    public void WhenAFailedOperationResultIsHandledThenItRunsTheEventsAndGeneratesTheResult() =>
        GivenA<OperationFailedResultHandler<TestEntity, TestParameters>>
                .AndGiven(new TestParameters())
                .AndGiven(new Result<TestEntity>(true, "successful"))
            .WithA<IOperationFailedEventsRunner<TestParameters>>()
            .WithA<IOperationFailedResultGenerator<TestEntity, TestParameters>>()
                .ThatDoes<Result<TestEntity>>(arrangement => operationFailedResultGenerator => operationFailedResultGenerator.Generate(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WhenIt(action => action.Sut.Handle(action.GetThe<TestParameters>()))
            .ThenThe<IOperationFailedEventsRunner<TestParameters>>()
                .Should(arrangement => runner => runner.Run(arrangement.GetThe<TestParameters>()))
                .Once()
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
