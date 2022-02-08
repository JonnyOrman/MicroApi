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

        serviceCollection.AddSingleton<IValidationResultBuilderCreator, ValidationResultBuilderCreator>();
        
        serviceCollection.AddSingleton<IInvalidResultHandler<T>, InvalidParametersResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IValidParametersHandler<T, TParameters>, ValidParametersHandler<T, TParameters, ICreator<T, TParameters>>>(
            serviceProvider => new ValidParametersHandler<T, TParameters, ICreator<T, TParameters>>(
                serviceProvider.GetService<ICreator<T, TParameters>>(),
                (creator, parameters) => creator.CreateAsync(parameters),
                serviceProvider.GetService<IOperationResultHandler<T, TParameters>>())
        );

        serviceCollection.AddSingleton<IOperationResultHandler<T, TParameters>, OperationResultHandler<T, TParameters>>();

        serviceCollection.AddSingleton<IOperationSuccessResultHandler<T, TParameters>, OperationSuccessResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationSuccessEventsRunner<T, TParameters>, OperationSuccessEventsRunner<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationSuccessResultGenerator<T>, OperationSuccessResultGenerator<T>>();

        serviceCollection.AddSingleton<IOperationFailedResultHandler<T, TParameters>, OperationFailedResultHandler<T, TParameters>>();
        serviceCollection.AddSingleton<IOperationFailedEventsRunner<TParameters>, OperationFailedEventsRunner<TParameters>>();
        serviceCollection.AddSingleton<IOperationFailedResultGenerator<T, TParameters>, CreateFailedResultGenerator<T, TParameters>>();
        
        return serviceCollection;
    }
}
