using MicroApi.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultHandler
{
    [Fact]
    public void WhenAResultIsHandledThenTheHandlerForTheResultTypeIsResolvedAndUsedToHandleTheResult() =>
        GivenA<ResultHandler<TestEntity>>
                .AndGiven(new Result<TestEntity>(true, "successful"))
                .AndGivenA<IResult>()
                .AndGivenA<IResultTypeHandler<TestEntity>>()
                    .ThatDoes<IResult>(arrangement =>
                resultTypeHandler => resultTypeHandler.Handle(arrangement.GetThe<Result<TestEntity>>()))
                    .AndReturns(arrangement => arrangement.GetTheMockObject<IResult>())
            .WithA<IResultTypeHandlerResolver<TestEntity>>()
                .ThatDoes<IResultTypeHandler<TestEntity>>(arrangement => resultTypeHandlerResolver =>
                resultTypeHandlerResolver.Resolve(arrangement.GetThe<Result<TestEntity>>()))
                .AndReturns(arrangement => arrangement.GetTheMockObject<IResultTypeHandler<TestEntity>>())
            .WhenIt(action => action.Sut.Handle(action.GetThe<Result<TestEntity>>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetTheMockObject<IResult>());
}