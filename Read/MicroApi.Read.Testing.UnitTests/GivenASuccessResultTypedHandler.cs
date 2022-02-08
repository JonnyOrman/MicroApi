using MicroApi.Read.Testing.UnitTests.TestClasses;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenASuccessResultTypedHandler
{
    public class WhenASuccessResultIsHandled
    {
        [Fact]
        public void ThenItReturnsAnOkObjectResult()
        {
            var entity = new TestEntity("abc");

            var successResult = new SuccessResult<TestEntity>(entity);

            var sut = new SuccessResultTypedHandler<TestEntity>();

            var result = sut.Handle(successResult);

            Assert.Equal("Microsoft.AspNetCore.Http.Result.OkObjectResult", result.GetType().FullName);

            var valueProperty = result.GetType().GetProperty("Value");
            var value = valueProperty.GetValue(result);
            Assert.Equal(successResult, value);
        }
    }
}
