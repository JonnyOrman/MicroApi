using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Delete;

public static class MicroDeleteApi
{
    public static MicroDeleteApiBuilder<T, TKey, TDeleter> New<T, TKey, TDeleter>(string[] args)
        where T : Entity<TKey>
        where TDeleter : class, IOperation<T, TKey>
    {
        return New<T, TKey, TDeleter>(args, serviceCollection => { });
    }

    public static MicroDeleteApiBuilder<T, TKey, TDeleter> New<T, TKey, TDeleter>(string[] args, Action<IServiceCollection> registerAdditionalServices)
        where T : Entity<TKey>
        where TDeleter : class, IOperation<T, TKey>
    {
        var microDeleteApiBuilder = new MicroDeleteApiBuilder<T, TKey, TDeleter>(args, registerAdditionalServices);

        return microDeleteApiBuilder;
    }
}
