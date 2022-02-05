using MicroApi.Core;
using MicroApi.Create.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace MicroApi.Create.Testing.UnitTests;

public class GivenRegistration
{
    public class WhenServicesAreRegisteredToAServiceCollection
    {
        [Fact]
        public void ThenTheServicesAreAddedToTheServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddCreate<TestEntity, string, TestParameters, TestCreator>();

            Assert.Equal(13, serviceCollection.Count);

            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IPostRequestHandler<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultGenerator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(ICreator<TestEntity, TestParameters>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, TestParameters>)));
        }
    }
}
