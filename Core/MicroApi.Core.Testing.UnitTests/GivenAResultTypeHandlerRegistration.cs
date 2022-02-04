using MicroApi.Core.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Core.Testing.UnitTests;

public class GivenAResultTypeHandlerRegistration
{
    public class WhenItIsConstructed
    {
        [Fact]
        public void ThenItShouldHaveTheProvidedHandler()
        {
            var resultTypeHandlerMock = new Mock<IResultTypeHandler<TestEntity>>();

            var validatorMock = new Mock<IValidator<Result<TestEntity>>>();

            var sut = new ResultTypeHandlerRegistration<TestEntity>(
                resultTypeHandlerMock.Object,
                validatorMock.Object
                );

            var result = sut.Handler;

            Assert.Equal(resultTypeHandlerMock.Object, result);
        }
    }

    public class WhenAResultIsCheckedForAMatch
    {
        [Fact]
        public void ThenTheResultsIsValidated()
        {
            var testEntityResult = new Result<TestEntity>(true, "success");

            var validationResult = new ValidationResult(true);

            var resultTypeHandlerMock = new Mock<IResultTypeHandler<TestEntity>>();

            var validatorMock = new Mock<IValidator<Result<TestEntity>>>();
            validatorMock
                .Setup(validator => validator.Validate(testEntityResult))
                .Returns(validationResult);

            var sut = new ResultTypeHandlerRegistration<TestEntity>(
                resultTypeHandlerMock.Object,
                validatorMock.Object
            );

            var result = sut.IsMatch(testEntityResult);

            Assert.True(result);
        }
    }
}