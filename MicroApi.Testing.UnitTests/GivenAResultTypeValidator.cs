using MicroApi.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeValidator
{
    public class WhenAResultOfTheRequiredTypeIsValidated
    {
        [Fact]
        public void ThenValidationIsSuccessful()
        {
            var testResult = new TestResultTypeA(true, "successful");

            var sut = new ResultTypeValidator<TestEntity, TestResultTypeA>();

            var result = sut.Validate(testResult);

            Assert.True(result.IsSuccessful);
        }
    }

    public class WhenAResultOfADifferentTypeIsValidated
    {
        [Fact]
        public void ThenValidationIsNotSuccessful()
        {
            var testResult = new TestResultTypeB(true, "successful");

            var sut = new ResultTypeValidator<TestEntity, TestResultTypeA>();

            var result = sut.Validate(testResult);

            Assert.False(result.IsSuccessful);
        }
    }
}
