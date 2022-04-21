using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidator
{
    [Fact]
    public void WhenItValidatesAValueThenItGeneratesAValidationResultFromTheRuleResults() =>
        GivenA<Validator<TestParameters>>
                .AndGiven(new TestParameters())
                .AndGivenA<IEnumerable<ValidationRuleResult>>()
                .AndGiven(new ValidationResult(true))
            .WithA<IValidationRuleResultsCalculator<TestParameters>>()
                .ThatDoes<IEnumerable<ValidationRuleResult>>(arrangement => validationRuleResultsCalculator =>
                    validationRuleResultsCalculator.Calculate(arrangement.GetThe<TestParameters>()))
                .AndReturns(arrangement => arrangement.GetTheMockObject<IEnumerable<ValidationRuleResult>>())
            .WithA<IValidationResultGenerator<TestParameters>>()
                .ThatDoes<ValidationResult>(arrangement => validationResultGenerator =>
                    validationResultGenerator.Generate(arrangement.GetTheMockObject<IEnumerable<ValidationRuleResult>>()))
                .AndReturns(arrangement => arrangement.GetThe<ValidationResult>())
            .WhenIt(action => action.Sut.Validate(action.GetThe<TestParameters>()))
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<ValidationResult>());
}
