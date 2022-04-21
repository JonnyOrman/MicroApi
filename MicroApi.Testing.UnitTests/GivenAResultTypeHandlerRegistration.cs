using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAResultTypeHandlerRegistration
{
    [Fact]
    public void WhenItIsConstructedThenItShouldHaveTheProvidedHandler() =>
        GivenA<ResultTypeHandlerRegistration<TestEntity>>
            .WithA<IResultTypeHandler<TestEntity>>()
            .WithA<IValidator<Result<TestEntity>>>()
            .WhenIt(sut => sut.Handler)
            .ThenTheResultShouldBe(arrangement => arrangement.GetTheMockObject<IResultTypeHandler<TestEntity>>());
    
    [Fact]
    public void WhenAResultIsCheckedForAMatchThenTheResultsIsValidated() =>
        GivenA<ResultTypeHandlerRegistration<TestEntity>>
                .AndGiven(new Result<TestEntity>(true, "success"))
                .AndGiven(new ValidationResult(true))
            .WithA<IResultTypeHandler<TestEntity>>()
            .WithA<IValidator<Result<TestEntity>>>()
                .ThatDoes<ValidationResult>(arrangement => validator => validator.Validate(arrangement.GetThe<Result<TestEntity>>()))
                .AndReturns(arrangement => arrangement.GetThe<ValidationResult>())
            .WhenIt(action => action.Sut.IsMatch(action.GetThe<Result<TestEntity>>()))
            .ThenTheResultShouldBe(true);
}