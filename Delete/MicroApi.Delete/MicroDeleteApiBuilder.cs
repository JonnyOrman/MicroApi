using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Delete;

public class MicroDeleteApiBuilder<T, TKey, TDeleter> : IMicroDeleteApiBuilder<T, TKey>
    where T : Entity<TKey>
    where TDeleter : class, IOperation<T, TKey>
{
    private readonly Action<IServiceCollection> _registerAdditionalServices;
    private readonly WebApplicationBuilder _builder;
    private IPropertyValidatorBuilder<TKey> _currentPropertyValidatorBuilder;

    public MicroDeleteApiBuilder(
        string[] args,
        Action<IServiceCollection> registerAdditionalServices
        )
    {
        _registerAdditionalServices = registerAdditionalServices;

        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddDelete<T, TKey, TDeleter>();
    }

    private Delegate DeleteAction<TKey>()
    {
        return async (TKey key, IRequestHandler<TKey> handler) =>
        {
            return await handler.HandleAsync(key);
        };
    }

    public IMicroDeleteApiBuilder<T, TKey> Where<TProperty>(Expression<Func<TKey, TProperty>> propertyExpression)
    {
        if (_currentPropertyValidatorBuilder != null)
        {
            var rules = _currentPropertyValidatorBuilder.ToRules();

            foreach (var rule in rules)
            {
                _builder.Services.AddSingleton(rule);
            }
        }

        _currentPropertyValidatorBuilder = new PropertyValidatorBuilder<TKey, TProperty>(propertyExpression);

        return this;
    }

    public IMicroDeleteApiBuilder<T, TKey> IsRequired()
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

        webApplication.MapDelete("/{key}", DeleteAction<TKey>());

        webApplication.Run();
    }

    public IMicroDeleteApiBuilder<T, TKey> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TKey>, new()
    {
        _currentPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }

    public IMicroDeleteApiBuilder<T, TKey> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TKey>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<T, TKey>, TSuccessEvent>();

        return this;
    }

    public IMicroDeleteApiBuilder<T, TKey> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TKey>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TKey>, TFailedEvent>();

        return this;
    }
}

