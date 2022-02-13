using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Read;

public static class MicroReadApi
{
    public static MicroReadApiBuilder<T, TKey, TQuery, TSingleReader, TCollectionReader> New<T, TKey, TQuery, TSingleReader, TCollectionReader>(string[] args)
        where T : Entity<TKey>
        where TSingleReader : class, IOperation<T, TKey>
        where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
    {
        return New<T, TKey, TQuery, TSingleReader, TCollectionReader>(args,serviceCollection => { });
    }
    public static MicroReadApiBuilder<T, TKey, TQuery, TSingleReader, TCollectionReader> New<T, TKey, TQuery, TSingleReader, TCollectionReader>(string[] args, Action<IServiceCollection> registerAdditionalServices)
        where T : Entity<TKey>
        where TSingleReader : class, IOperation<T, TKey>
        where TCollectionReader : class, IOperation<IEnumerable<T>, TQuery>
    {
        var microCreateApiBuilder = new MicroReadApiBuilder<T, TKey, TQuery, TSingleReader, TCollectionReader>(args, registerAdditionalServices);

        return microCreateApiBuilder;
    }
}