using MicroApi.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenARequestHandler
{
    [Fact]
    public void WhenItHandlesARequestThenItProcessesTheParametersAndHandlesTheResult() =>
        GivenA<RequestHandler<TestEntity, TestParameters>>
                .AndGiven(new TestParameters())
                .AndGiven(new Result<TestEntity>(true, "message"))
                .AndGivenA<IResult>()
            .WithA<IParametersProcessor<TestEntity, TestParameters>>()
                .ThatDoesAsync<Result<TestEntity>>(arrangement => parametersProcessor =>
                parametersProcessor.ProcessAsync(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WithA<IResultHandler<TestEntity>>()
                .ThatDoes<IResult>(arrangement =>
                resultHandler => resultHandler.Handle(arrangement.GetThe<Result<TestEntity>>()))
                .AndReturns(arrangement => arrangement.GetTheMockObject<IResult>())
            .WhenItAsync(action => action.Sut.HandleAsync(action.GetThe<TestParameters>()))
            .ThenTheAsync<IParametersProcessor<TestEntity, TestParameters>>()
                .Should(arrangement => parametersProcessor =>
                parametersProcessor.ProcessAsync(arrangement.GetThe<TestParameters>()))
                .Once()
            .ThenThe<IResultHandler<TestEntity>>()
                .Should(arrangement => resultHandler => resultHandler.Handle(arrangement.GetThe<Result<TestEntity>>()))
                .Once();
}