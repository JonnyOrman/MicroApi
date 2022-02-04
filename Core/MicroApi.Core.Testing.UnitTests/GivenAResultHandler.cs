using MicroApi.Core.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace MicroApi.Core.Testing.UnitTests;

public class GivenAResultHandler
{
    public class WhenAResultIsHandled
    {
        [Fact]
        public void ThenTheHandlerForTheResultTypeIsResolvedAndUsedToHandleTheResult()
        {
            var testEntityResult = new Result<TestEntity>(true, "successful");

            var resultMock = new Mock<IResult>();

            var resultTypeHandlerMock = new Mock<IResultTypeHandler<TestEntity>>();
            resultTypeHandlerMock
                .Setup(resultTypeHandler => resultTypeHandler.Handle(testEntityResult))
                .Returns(resultMock.Object);

            var resultTypeHandlerResolverMock = new Mock<IResultTypeHandlerResolver<TestEntity>>();
            resultTypeHandlerResolverMock
                .Setup(resultTypeHandlerResolver => resultTypeHandlerResolver.Resolve(testEntityResult))
                .Returns(resultTypeHandlerMock.Object);

            var sut = new ResultHandler<TestEntity>(resultTypeHandlerResolverMock.Object);

            var result = sut.Handle(testEntityResult);

            Assert.Equal(resultMock.Object, result);
        }
    }
}