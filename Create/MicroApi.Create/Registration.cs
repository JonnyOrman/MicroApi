using MicroApi.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Create;

public static class Registration
{
    public static IServiceCollection AddCreate<
        T,
        TParameters,
        TCreator
        >(this IServiceCollection serviceCollection)
        where TCreator : class, ICreator<T, TParameters>
    {
        serviceCollection.AddSingleton<ICreator<T, TParameters>, TCreator>();

        serviceCollection.AddSingleton<IParametersProcessor<T, TParameters>, ValidationParametersProcessor<T, TParameters>>();

        serviceCollection.AddSingleton<IValidationRuleResultsCalculator<TParameters>, ValidationRuleResultsCalculator<TParameters>>();

        serviceCollection.AddSingleton<IValidationResultGenerator<TParameters>, ValidationResultGenerator<TParameters>>();

        serviceCollection.AddSingleton<IValidator<TParameters>, Validator<TParameters>>();

        serviceCollection.AddSingleton<IParametersValidationResultHandler<T, TParameters>, ParametersValidationResultHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IPostRequestHandler<TParameters>, PostRequestHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IResultHandler<T>, ResultHandler<T>>();
        serviceCollection.AddSingleton<IResultTypeHandlerResolver<T>, ResultTypeHandlerResolver<T>>();

        serviceCollection.AddSingleton<IResultTypeHandlerRegistration<T>>(
            new ResultTypeHandlerRegistration<T>(
                new SuccessResultHandler<T>(
                    new SuccessResultTypedHandler<T>()
                    ),
                new ResultTypeValidator<T, SuccessResult<T>>()
            ));

        return serviceCollection;
    }
}
