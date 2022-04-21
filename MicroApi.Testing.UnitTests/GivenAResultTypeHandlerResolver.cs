using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeHandlerResolver
{
    [Fact]
    public void WhenAResultTypeHandlerIsResolvedForAResultThenItResolvesTheResultTypeHandler() =>
    GivenA<ResultTypeHandlerResolver<TestEntity>>
                .AndGiven(new Result<TestEntity>(true, "successful"))
                .AndGivenA<IResultTypeHandler<TestEntity>>()
                .AndGivenA<IResultTypeHandlerRegistration<TestEntity>>()
                    .ThatDoes<bool>(arrangement => resultTypeHandlerRegistration =>
                        resultTypeHandlerRegistration.IsMatch(arrangement.GetThe<Result<TestEntity>>()))
                    .AndReturns(true)
                    .ThatDoes(resultTypeHandlerRegistration => resultTypeHandlerRegistration.Handler)
                    .AndReturns(arrangement => arrangement.GetTheMockObject<IResultTypeHandler<TestEntity>>())
            .With(arrangement => new List<IResultTypeHandlerRegistration<TestEntity>>
                {
                    arrangement.GetTheMockObject<IResultTypeHandlerRegistration<TestEntity>>()
                })
            .WhenIt(action => action.Sut.Resolve(action.GetThe<Result<TestEntity>>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetTheMockObject<IResultTypeHandler<TestEntity>>());
}
