using System.Collections.Generic;
using System.Linq;
using MicroApi.Testing.UnitTests.TestClasses;
using Xunit;
using XpressTest;

namespace MicroApi.Testing.UnitTests;

public class GivenAnInvalidParametersResultHandler
{
    [Fact]
    public void WhenAnInvalidResultIsHandledThenAnInvalidParametersResultIsCreated()
    {
        var invalidPropertyValue1 = new InvalidPropertyValue(
            "Property1",
            new List<string>
            {
                "Property1 is invalid"
            });
        var invalidPropertyValue2 = new InvalidPropertyValue(
            "Property2",
            new List<string>
            {
                "Property2 is invalid"
            });
        var invalidPropertyValue3 = new InvalidPropertyValue(
            "Property3",
            new List<string>
            {
                "Property3 is invalid"
            });

        var invalidPropertyValues = new List<InvalidPropertyValue>
            {
                invalidPropertyValue1,
                invalidPropertyValue2,
                invalidPropertyValue3
            };

        var invalidResult = new InvalidResult(invalidPropertyValues);

        GivenA<InvalidParametersResultHandler<TestEntity, TestParameters>>
            .WhenIt(sut => sut.Handle(invalidResult))
            .Then(assertion => {
                var invalidParametersResult = assertion.Result as InvalidParametersResult<TestEntity, TestParameters>;
        
                var invalidParametersList = invalidParametersResult.InvalidParameters.ToList();
                Assert.Equal("Property1", invalidParametersList[0].ParameterName);
                Assert.Equal("Property1 is invalid", invalidParametersList[0].Messages.First());
                Assert.Equal("Property2", invalidParametersList[1].ParameterName);
                Assert.Equal("Property2 is invalid", invalidParametersList[1].Messages.First());
                Assert.Equal("Property3", invalidParametersList[2].ParameterName);
                Assert.Equal("Property3 is invalid", invalidParametersList[2].Messages.First());
            });
    }
}
