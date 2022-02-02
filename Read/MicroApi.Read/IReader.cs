namespace MicroApi.Read;

public interface IReader<T, TKey, TQuery> 
:
 ISingleReader<T, TKey>, 
 ICollectionReader<T, TQuery>
{
}