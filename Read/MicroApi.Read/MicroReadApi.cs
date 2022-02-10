using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Read;

public static class MicroReadApi
{
    public static void Start<T, TKey, TQuery, TSingleReader, TCollectionReader>(string [] args)
        where T : Entity<TKey>
        where TSingleReader : class, IOperation<T, TKey>
        where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
    {
        Start<T, TKey, TQuery, TSingleReader, TCollectionReader>(args, serviceCollection => {});
    }

    public static void Start<T, TKey, TQuery, TSingleReader, TCollectionReader>(string [] args, Action<IServiceCollection> servicesSetup)
        where T : Entity<TKey>
        where TSingleReader : class, IOperation<T, TKey>
        where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRead<T, TKey, TQuery, TSingleReader, TCollectionReader>();

        servicesSetup.Invoke(builder.Services);

        var app = builder.Build();

        app.UseHttpsRedirection();
        
        app.MapGet("/", async (TQuery query, IGetManyRequestHandler<TQuery> handler) => await handler.HandleAsync(query));

        app.MapGet("/{key}", async (TKey key, IRequestHandler<TKey> handler) => await handler.HandleAsync(key));

        app.Run();
    }
}