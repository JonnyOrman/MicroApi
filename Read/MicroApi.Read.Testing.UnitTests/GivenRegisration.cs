using MicroApi.Read.Testing.UnitTests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using XpressTest;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenRegisration
{
    [Fact]
    public void WhenServicesAreRegisteredToAServiceCollectionThenTheServicesAreAddedToTheServiceCollection() =>
        GivenA<ServiceCollection>
            .WhenIt(sut => sut.AddRead<TestEntity, string, TestQuery, TestSingleReader, TestCollectionReader>())
            .Then(assertion =>
            {
                Assert.Equal(41, assertion.Result.Count);

                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultBuilderCreator)));

                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IRequestHandler<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultGenerator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersProcessor<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IInvalidResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidParametersHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultHandler<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperation<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<TestEntity>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<TestEntity, string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<string>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<TestEntity, string>)));
                Assert.Equal(2, assertion.Result.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<TestEntity>)));

                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IRequestHandler<TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersValidationResultHandler<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidator<TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationResultGenerator<TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidationRuleResultsCalculator<TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IParametersProcessor<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IInvalidResultHandler<IEnumerable<TestEntity>>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IValidParametersHandler<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultTypeHandlerResolver<IEnumerable<TestEntity>>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IResultHandler<IEnumerable<TestEntity>>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperation<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationResultHandler<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultHandler<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessEventsRunner<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationSuccessResultGenerator<IEnumerable<TestEntity>>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultHandler<IEnumerable<TestEntity>, TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedEventsRunner<TestQuery>)));
                Assert.NotNull(assertion.Result.Single(x => x.ServiceType == typeof(IOperationFailedResultGenerator<IEnumerable<TestEntity>, TestQuery>)));
                Assert.Equal(2, assertion.Result.Count(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResultTypeHandlerRegistration<IEnumerable<TestEntity>>)));
            });
}
