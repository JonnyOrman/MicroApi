namespace MicroApi.Create;

public interface ICreator<T, TParameters>
{
    Task<T> Create(TParameters parameters);
}