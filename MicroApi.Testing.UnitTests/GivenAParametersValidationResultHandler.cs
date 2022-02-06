using System.Collections.Generic;
using System.Threading.Tasks;
using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAParametersValidationResultHandler
{
    public class WhenAValidParametersValidationResultIsHandled
    {
        [Fact]
        public async Task ThenTheParametersAreHandled()
        {
            var parameters = new TestParameters();

            var validationResult = new ValidationResult(true);

            var parametersHandledResult = new Result<TestEntity>(true, "success");

            var invalidResultHandlerMock = new Mock<IInvalidResultHandler<TestEntity>>();

            var validParametersHandlerMock = new Mock<IValidParametersHandler<TestEntity, TestParameters>>();
            validParametersHandlerMock
                .Setup(validationParametersHandler => validationParametersHandler.HandleAsync(parameters))
                .ReturnsAsync(parametersHandledResult);

            var sut = new ParametersValidationResultHandler<TestEntity, TestParameters>(
                invalidResultHandlerMock.Object,
                validParametersHandlerMock.Object
                );

            var result = await sut.HandleAsync(parameters, validationResult);

            Assert.Equal(parametersHandledResult, result);
        }
    }

    public class WhenAnInvalidParametersValidationResultIsHandled
    {
        [Fact]
        public async Task ThenTheInvalidResultIsHandled()
        {
            var parameters = new TestParameters();

            var invalidPropertyValuesMock = new Mock<IEnumerable<InvalidPropertyValue>>();

            var validationResult = new InvalidResult(invalidPropertyValuesMock.Object);

            var parametersHandledResult = new Result<TestEntity>(false, "invalid");

            var invalidResultHandlerMock = new Mock<IInvalidResultHandler<TestEntity>>();
            invalidResultHandlerMock
                .Setup(validationParametersHandler => validationParametersHandler.Handle(validationResult))
                .Returns(parametersHandledResult);

            var validParametersHandlerMock = new Mock<IValidParametersHandler<TestEntity, TestParameters>>();

            var sut = new ParametersValidationResultHandler<TestEntity, TestParameters>(
                invalidResultHandlerMock.Object,
                validParametersHandlerMock.Object
            );

            var result = await sut.HandleAsync(parameters, validationResult);

            Assert.Equal(parametersHandledResult, result);
        }
    }
}
