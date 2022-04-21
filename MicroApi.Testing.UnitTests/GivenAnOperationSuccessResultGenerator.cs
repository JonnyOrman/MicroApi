using MicroApi.Testing.UnitTests.TestClasses;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessResultGenerator
{
    [Fact]
    public void WhenItGeneratesAnOperationSuccessResultThenASuccessResultIsGenerated() =>
        GivenA<OperationSuccessResultGenerator<TestEntity>>
                .AndGiven(new TestEntity(1))
            .WhenIt(action => action.Sut.Generate(action.GetThe<TestEntity>()))
            .Then(assertion =>
            {
                Assert.IsType<SuccessResult<TestEntity>>(assertion.Result);
                var successResult = assertion.Result as SuccessResult<TestEntity>;
                Assert.Equal(assertion.GetThe<TestEntity>(), successResult.Entity);
            });
}
