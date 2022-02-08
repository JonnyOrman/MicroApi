using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnInvalidParametersResultTypedHandler
{
    public class WhenAnInvalidParametersResultIsHandled
    {
        [Fact]
        public void ThenItReturnsABadRequestObjectResult()
        {
            var invalidParametersMock = new Mock<IEnumerable<InvalidParameter>>();

            var notFoundResult = new InvalidParametersResult<TestEntity, TestParameters>(invalidParametersMock.Object);

            var sut = new InvalidParametersResultTypedHandler<TestEntity, TestParameters>();

            var result = sut.Handle(notFoundResult);

            Assert.Equal("Microsoft.AspNetCore.Http.Result.BadRequestObjectResult", result.GetType().FullName);

            var valueProperty = result.GetType().GetProperty("Value");
            var value = valueProperty.GetValue(result);
            Assert.Equal(notFoundResult, value);
        }
    }
}
