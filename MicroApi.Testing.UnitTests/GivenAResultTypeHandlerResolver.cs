using System.Collections.Generic;
using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeHandlerResolver
{
    public class WhenAResultTypeHandlerIsResolvedForAResult
    {
        [Fact]
        public void ThenItResolvesTheResultTypeHandler()
        {
            var testEntityResult = new Result<TestEntity>(true, "successful");

            var resultTypeHandlerMock = new Mock<IResultTypeHandler<TestEntity>>();

            var resultTypeHandlerRegistrationMock = new Mock<IResultTypeHandlerRegistration<TestEntity>>();
            resultTypeHandlerRegistrationMock
                .Setup(resultTypeHandlerRegistration => resultTypeHandlerRegistration.IsMatch(testEntityResult))
                .Returns(true);
            resultTypeHandlerRegistrationMock
                .Setup(resultTypeHandlerRegistration => resultTypeHandlerRegistration.Handler)
                .Returns(resultTypeHandlerMock.Object);

            var handlerRegistrations = new List<IResultTypeHandlerRegistration<TestEntity>>
            {
                resultTypeHandlerRegistrationMock.Object
            };

            var sut = new ResultTypeHandlerResolver<TestEntity>(handlerRegistrations);

            var result = sut.Resolve(testEntityResult);

            Assert.Equal(resultTypeHandlerMock.Object, result);
        }
    }
}
