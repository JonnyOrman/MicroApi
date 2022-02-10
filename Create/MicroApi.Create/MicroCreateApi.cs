namespace MicroApi.Create;
public static class MicroCreateApi
{
    public static MicroCreateApiBuilder<T, TKey, TParameters, TCreator> New<T, TKey, TParameters, TCreator>(string[] args)
        where T : Entity<TKey>
        where TCreator : class, IOperation<T, TParameters>
    {
        var microCreateApiBuilder = new MicroCreateApiBuilder<T, TKey, TParameters, TCreator>(args);
        
        return microCreateApiBuilder;
    }
}