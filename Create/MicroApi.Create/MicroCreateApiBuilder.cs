using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Create;

public class MicroCreateApiBuilder<T, TKey, TParameters, TCreator> : IMicroApiBuilder<T, TParameters>
    where T : Entity<TKey>
    where TCreator : class, IOperation<T, TParameters>
{
    private readonly WebApplicationBuilder _builder;
    private IPropertyValidatorBuilder<TParameters> _currentPropertyValidatorBuilder;

    public MicroCreateApiBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddCreate<T, TKey, TParameters, TCreator>();

        _builder.Services.AddSingleton(PostAction<TParameters>());
    }

    private Delegate PostAction<TParameters>()
    {
        return async (TParameters parameters, IRequestHandler<TParameters> handler) =>
        {
            return await handler.HandleAsync(parameters);
        };
    }

    public IMicroApiBuilder<T, TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression)
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

    public IMicroApiBuilder<T, TParameters> IsRequired()
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

        var webApplication = _builder.Build();

        webApplication.UseHttpsRedirection();

        webApplication.MapPost("/", PostAction<TParameters>());

        webApplication.Run();
    }

    public IMicroApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new()
    {
        _currentPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }

    public IMicroApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<T, TParameters>, TSuccessEvent>();

        return this;
    }

    public IMicroApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TParameters>, TFailedEvent>();

        return this;
    }
}
