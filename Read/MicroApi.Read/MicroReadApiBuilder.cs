using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Read;

public class MicroReadApiBuilder<T, TKey, TQuery, TSingleReader, TCollectionReader> : IMicroReadApiBuilder<T, TKey, TQuery>
    where T : Entity<TKey>
    where TSingleReader : class, IOperation<T, TKey>
    where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
{
    private readonly Action<IServiceCollection> _registerAdditionalServices;
    private readonly WebApplicationBuilder _builder;
    private IPropertyValidatorBuilder<TQuery> _currentQueryPropertyValidatorBuilder;

    public MicroReadApiBuilder(
        string[] args,
        Action<IServiceCollection> registerAdditionalServices
        )
    {
        _registerAdditionalServices = registerAdditionalServices;

        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddRead<T, TKey, TQuery, TSingleReader, TCollectionReader>();
    }

    public void Start()
    {
        if (_currentQueryPropertyValidatorBuilder != null)
        {
            var rules = _currentQueryPropertyValidatorBuilder.ToRules();

            foreach (var rule in rules)
            {
                _builder.Services.AddSingleton(rule);
            }
        }

        _registerAdditionalServices.Invoke(_builder.Services);

        var webApplication = _builder.Build();

        webApplication.UseHttpsRedirection();

        webApplication.MapGet("/{key}", async (TKey key, IRequestHandler<TKey> handler) => await handler.HandleAsync(key));
        webApplication.MapGet("/", async (TQuery query, IRequestHandler<TQuery> handler) => await handler.HandleAsync(query));

        webApplication.Run();
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> Where<TProperty>(Expression<Func<TQuery, TProperty>> propertyExpression)
    {
        if (_currentQueryPropertyValidatorBuilder != null)
        {
            var rules = _currentQueryPropertyValidatorBuilder.ToRules();

            foreach (var rule in rules)
            {
                _builder.Services.AddSingleton(rule);
            }
        }

        _currentQueryPropertyValidatorBuilder = new PropertyValidatorBuilder<TQuery, TProperty>(propertyExpression);

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> IsRequired()
    {
        _currentQueryPropertyValidatorBuilder.IsRequired();

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> MustPass<TValidationRule>() where TValidationRule : IValidationRule<TQuery>, new()
    {
        _currentQueryPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> OnGetSingleSuccess<TSuccessEvent>() where TSuccessEvent : class, IOperationSuccessEvent<T, TKey>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<T, TKey>, TSuccessEvent>();

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> OnGetSingleFailure<TFailedEvent>() where TFailedEvent : class, IOperationFailedEvent<TKey>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TKey>, TFailedEvent>();

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> OnGetCollectionSuccess<TSuccessEvent>() where TSuccessEvent : class, IOperationSuccessEvent<IEnumerable<T>, TQuery>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<IEnumerable<T>, TQuery>, TSuccessEvent>();

        return this;
    }

    public IMicroReadApiBuilder<T, TKey, TQuery> OnGetCollectionFailure<TFailedEvent>() where TFailedEvent : class, IOperationFailedEvent<TQuery>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TQuery>, TFailedEvent>();

        return this;
    }

    private Delegate GetSingleAction<TKey>()
    {
        return async (TKey key, IRequestHandler<TKey> handler) =>
        {
            return await handler.HandleAsync(key);
        };
    }

    private Delegate GetCollectionAction<TQuery>()
    {
        return async (TQuery query, IRequestHandler<TQuery> handler) =>
        {
            return await handler.HandleAsync(query);
        };
    }
}
