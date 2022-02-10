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
        
        serviceCollection.AddSingleton<ICollectionProvider<T, TQuery>, CollectionProvider<T, TQuery>>();

        serviceCollection.AddSingleton<IGetManyRequestHandler<TQuery>, GetManyRequestHandler<T, TQuery>>();

        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TKey>, ValidParametersHandler<T, TKey, IOperation<T, TKey>>>(
            serviceProvider => new ValidParametersHandler<T, TKey, IOperation<T, TKey>>(
                serviceProvider.GetService<IOperation<T, TKey>>(),
                (reader, parameters) => reader.ExecuteAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TKey>>())
            );

        return serviceCollection;
    }
}