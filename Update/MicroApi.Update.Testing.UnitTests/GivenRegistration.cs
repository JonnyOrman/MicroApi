using MicroApi.Update.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using XpressTest;
using Xunit;

namespace MicroApi.Update.Testing.UnitTests;

public class GivenRegistration
{
    [Fact]
    public void WhenServicesAreRegisteredToAServiceCollectionThenTheServicesAreAddedToTheServiceCollection() =>
        GivenA<ServiceCollection>
            .WhenIt(sut => sut.AddUpdate<TestEntity, string, TestParameters, TestUpdater>())
            .Then(assertion =>
            {
                Assert.Equal(21, assertion.Result.Count);
            
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IRequestHandler<TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidator<TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultGenerator<TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperation<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationResultHandler<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<TestEntity, TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<TestParameters>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<TestEntity, TestParameters>)));
                Assert.Equal(2, assertion.Result.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
            });
}
