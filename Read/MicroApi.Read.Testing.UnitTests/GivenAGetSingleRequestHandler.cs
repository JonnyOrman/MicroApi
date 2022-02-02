using MicroApi.Core;
using MicroApi.Read.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenAGetSingleRequestHandler
{
    public class WhenItHandlesAnEntityKey
    {
        [Fact]
        public async Task ThenItProcessesTheKeyAndHandlesTheResult()
        {
            var key = "abc";

            var parametersProcessorResult = new Result<TestEntity>(true, "message");

            var handledResultMock = new Mock<IResult>();

            var parametersProcessorMock = new Mock<IParametersProcessor<TestEntity, string>>();
            parametersProcessorMock
                .Setup(parametersProcessor => parametersProcessor.ProcessAsync(key))
                .ReturnsAsync(parametersProcessorResult);

            var resultHandlerMock = new Mock<IResultHandler<TestEntity>>();
            resultHandlerMock
                .Setup(resultHandler => resultHandler.Handle(parametersProcessorResult))
                .Returns(handledResultMock.Object);

            var sut = new GetSingleRequestHandler<TestEntity, string>(
                parametersProcessorMock.Object,
                resultHandlerMock.Object
            );

            var result = await sut.HandleAsync(key);

            parametersProcessorMock.Verify(parametersProcessor => parametersProcessor.ProcessAsync(key), Times.Once);
            resultHandlerMock.Verify(resultHandler => resultHandler.Handle(parametersProcessorResult), Times.Once);

            Assert.Equal(handledResultMock.Object, result);
        }
    }
}
