namespace MicroApi.Create;

public interface ICreator<T, TParameters>
{
    Task<T> CreateAsync(TParameters parameters);
}