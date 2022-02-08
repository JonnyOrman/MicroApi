﻿using MicroApi.Read.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenRegisration
{
    public class WhenServicesAreRegisteredToAServiceCollection
    {
        [Fact]
        public void ThenTheServicesAreAddedToTheServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddRead<TestEntity, string, TestQuery>();

            Assert.Equal(25, serviceCollection.Count);

            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IGetManyRequestHandler<TestQuery>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IGetSingleRequestHandler<string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(ICollectionProvider<TestEntity, TestQuery>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidator<string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultGenerator<string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerRegistration<IEnumerable<TestEntity>>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<IEnumerable<TestEntity>>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultHandler<IEnumerable<TestEntity>>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationResultHandler<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<TestEntity>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<TestEntity, string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<string>)));
            Assert.NotNull(serviceCollection.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<TestEntity, string>)));
            Assert.Equal(2, serviceCollection.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));
        }
    }
}
