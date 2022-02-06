using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidator
{
    public class WhenItValidatesAValue
    {
        [Fact]
        public void ThenItGeneratesAValidationResultFromTheRuleResults()
        {
            var validationValue = new TestParameters();

            var validationRuleResultsMock = new Mock<IEnumerable<ValidationRuleResult>>();

            var validationResult = new ValidationResult(true);
            
            var validationRuleResultsCalculatorMock = new Mock<IValidationRuleResultsCalculator<TestParameters>>();
            validationRuleResultsCalculatorMock
                .Setup(validationRuleResultsCalculator => validationRuleResultsCalculator.Calculate(validationValue))
                .Returns(validationRuleResultsMock.Object);

            var validationResultGeneratorMock = new Mock<IValidationResultGenerator<TestParameters>>();
            validationResultGeneratorMock
                .Setup(validationResultGenerator =>
                    validationResultGenerator.Generate(validationRuleResultsMock.Object))
                .Returns(validationResult);

            var sut = new Validator<TestParameters>(
                validationRuleResultsCalculatorMock.Object,
                validationResultGeneratorMock.Object
                );

            var result = sut.Validate(validationValue);

            Assert.Equal(validationResult, result);
        }
    }
}
