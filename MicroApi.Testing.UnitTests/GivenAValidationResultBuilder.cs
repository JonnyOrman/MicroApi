using System.Linq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultBuilder
{
    public class WhenAValidationResultIsBuiltFromSomeValidValidationRuleResults
    {
        [Fact]
        public void ThenAValidResultIsBuilt()
        {
            var validationRuleResult1 = new ValidationRuleResult(true);
            var validationRuleResult2 = new ValidationRuleResult(true);
            var validationRuleResult3 = new ValidationRuleResult(true);

            var sut = new ValidationResultBuilder();

            sut.Add(validationRuleResult1);
            sut.Add(validationRuleResult2);
            sut.Add(validationRuleResult3);

            var result = sut.Build();

            Assert.IsType<ValidResult>(result);
        }
    }

    public class WhenAValidationResultIsBuiltIncludingAnInvalidValidationRuleResult
    {
        [Fact]
        public void ThenAnInvalidResultIsBuilt()
        {
            var validationRuleResult1 = new ValidationRuleResult(true);
            var validationRuleResult2 =
                new InvalidPropertyRuleResult("InvalidProperty", "The value for this property is invalid");
            var validationRuleResult3 = new ValidationRuleResult(true);

            var sut = new ValidationResultBuilder();

            sut.Add(validationRuleResult1);
            sut.Add(validationRuleResult2);
            sut.Add(validationRuleResult3);

            var result = sut.Build();

            Assert.IsType<InvalidResult>(result);
            var invalidResult = result as InvalidResult;
            Assert.Equal(1, invalidResult.InvalidPropertyValues.Count());
            Assert.Equal("The value for this property is invalid", invalidResult.InvalidPropertyValues.Single(x => x.PropertyName == "InvalidProperty").ErrorMessages.First());
        }
    }
}
