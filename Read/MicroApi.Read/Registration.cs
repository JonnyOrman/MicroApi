using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Read;

public static class Registration
{
    public static IServiceCollection AddRead<T, TKey, TQuery>(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IResultHandler<T>, ResultHandler<T>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<T>, ResultTypeHandlerResolver<T>>();

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new ResultTypeHandler<T, SuccessResult<T>>(
                    new SuccessResultTypedHandler<T>()
                    ),
                new ResultTypeValidator<T, SuccessResult<T>>()
            ));

            serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new NotFoundResultHandler<T, TKey>(
                    new NotFoundResultTypedHandler<T, TKey>()
                    ),
                new ResultTypeValidator<T, NotFoundResult<T, TKey>>()
            ));

        serviceCollection.AddSingleton<IResultHandler<IEnumerable<T>>, ResultHandler<IEnumerable<T>>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<IEnumerable<T>>, ResultTypeHandlerResolver<IEnumerable<T>>>();
        
        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<IEnumerable<T>>>(
            new ResultTypeHandlerRegistration<IEnumerable<T>>(
                new ResultTypeHandler<IEnumerable<T>, SuccessResult<IEnumerable<T>>>(
                    new SuccessResultTypedHandler<IEnumerable<T>>()
                    ),
                new ResultTypeValidator<IEnumerable<T>, SuccessResult<IEnumerable<T>>>()
            ));

        serviceCollection.AddSingleton<IParametersProcessor<T, TKey>, ValidationParametersProcessor<T, TKey>>();

        serviceCollection.AddSingleton<IValidationRuleResultsCalculator<TKey>, ValidationRuleResultsCalculator<TKey>>();

        serviceCollection.AddSingleton<IValidationResultGenerator<TKey>, ValidationResultGenerator<TKey>>();

        serviceCollection.AddSingleton<IValidator<TKey>, Validator<TKey>>();

        serviceCollection.AddSingleton<IParametersValidationResultHandler<T, TKey>, ParametersValidationResultHandler<T, TKey>>();

        serviceCollection.AddSingleton<ICollectionProvider<T, TQuery>, CollectionProvider<T, TQuery>>();

        serviceCollection.AddSingleton<IGetSingleRequestHandler<TKey>, GetSingleRequestHandler<T, TKey>>();
        serviceCollection.AddSingleton<IGetManyRequestHandler<TQuery>, GetManyRequestHandler<T, TQuery>>();

        serviceCollection.AddSingleton<IValidationResultBuilderCreator, ValidationResultBuilderCreator>();

        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TKey>, ValidParametersHandler<T, TKey, ISingleReader<T, TKey>>>(
            serviceProvider => new ValidParametersHandler<T, TKey, ISingleReader<T, TKey>>(
                serviceProvider.GetService<ISingleReader<T, TKey>>(),
                (reader, parameters) => reader.ReadAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TKey>>())
            );

        serviceCollection.AddSingleton<IOperationResultHandler<T, TKey>, OperationResultHandler<T, TKey>>();

        serviceCollection.AddSingleton<IOperationSuccessResultHandler<T, TKey>, OperationSuccessResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IOperationSuccessEventsRunner<T, TKey>, OperationSuccessEventsRunner<T, TKey>>();
        serviceCollection.AddSingleton<IOperationSuccessResultGenerator<T>, OperationSuccessResultGenerator<T>>();

        serviceCollection.AddSingleton<IOperationFailedResultHandler<T, TKey>, OperationFailedResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IOperationFailedEventsRunner<TKey>, OperationFailedEventsRunner<TKey>>();
        serviceCollection.AddSingleton<IOperationFailedResultGenerator<T, TKey>, ReadSingleFailedResultGenerator<T, TKey>>();
        
        return serviceCollection;
    }
}