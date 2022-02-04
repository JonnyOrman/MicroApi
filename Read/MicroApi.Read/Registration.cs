using MicroApi.Core;
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

        return serviceCollection;
    }
}