using MicroApi.Core;
using MicroApi.Read.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenAGetManyRequestHandler
{
    public class WhenItHandlesAGetManyQuery
    {
        [Fact]
        public async Task ThenItProcessesTheQueryAndHandlesTheResult()
        {
            var query = new TestQuery();

            var parametersProcessorResult = new Result<IEnumerable<TestEntity>>(true, "message");

            var handledResultMock = new Mock<IResult>();

            var parametersProcessorMock = new Mock<ICollectionProvider<TestEntity, TestQuery>>();
            parametersProcessorMock
                .Setup(parametersProcessor => parametersProcessor.GetAsync(query))
                .ReturnsAsync(parametersProcessorResult);

            var resultHandlerMock = new Mock<IResultHandler<IEnumerable<TestEntity>>>();
            resultHandlerMock
                .Setup(resultHandler => resultHandler.Handle(parametersProcessorResult))
                .Returns(handledResultMock.Object);

            var sut = new GetManyRequestHandler<TestEntity, TestQuery>(
                parametersProcessorMock.Object,
                resultHandlerMock.Object
                );

            var result = await sut.HandleAsync(query);

            parametersProcessorMock.Verify(parametersProcessor => parametersProcessor.GetAsync(query), Times.Once);
            resultHandlerMock.Verify(resultHandler => resultHandler.Handle(parametersProcessorResult), Times.Once);
            
            Assert.Equal(handledResultMock.Object, result);
        }
    }
}
