using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace MicroApi.Create;

public class MicroCreateApiBuilder<T, TKey, TParameters, TCreator> : IMicroApiBuilder<TParameters>
    where T : Entity<TKey>
    where TCreator : class, ICreator<T, TParameters>
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
        return async (TParameters parameters, IPostRequestHandler<TParameters> handler) =>
        {
            return await handler.HandleAsync(parameters);
        };
    }

    public IMicroApiBuilder<TParameters> Where<TProperty>(Expression<Func<TParameters, TProperty>> propertyExpression)
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

    public IMicroApiBuilder<TParameters> IsRequired()
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

    public IMicroApiBuilder<TParameters> MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<TParameters>, new()
    {
        _currentPropertyValidatorBuilder.MustPass<TValidationRule>();

        return this;
    }
}
