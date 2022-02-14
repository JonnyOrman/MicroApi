using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Delete;

public static class Registration
{
    public static IServiceCollection AddDelete<
        T,
        TKey,
        TDeleter
    >(this IServiceCollection serviceCollection)
        where T : Entity<TKey>
        where TDeleter : class, IOperation<T, TKey>
    {
        serviceCollection.AddMicroApi<
            T,
            TKey,
            TKey,
            TDeleter,
            DeleteFailedResultGenerator<T, TKey>
        >();

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new ResultTypeHandler<T, SuccessResult<T>>(
                    new DeletedResultTypedHandler<T, TKey>()
                ),
                new ResultTypeValidator<T, SuccessResult<T>>()
            ));

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new BadRequestResultHandler<T, TKey>(
                    new InvalidParametersResultTypedHandler<T, TKey>()
                ),
                new ResultTypeValidator<T, InvalidParametersResult<T, TKey>>()
            ));

        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TKey>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TKey>, ValidParametersHandler<T, TKey, IOperation<T, TKey>>>(
            serviceProvider => new ValidParametersHandler<T, TKey, IOperation<T, TKey>>(
                serviceProvider.GetService<IOperation<T, TKey>>(),
                (deleter, parameters) => deleter.ExecuteAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TKey>>())
        );

        return serviceCollection;
    }
}
