using MicroApi.Read.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenANotFoundResultTypedHandler
{
    [Fact]
    public void WhenANotFoundResultIsHandledThenItReturnsANotFoundObjectResult() =>
        GivenA<NotFoundResultTypedHandler<TestEntity, string>>
                .AndGiven(new NotFoundResult<TestEntity, string>("ghi"))
            .WhenIt(arrangement => arrangement.Sut.Handle(arrangement.GetThe<NotFoundResult<TestEntity, string>>()))
            .Then(assertion =>
            {
                Assert.Equal("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult", assertion.Result.GetType().FullName);
            
                var valueProperty = assertion.Result.GetType().GetProperty("Value");
                var value = valueProperty.GetValue(assertion.Result);
                Assert.Equal(assertion.GetThe<NotFoundResult<TestEntity, string>>(), value);
            });
}
