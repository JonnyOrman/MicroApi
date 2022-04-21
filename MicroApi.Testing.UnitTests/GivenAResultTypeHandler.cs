using MicroApi.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeHandler
{
    [Fact]
    public void WhenAMatchingResultTypeIsHandledThenItIsSuccessfullyHandled() =>
        GivenA<ResultTypeHandler<TestEntity, TestResult>>
                .AndGiven(new TestResult(true, "successful"))
                .AndGivenA<IResult>()
            .WithA<ITypedResultTypeHandler<TestResult>>()
                .ThatDoes<IResult>(arrangement =>
                typedResultTypeHandler => typedResultTypeHandler.Handle(arrangement.GetThe<TestResult>()))
                .AndReturns(arrangement => arrangement.GetTheMockObject<IResult>())
            .WhenIt(action => action.Sut.Handle(action.GetThe<TestResult>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetTheMockObject<IResult>());
}