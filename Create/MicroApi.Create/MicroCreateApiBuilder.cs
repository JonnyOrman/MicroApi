using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Create;

public class MicroCreateApiBuilder<T, TKey, TParameters, TCreator> : IMicroCreateApiBuilder<T, TParameters>
    where T : Entity<TKey>
    where TCreator : class, IOperation<T, TParameters>
{
    private readonly Action<IServiceCollection> _registerAdditionalServices;
    private readonly WebApplicationBuilder _builder;
    private IPropertyValidatorBuilder<TParameters> _currentPropertyValidatorBuilder;

    public MicroCreateApiBuilder(
        string[] args,
        Action<IServiceCollection> registerAdditionalServices
        )
    {
        _registerAdditionalServices = registerAdditionalServices;

        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddCreate<T, TKey, TParameters, TCreator>();
    }

    private Delegate PostAction<TParameters>()
    {
        return async (TParameters parameters, IRequestHandler<TParameters> handler) =>
        {
            return await handler.HandleAsync(parameters);
        };
    }

    public IMicroCreateApiBuilder<T, TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression)
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

    public IMicroCreateApiBuilder<T, TParameters> IsRequired()
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

        webApplication.MapPost("/", PostAction<TParameters>());

        webApplication.Run();
    }

    public IMicroCreateApiBuilder<T, TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new()
    {
        _currentPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }

    public IMicroCreateApiBuilder<T, TParameters> OnSuccess<TSuccessEvent>()
        where TSuccessEvent : class, IOperationSuccessEvent<T, TParameters>
    {
        _builder.Services.AddSingleton<IOperationSuccessEvent<T, TParameters>, TSuccessEvent>();

        return this;
    }

    public IMicroCreateApiBuilder<T, TParameters> OnFailure<TFailedEvent>()
        where TFailedEvent : class, IOperationFailedEvent<TParameters>
    {
        _builder.Services.AddSingleton<IOperationFailedEvent<TParameters>, TFailedEvent>();

        return this;
    }
}
