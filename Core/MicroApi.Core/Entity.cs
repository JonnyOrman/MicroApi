namespace MicroApi.Core;

public abstract class Entity<TKey>
{
    public Entity(
        TKey key
        )
    {
        Key = key;
    }

    public TKey Key { get; }
}
