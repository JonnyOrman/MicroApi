using MicroApi.Read.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenASuccessResultTypedHandler
{
    [Fact]
    public void WhenASuccessResultIsHandledThenItReturnsAnOkObjectResult() =>
        GivenA<SuccessResultTypedHandler<TestEntity>>
                .AndGiven(new TestEntity("abc"))
                .AndGiven(arrangement => new SuccessResult<TestEntity>(arrangement.GetThe<TestEntity>()))
            .WhenIt(arrangement => arrangement.Sut.Handle(arrangement.GetThe<SuccessResult<TestEntity>>()))
            .Then(assertion =>
            {
                Assert.Equal("Microsoft.AspNetCore.Http.Result.OkObjectResult", assertion.Result.GetType().FullName);

                var valueProperty = assertion.Result.GetType().GetProperty("Value");
                var value = valueProperty.GetValue(assertion.Result);
                Assert.Equal(assertion.GetThe<SuccessResult<TestEntity>>(), value);
            });
}
