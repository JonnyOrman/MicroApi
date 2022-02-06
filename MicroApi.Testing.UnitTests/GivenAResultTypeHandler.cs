using MicroApi.Testing.UnitTests.TestClasses;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeHandler
{
    public class WhenAMatchingResultTypeIsHandled
    {
        [Fact]
        public void ThenItIsSuccessfullyHandled()
        {
            var testResult = new TestResult(true, "successful");

            var resultMock = new Mock<IResult>();

            var typedResultTypeHandlerMock = new Mock<ITypedResultTypeHandler<TestResult>>();
            typedResultTypeHandlerMock
                .Setup(typedResultTypeHandler => typedResultTypeHandler.Handle(testResult))
                .Returns(resultMock.Object);

            var sut = new ResultTypeHandler<TestEntity, TestResult>(
                typedResultTypeHandlerMock.Object
            );

            var result = sut.Handle(testResult);

            Assert.Equal(resultMock.Object, result);
        }
    }
}