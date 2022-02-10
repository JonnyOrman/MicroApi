using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Create;

public static class Registration
{
    public static IServiceCollection AddCreate<
        T,
        TKey,
        TParameters,
        TCreator
        >(this IServiceCollection serviceCollection)
        where T : Entity<TKey>
        where TCreator : class, IOperation<T, TParameters>
    {
        serviceCollection.AddMicroApi<
            T, 
            TKey,
            TParameters,
            TCreator,
            CreateFailedResultGenerator<T, TParameters>
        >();

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new ResultTypeHandler<T, SuccessResult<T>>(
                    new CreatedResultTypedHandler<T, TKey>()
                    ),
                new ResultTypeValidator<T, SuccessResult<T>>()
            ));

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new BadRequestResultHandler<T, TParameters>(
                    new InvalidParametersResultTypedHandler<T, TParameters>()
                ),
                new ResultTypeValidator<T, InvalidParametersResult<T, TParameters>>()
            ));

        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TParameters>, ValidParametersHandler<T, TParameters, IOperation<T, TParameters>>>(
            serviceProvider => new ValidParametersHandler<T, TParameters, IOperation<T, TParameters>>(
                serviceProvider.GetService<IOperation<T, TParameters>>(),
                (creator, parameters) => creator.ExecuteAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TParameters>>())
        );
        
        return serviceCollection;
    }
}
