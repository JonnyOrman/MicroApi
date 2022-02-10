using Microsoft.Extensions.DependencyInjection;

namespace MicroApi;

public static class Registration
{
    public static IServiceCollection AddMicroApi<
        T,
        TKey,
        TParameters,
        TOperation,
        TFailedResultGenerator
        >(this IServiceCollection serviceCollection)
        where T : Entity<TKey>
        where TOperation : class, IOperation<T, TParameters>
        where TFailedResultGenerator : class, IOperationFailedResultGenerator<T, TParameters>
    {
        serviceCollection.AddSingleton<IParametersProcessor<T, TParameters>, ValidationParametersProcessor<T, TParameters>>();

        serviceCollection.AddSingleton<IValidationRuleResultsCalculator<TParameters>, ValidationRuleResultsCalculator<TParameters>>();

        serviceCollection.AddSingleton<IValidationResultGenerator<TParameters>, ValidationResultGenerator<TParameters>>();

        serviceCollection.AddSingleton<IValidator<TParameters>, Validator<TParameters>>();

        serviceCollection.AddSingleton<IParametersValidationResultHandler<T, TParameters>, ParametersValidationResultHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IRequestHandler<TParameters>, RequestHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IResultHandler<T>, ResultHandler<T>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<T>, ResultTypeHandlerResolver<T>>();

        serviceCollection.AddSingleton<IValidationResultBuilderCreator, ValidationResultBuilderCreator>();

        serviceCollection.AddSingleton<IOperation<T, TParameters>, TOperation>();

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