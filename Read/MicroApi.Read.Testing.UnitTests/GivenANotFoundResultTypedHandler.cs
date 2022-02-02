using MicroApi.Read.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenANotFoundResultTypedHandler
{
    public class WhenANotFoundResultIsHandled
    {
        [Fact]
        public void ThenItReturnsANotFoundStatusResult()
        {
            var key = "ghi";

            var notFoundResult = new NotFoundResult<TestEntity, string>(key);

            var sut = new NotFoundResultTypedHandler<TestEntity, string>();

            var result = sut.Handle(notFoundResult);

            Assert.Equal("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult", result.GetType().FullName);
            
            var valueProperty = result.GetType().GetProperty("Value");
            var value = valueProperty.GetValue(result);
            Assert.Equal(notFoundResult, value);
        }
    }
}
