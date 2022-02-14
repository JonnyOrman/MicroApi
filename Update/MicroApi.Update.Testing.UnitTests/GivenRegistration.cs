using MicroApi.Update.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace MicroApi.Update.Testing.UnitTests;

public class GivenRegistration
{
    public class WhenServicesAreRegisteredToAServiceCollection
    {
        [Fact]
        public void ThenTheServicesAreAddedToTheServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddUpdate<TestEntity, string, TestParameters, TestUpdater>();

            Assert.Equal(21, serviceCollection.Count);
            
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IRequestHandler<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultGenerator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperation<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationResultHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<TestEntity, TestParameters>)));
            Assert.Equal(2, serviceCollection.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
        }
    }
}
