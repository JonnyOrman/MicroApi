using System;
using System.Threading.Tasks;
using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidParametersHandler
{
    [Fact]
    public void WhenSomeParametersAreHandledThenTheOperationResultIsHandled() =>
        GivenA<ValidParametersHandler<TestEntity, TestParameters, TestOperator>>
                .AndGiven(new TestParameters())
                .AndGiven(new TestEntity(1))
                .AndGiven(new Result<TestEntity>(true, "success"))
            .With(arrangement => new TestOperator(arrangement.GetThe<TestEntity>()))
            .With<Func<TestOperator, TestParameters, Task<TestEntity>>>((testOperator, testParameters) => testOperator.Execute(testParameters))
            .WithA<IOperationResultHandler<TestEntity, TestParameters>>()
                .ThatDoes<Result<TestEntity>>(arrangement => operationResultHandler =>
                    operationResultHandler.Handle(arrangement.GetThe<TestEntity>(), arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WhenItAsync(action => action.Sut.HandleAsync(action.GetThe<TestParameters>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
