using System.Linq;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultBuilder
{
    [Fact]
    public void WhenAValidationResultIsBuiltFromSomeValidValidationRuleResultsThenAValidResultIsBuilt() =>
        GivenA<ValidationResultBuilder>
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult1")
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult2")
                .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult3")
            .WhenIt(action => action.Sut.Add(action.GetThe<ValidationRuleResult>("ValidationRuleResult1")))
            .ThenWhenIt(action => action.Sut.Add(action.GetThe<ValidationRuleResult>("ValidationRuleResult2")))
            .ThenWhenIt(action => action.Sut.Add(action.GetThe<ValidationRuleResult>("ValidationRuleResult3")))
            .ThenWhenIt(sut => sut.Build())
            .ThenTheResultShouldBeA<ValidResult>();

    [Fact]
    public void WhenAValidationResultIsBuiltIncludingAnInvalidValidationRuleResultThenAnInvalidResultIsBuilt() =>
        GivenA<ValidationResultBuilder>
            .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult1")
            .AndGiven(new InvalidPropertyRuleResult("InvalidProperty", "The value for this property is invalid"),
                "ValidationRuleResult2")
            .AndGiven(new ValidationRuleResult(true), "ValidationRuleResult3")
            .WhenIt(action => action.Sut.Add(action.GetThe<ValidationRuleResult>("ValidationRuleResult1")))
            .ThenWhenIt(action => action.Sut.Add(action.GetThe<InvalidPropertyRuleResult>("ValidationRuleResult2")))
            .ThenWhenIt(action => action.Sut.Add(action.GetThe<ValidationRuleResult>("ValidationRuleResult3")))
            .ThenWhenIt(sut => sut.Build())
            .Then(assertion =>
            {
                Assert.IsType<InvalidResult>(assertion.Result); 
                var invalidResult = assertion.Result as InvalidResult;
                Assert.Equal(1, invalidResult.InvalidPropertyValues.Count());
                Assert.Equal("The value for this property is invalid", invalidResult.InvalidPropertyValues.Single(x => x.PropertyName == "InvalidProperty").ErrorMessages.First());
            });
}
