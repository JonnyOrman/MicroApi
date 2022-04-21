using MicroApi.Testing.UnitTests.TestClasses;
using System.Collections.Generic;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnInvalidParametersResultTypedHandler
{
    [Fact]
    public void WhenAnInvalidParametersResultIsHandledThenItReturnsABadRequestObjectResult() =>
        GivenA<InvalidParametersResultTypedHandler<TestEntity, TestParameters>>
                .AndGivenA<IEnumerable<InvalidParameter>>()
                .AndGiven(arrangement => new InvalidParametersResult<TestEntity, TestParameters>(arrangement.GetTheMockObject<IEnumerable<InvalidParameter>>()))
            .WhenIt(action => action.Sut.Handle(action.GetThe<InvalidParametersResult<TestEntity, TestParameters>>()))
            .Then(assertion =>
            {
                Assert.Equal("Microsoft.AspNetCore.Http.Result.BadRequestObjectResult", assertion.Result.GetType().FullName);

                var notFoundResult = assertion.GetThe<InvalidParametersResult<TestEntity, TestParameters>>();
                
                var valueProperty = assertion.Result.GetType().GetProperty("Value");
                var value = valueProperty.GetValue(assertion.Result);
                Assert.Equal(notFoundResult, value);
            });
}
