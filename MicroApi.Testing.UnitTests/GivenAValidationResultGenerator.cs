using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultGenerator
{
    public class WhenAValidationResultIsGeneratedForSomeValidationRuleResults
    {
        [Fact]
        public void ThenTheValidationResultIsBuiltFromTheValidationRuleResults()
        {
            var validationResult = new ValidationResult(true);

            var validationRuleResult1 = new ValidationRuleResult(true);
            var validationRuleResult2 = new ValidationRuleResult(true);
            var validationRuleResult3 = new ValidationRuleResult(true);

            var validationRuleResultsMock = new List<ValidationRuleResult>
            {
                validationRuleResult1,
                validationRuleResult2,
                validationRuleResult3
            };

            var validationResultBuilderMock = new Mock<IValidationResultBuilder>();
            validationResultBuilderMock
                .Setup(validationResultBuilder => validationResultBuilder.Build())
                .Returns(validationResult);

            var validationResultBuilderCreatorMock = new Mock<IValidationResultBuilderCreator>();
            validationResultBuilderCreatorMock
                .Setup(validationResultBuilderCreator => validationResultBuilderCreator.Create())
                .Returns(validationResultBuilderMock.Object);

            var sut = new ValidationResultGenerator<TestParameters>(
                validationResultBuilderCreatorMock.Object
                );

            var result = sut.Generate(validationRuleResultsMock);

            validationResultBuilderMock.Verify(validationResultBuilder => validationResultBuilder.Add(validationRuleResult1), Times.Once);
            validationResultBuilderMock.Verify(validationResultBuilder => validationResultBuilder.Add(validationRuleResult2), Times.Once);
            validationResultBuilderMock.Verify(validationResultBuilder => validationResultBuilder.Add(validationRuleResult3), Times.Once);

            Assert.Equal(validationResult, result);
        }
    }
}
