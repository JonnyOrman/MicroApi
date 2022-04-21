using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeValidator
{
    [Fact]
    public void WhenAResultOfTheRequiredTypeIsValidatedThenValidationIsSuccessful() =>
        GivenA<ResultTypeValidator<TestEntity, TestResultTypeA>>
                .AndGiven(new TestResultTypeA(true, "successful"))
            .WhenIt(action => action.Sut.Validate(action.GetThe<TestResultTypeA>()))
            .ThenTheResult(result => result.IsSuccessful).ShouldBe(true);
    
    [Fact]
    public void WhenAResultOfADifferentTypeIsValidatedThenValidationIsNotSuccessful() =>
        GivenA<ResultTypeValidator<TestEntity, TestResultTypeA>>
                .AndGiven(new TestResultTypeB(true, "successful"))
            .WhenIt(action => action.Sut.Validate(action.GetThe<TestResultTypeB>()))
            .ThenTheResult(result => result.IsSuccessful).ShouldBe(false);
}
