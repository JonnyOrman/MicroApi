using System.Threading.Tasks;
using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationParametersProcessor
{
    public class WhenSomeParametersAreProcessed
    {
        [Fact]
        public async Task ThenTheParametersAreValidatedAndTheValidationResultIsHandled()
        {
            var parameters = new TestParameters();

            var validationResult = new ValidationResult(true);

            var testEntityResult = new Result<TestEntity>(true, "successful");

            var validatorMock = new Mock<IValidator<TestParameters>>();
            validatorMock
                .Setup(validator => validator.Validate(parameters))
                .Returns(validationResult);

            var parametersValidationResultHandlerMock = new Mock<IParametersValidationResultHandler<TestEntity, TestParameters>>();
            parametersValidationResultHandlerMock
                .Setup(parametersValidationResultHandler =>
                    parametersValidationResultHandler.HandleAsync(parameters, validationResult))
                .ReturnsAsync(testEntityResult);

            var sut = new ValidationParametersProcessor<TestEntity, TestParameters>(
                validatorMock.Object,
                parametersValidationResultHandlerMock.Object
                );

            var result = await sut.ProcessAsync(parameters);

            Assert.Equal(testEntityResult, result);
        }
    }
}
