using Microsoft.Extensions.DependencyInjection;

namespace MicroApi;

public static class Registration
{
    public static IServiceCollection AddMicroApi<
        T,
        TKey,
        TParameters,
        TRequestHandlerAbstraction,
        TRequestHandler,
        TFailedResultGenerator
        >(this IServiceCollection serviceCollection)
        where T : Entity<TKey>
        where TRequestHandlerAbstraction : class
        where TRequestHandler : class, TRequestHandlerAbstraction
        where TFailedResultGenerator : class, IOperationFailedResultGenerator<T, TParameters>
    {
        serviceCollection.AddSingleton<IParametersProcessor<T, TParameters>, ValidationParametersProcessor<T, TParameters>>();

        serviceCollection.AddSingleton<IValidationRuleResultsCalculator<TParameters>, ValidationRuleResultsCalculator<TParameters>>();

        serviceCollection.AddSingleton<IValidationResultGenerator<TParameters>, ValidationResultGenerator<TParameters>>();

        serviceCollection.AddSingleton<IValidator<TParameters>, Validator<TParameters>>();

        serviceCollection.AddSingleton<IParametersValidationResultHandler<T, TParameters>, ParametersValidationResultHandler<T, TParameters>>();

        serviceCollection.AddSingleton<TRequestHandlerAbstraction, TRequestHandler>();

        serviceCollection.AddSingleton<IResultHandler<T>, ResultHandler<T>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<T>, ResultTypeHandlerResolver<T>>();

        serviceCollection.AddSingleton<IValidationResultBuilderCreator, ValidationResultBuilderCreator>();

        serviceCollection.AddSingleton<IOperationResultHandler<T, TParameters>, OperationResultHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IOperationSuccessResultHandler<T, TParameters>, OperationSuccessResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationSuccessEventsRunner<T, TParameters>, OperationSuccessEventsRunner<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationSuccessResultGenerator<T>, OperationSuccessResultGenerator<T>>();

        serviceCollection.AddSingleton<IOperationFailedResultHandler<T, TParameters>, OperationFailedResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationFailedEventsRunner<TParameters>, OperationFailedEventsRunner<TParameters>>();
        serviceCollection.AddSingleton<IOperationFailedResultGenerator<T, TParameters>, TFailedResultGenerator>();

        return serviceCollection;
    }
}