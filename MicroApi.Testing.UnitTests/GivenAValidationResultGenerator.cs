using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultGenerator
{
    [Fact]
    public void WhenAValidationResultIsGeneratedForSomeValidationRuleResultsThenTheValidationResultIsBuiltFromTheValidationRuleResults() =>
        GivenA<ValidationResultGenerator<TestParameters>>
                .AndGiven(new ValidationResult(true))
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult1")
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult2")
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult3")
                .AndGiven(arrangement => new List<ValidationRuleResult>
                {
                    arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult1"),
                    arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult2"),
                    arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult3")
                })
                .AndGivenA<IValidationResultBuilder>()
                    .ThatDoes(validationResultBuilder => validationResultBuilder.Build())
                    .AndReturns(arrangement => arrangement.GetThe<ValidationResult>())
            .WithA<IValidationResultBuilderCreator>()
                .ThatDoes(validationResultBuilderCreator => validationResultBuilderCreator.Create())
                .AndReturnsTheMock<IValidationResultBuilder>()
            .WhenIt(action => action.Sut.Generate(action.GetThe<List<ValidationRuleResult>>()))
            .ThenThe<IValidationResultBuilder>()
                .Should(arrangement => validationResultBuilder =>
                    validationResultBuilder.Add(arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult1")))
                .Once()
            .ThenThe<IValidationResultBuilder>()
                .Should(arrangement => validationResultBuilder =>
                    validationResultBuilder.Add(arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult2")))
                .Once()
            .ThenThe<IValidationResultBuilder>()
                .Should(arrangement => validationResultBuilder =>
                    validationResultBuilder.Add(arrangement.GetThe<ValidationRuleResult>("ValidationRuleResult3")))
                .Once()
            .ThenTheResultShouldBe(arrangement => arrangement.GetThe<ValidationResult>());
}
