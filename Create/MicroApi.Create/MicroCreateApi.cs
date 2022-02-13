using Microsoft.Extensions.DependencyInjection;

namespace MicroApi.Create;
public static class MicroCreateApi
{
    public static MicroCreateApiBuilder<T, TKey, TParameters, TCreator> New<T, TKey, TParameters, TCreator>(string[] args)
        where T : Entity<TKey>
        where TCreator : class, IOperation<T, TParameters>
    {
        return New<T, TKey, TParameters, TCreator>(args, serviceCollection => { });
    }

    public static MicroCreateApiBuilder<T, TKey, TParameters, TCreator> New<T, TKey, TParameters, TCreator>(string[] args, Action<IServiceCollection> registerAdditionalServices)
        where T : Entity<TKey>
        where TCreator : class, IOperation<T, TParameters>
    {
        var microCreateApiBuilder = new MicroCreateApiBuilder<T, TKey, TParameters, TCreator>(args, registerAdditionalServices);
        
        return microCreateApiBuilder;
    }
}