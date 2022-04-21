using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationParametersProcessor
{
    [Fact]
    public void WhenSomeParametersAreProcessedThenTheParametersAreValidatedAndTheValidationResultIsHandled() =>
        GivenA<ValidationParametersProcessor<TestEntity, TestParameters>>
                .AndGiven(new TestParameters())
                .AndGiven(new ValidationResult(true))
                .AndGiven(new Result<TestEntity>(true, "successful"))
            .WithA<IValidator<TestParameters>>()
                .ThatDoes<ValidationResult>(arrangement =>
                    validator => validator.Validate(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetThe<ValidationResult>())
            .WithA<IParametersValidationResultHandler<TestEntity, TestParameters>>()
                .ThatDoesAsync<Result<TestEntity>>(arrangement => parametersValidationResultHandler =>
                    parametersValidationResultHandler.HandleAsync(arrangement.GetThe<TestParameters>(),
                    arrangement.GetThe<ValidationResult>()))
                .AndReturns(arrangement => arrangement.GetThe<Result<TestEntity>>())
                .WhenItAsync(action => action.Sut.ProcessAsync(action.GetThe<TestParameters>()))
                .ThenTheResultShouldBe(arrangement => arrangement.GetThe<Result<TestEntity>>());
}
