using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Update;

public class MicroUpdateApiBuilder<T, TKey, TParameters, TUpdater> : IMicroUpdateApiBuilder<T, TParameters>
    where T : Entity<TKey>
    where TParameters : Parameters<TKey>
    where TUpdater : class, IOperation<T, TParameters>
{
    private readonly Action<IServiceCollection> _registerAdditionalServices;
    private readonly WebApplicationBuilder _builder;
    private IPropertyValidatorBuilder<TParameters> _currentPropertyValidatorBuilder;

    public MicroUpdateApiBuilder(
        string[] args,
        Action<IServiceCollection> registerAdditionalServices
        )
    {
        _registerAdditionalServices = registerAdditionalServices;

        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddUpdate<T, TKey, TParameters, TUpdater>();
    }

    private Delegate PatchAction<TParameters>() where TParameters : Parameters<TKey>
    {
        return async (TKey key,TParameters parameters, IRequestHandler<TParameters> handler) =>
        {
            parameters.Key = key;

            return await handler.HandleAsync(parameters);
        };
    }

    public IMicroUpdateApiBuilder<T, TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression)
    {
        if (_currentPropertyValidatorBuilder != null)
        {
            var rules = _currentPropertyValidatorBuilder.ToRules();

            foreach (var rule in rules)
            {
                _builder.Services.AddSingleton(rule);
            }
        }

        _currentPropertyValidatorBuilder = new PropertyValidatorBuilder<TParameters, TProperty>(propertyExpression);

        return this;
    }

    public IMicroUpdateApiBuilder<T, TParameters> IsRequired()
    {
        _currentPropertyValidatorBuilder.IsRequired();

        return this;
    }

    public void Start()
    {
        if (_currentPropertyValidatorBuilder != null)
        {
            var rules = _currentPropertyValidatorBuilder.ToRules();

            foreach (var rule in rules)
            {
                _builder.Services.AddSingleton(rule);
            }
        }

        _registerAdditionalServices.Invoke(_builder.Services);

        var webApplication = _builder.Build();

        webApplication.UseHttpsRedirection();

        webApplication.MapMethods("/{key}", new List<string>{"PATCH"}, PatchAction<TParameters>());

        webApplication.Run();
    }

    public IMicroUpdateApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new()
    {
        _currentPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }

    public IMicroUpdateApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<T, TParameters>, TSuccessEvent>();

        return this;
    }

    public IMicroUpdateApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TParameters>, TFailedEvent>();

        return this;
    }
}

