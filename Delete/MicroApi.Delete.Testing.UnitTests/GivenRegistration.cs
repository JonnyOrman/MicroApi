using MicroApi.Delete.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using XpressTest;
using Xunit;

namespace MicroApi.Delete.Testing.UnitTests;

public class GivenRegistration
{
    [Fact]
    public void WhenServicesAreRegisteredToAServiceCollectionThenTheServicesAreAddedToTheServiceCollection() =>
        GivenA<ServiceCollection>
            .WhenIt(sut => sut.AddDelete<TestEntity, string, TestDeleter>())
            .Then(assertion =>
            {
                Assert.Equal(21, assertion.Result.Count);
            
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IRequestHandler<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultGenerator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperation<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<TestEntity, string>)));
                Assert.Equal(2, assertion.Result.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
            });
}
