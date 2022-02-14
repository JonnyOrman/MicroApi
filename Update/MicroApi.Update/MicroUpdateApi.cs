using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Update;

public static class MicroUpdateApi
{
    public static MicroUpdateApiBuilder<T, TKey, TParameters, TUpdater> New<T, TKey, TParameters, TUpdater>(string[] args)
        where T : Entity<TKey>
        where TParameters : Parameters<TKey>
        where TUpdater : class, IOperation<T, TParameters>
    {
        return New<T, TKey, TParameters, TUpdater>(args, serviceCollection => { });
    }

    public static MicroUpdateApiBuilder<T, TKey, TParameters, TUpdater> New<T, TKey, TParameters, TUpdater>(string[] args, Action<IServiceCollection> registerAdditionalServices)
        where T : Entity<TKey>
        where TParameters : Parameters<TKey>
        where TUpdater : class, IOperation<T, TParameters>
    {
        var microUpdateApiBuilder = new MicroUpdateApiBuilder<T, TKey, TParameters, TUpdater>(args, registerAdditionalServices);

        return microUpdateApiBuilder;
    }
}
