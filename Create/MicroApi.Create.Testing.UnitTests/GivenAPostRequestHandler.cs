using MicroApi.Create.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Create.Testing.UnitTests;

public class GivenAPostRequestHandler
{
    public class WhenItHandlesPostedParameters
    {
        [Fact]
        public async Task ThenItProcessesTheParametersAndHandlesTheResult()
        {
            var parameters = new TestParameters();

            var parametersProcessorResult = new Result<TestEntity>(true, "message");

            var handledResultMock = new Mock<IResult>();

            var parametersProcessorMock = new Mock<IParametersProcessor<TestEntity, TestParameters>>();
            parametersProcessorMock
                .Setup(parametersProcessor => parametersProcessor.ProcessAsync(parameters))
                .ReturnsAsync(parametersProcessorResult);

            var resultHandlerMock = new Mock<IResultHandler<TestEntity>>();
            resultHandlerMock
                .Setup(resultHandler => resultHandler.Handle(parametersProcessorResult))
                .Returns(handledResultMock.Object);

            var sut = new PostRequestHandler<TestEntity, TestParameters>(
                parametersProcessorMock.Object,
                resultHandlerMock.Object
            );

            var result = await sut.HandleAsync(parameters);

            parametersProcessorMock.Verify(parametersProcessor => parametersProcessor.ProcessAsync(parameters), Times.Once);
            resultHandlerMock.Verify(resultHandler => resultHandler.Handle(parametersProcessorResult), Times.Once);

            Assert.Equal(handledResultMock.Object, result);
        }
    }
}