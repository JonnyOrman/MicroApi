using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Read;

public static class Registration
{
    public static IServiceCollection AddRead<
        T,
        TKey,
        TQuery,
        TSingleReader,
        TCollectionReader
        >(this IServiceCollection serviceCollection)
        where T : Entity<TKey>
        where TSingleReader : class, IOperation<T, TKey>
        where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
    {
        serviceCollection.AddMicroApi<
            T, 
            TKey,
            TKey,
            TSingleReader,
            ReadSingleFailedResultGenerator<T, TKey>
        >();

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

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<IEnumerable<T>>>(
            new ResultTypeHandlerRegistration<IEnumerable<T>>(
                new ResultTypeHandler<IEnumerable<T>, SuccessResult<IEnumerable<T>>>(
                    new SuccessResultTypedHandler<IEnumerable<T>>()
                    ),
                new ResultTypeValidator<IEnumerable<T>, SuccessResult<IEnumerable<T>>>()
            ));

        serviceCollection.AddSingleton<IOperation<IEnumerable<T>, TQuery>, TCollectionReader>();

        serviceCollection.AddSingleton<IResultHandler<IEnumerable<T>>, ResultHandler<IEnumerable<T>>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<IEnumerable<T>>, ResultTypeHandlerResolver<IEnumerable<T>>>();
        
        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TKey>, ValidParametersHandler<T, TKey, IOperation<T, TKey>>>(
            serviceProvider => new ValidParametersHandler<T, TKey, IOperation<T, TKey>>(
                serviceProvider.GetService<IOperation<T, TKey>>(),
                (reader, parameters) => reader.ExecuteAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TKey>>())
            );

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<IEnumerable<T>>>(
            new ResultTypeHandlerRegistration<IEnumerable<T>>(
                new BadRequestResultHandler<IEnumerable<T>, TQuery>(
                    new InvalidParametersResultTypedHandler<IEnumerable<T>, TQuery>()
                ),
                new ResultTypeValidator<IEnumerable<T>, InvalidParametersResult<IEnumerable<T>, TQuery>>()
            ));

        serviceCollection.AddSingleton<IRequestHandler<TQuery>, RequestHandler<IEnumerable<T>, TQuery>>();

        serviceCollection.AddSingleton<IParametersProcessor<IEnumerable<T>, TQuery>, ValidationParametersProcessor<IEnumerable<T>, TQuery>>();
        
        serviceCollection.AddSingleton<IValidationRuleResultsCalculator<TQuery>, ValidationRuleResultsCalculator<TQuery>>();

        serviceCollection.AddSingleton<IValidationResultGenerator<TQuery>, ValidationResultGenerator<TQuery>>();

        serviceCollection.AddSingleton<IValidator<TQuery>, Validator<TQuery>>();

        serviceCollection.AddSingleton<IParametersValidationResultHandler<IEnumerable<T>, TQuery>, ParametersValidationResultHandler<IEnumerable<T>, TQuery>>();

        serviceCollection.AddSingleton<IInvalidResultHandler<IEnumerable<T>>, InvalidParametersResultHandler<IEnumerable<T>, TQuery>>();
        serviceCollection.AddSingleton<IValidParametersHandler<IEnumerable<T>, TQuery>, ValidParametersHandler<IEnumerable<T>, TQuery, IOperation<IEnumerable<T>, TQuery>>>(
            serviceProvider => new ValidParametersHandler<IEnumerable<T>, TQuery, IOperation<IEnumerable<T>, TQuery>>(
                serviceProvider.GetService<IOperation<IEnumerable<T>, TQuery>>(),
                (reader, parameters) => reader.ExecuteAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<IEnumerable<T>, TQuery>>())
        );

        serviceCollection.AddSingleton<IOperationResultHandler<IEnumerable<T>, TQuery>, OperationResultHandler<IEnumerable<T>, TQuery>>();

        serviceCollection.AddSingleton<IOperationSuccessResultHandler<IEnumerable<T>, TQuery>, OperationSuccessResultHandler<IEnumerable<T>, TQuery>>();
        serviceCollection.AddSingleton<IOperationSuccessEventsRunner<IEnumerable<T>, TQuery>, OperationSuccessEventsRunner<IEnumerable<T>, TQuery>>();
        serviceCollection.AddSingleton<IOperationSuccessResultGenerator<IEnumerable<T>>, OperationSuccessResultGenerator<IEnumerable<T>>>();

        serviceCollection.AddSingleton<IOperationFailedResultHandler<IEnumerable<T>, TQuery>, OperationFailedResultHandler<IEnumerable<T>, TQuery>>();
        serviceCollection.AddSingleton<IOperationFailedEventsRunner<TQuery>, OperationFailedEventsRunner<TQuery>>();
        serviceCollection.AddSingleton<IOperationFailedResultGenerator<IEnumerable<T>, TQuery>, ReadCollectionFailedResultGenerator<IEnumerable<T>, TQuery>>();

        return serviceCollection;
    }
}