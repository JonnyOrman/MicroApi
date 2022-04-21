using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAParametersValidationResultHandler
{
    [Fact]
    public void WhenAValidParametersValidationResultIsHandledThenTheParametersAreHandled() =>
        GivenA<ParametersValidationResultHandler<TestEntity, TestParameters>>
                .AndGiven(new TestParameters())
                .AndGiven(new ValidationResult(true))
                .AndGiven(new Result<TestEntity>(true, "success"))
            .WithA<IInvalidResultHandler<TestEntity>>()
            .WithA<IValidParametersHandler<TestEntity, TestParameters>>()
                .ThatDoesAsync<Result<TestEntity>>(arrangement => validParametersHandler => validParametersHandler.HandleAsync(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WhenItAsync(action =>
                action.Sut.HandleAsync(action.GetThe<TestParameters>(), action.GetThe<ValidationResult>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
    
    [Fact]
    public void WhenAnInvalidParametersValidationResultIsHandledThenTheInvalidResultIsHandled() =>
        GivenA<ParametersValidationResultHandler<TestEntity, TestParameters>>
            .AndGiven(new TestParameters())
            .AndGivenA<IEnumerable<InvalidPropertyValue>>()
            .AndGiven(arrangement => new InvalidResult(arrangement.GetTheMockObject<IEnumerable<InvalidPropertyValue>>()))
            .AndGiven(new Result<TestEntity>(false, "invalid"))
            .WithA<IInvalidResultHandler<TestEntity>>()
                .ThatDoes<Result<TestEntity>>(arrangement => validationParametersHandler => validationParametersHandler.Handle(arrangement.GetThe<InvalidResult>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
            .WithA<IValidParametersHandler<TestEntity, TestParameters>>()
            .WhenItAsync(action =>
                action.Sut.HandleAsync(action.GetThe<TestParameters>(), action.GetThe<ValidationResult>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
