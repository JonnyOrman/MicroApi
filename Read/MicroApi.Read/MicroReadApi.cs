using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Read;
public static class MicroReadApi
{
    public static void Start<T, TKey, TQuery, TReader>(string [] args)
        where T : Entity<TKey>
        where TReader : class, IReader<T, TKey, TQuery>
    {
        Start<T, TKey, TQuery>(args, serviceCollection => {
            serviceCollection.AddSingleton<TReader>();
            serviceCollection.AddSingleton<ISingleReader<T, TKey>>(serviceCollection => serviceCollection.GetService<TReader>());
            serviceCollection.AddSingleton<ICollectionReader<T, TQuery>>(serviceCollection => serviceCollection.GetService<TReader>());
        });
    }

    public static void Start<T, TKey, TQuery, TReader>(string [] args, Action<IServiceCollection> servicesSetup)
        where T : Entity<TKey>
        where TReader : class, IReader<T, TKey, TQuery>
    {
        Start<T, TKey, TQuery>(args, serviceCollection => {
            serviceCollection.AddSingleton<TReader>();
            serviceCollection.AddSingleton<ISingleReader<T, TKey>>(serviceCollection => serviceCollection.GetService<TReader>());
            serviceCollection.AddSingleton<ICollectionReader<T, TQuery>>(serviceCollection => serviceCollection.GetService<TReader>());
            servicesSetup.Invoke(serviceCollection);
        });
    }

    public static void Start<T, TKey, TQuery, TSingleReader, TCollectionReader>(string [] args)
        where T : Entity<TKey>
        where TSingleReader : class, ISingleReader<T, TKey>
        where TCollectionReader : class, ICollectionReader<T, TQuery>
    {
        Start<T, TKey, TQuery>(args, serviceCollection => {
            serviceCollection.AddSingleton<ISingleReader<T, TKey>, TSingleReader>();
            serviceCollection.AddSingleton<ICollectionReader<T, TQuery>, TCollectionReader>();
        });
    }

    public static void Start<T, TKey, TQuery, TSingleReader, TCollectionReader>(string [] args, Action<IServiceCollection> servicesSetup)
        where T : Entity<TKey>
        where TSingleReader : class, ISingleReader<T, TKey>
        where TCollectionReader : class, ICollectionReader<T, TQuery>
    {
        Start<T, TKey, TQuery>(args, serviceCollection => {
            serviceCollection.AddSingleton<ISingleReader<T, TKey>, TSingleReader>();
            serviceCollection.AddSingleton<ICollectionReader<T, TQuery>, TCollectionReader>();
            servicesSetup.Invoke(serviceCollection);
        });
    }

    public static void Start<T, TKey, TQuery>(string [] args, Action<IServiceCollection> servicesSetup)
        where T : Entity<TKey>
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRead<T, TKey, TQuery>();

        servicesSetup.Invoke(builder.Services);

        var app = builder.Build();

        app.UseHttpsRedirection();
        
        app.MapGet("/", async (TQuery query, IGetManyRequestHandler<TQuery> handler) => await handler.HandleAsync(query));

        app.MapGet("/{key}", async (TKey key, IGetSingleRequestHandler<TKey> handler) => await handler.HandleAsync(key));

        app.Run();
    }
}