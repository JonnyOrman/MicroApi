namespace MicroApi;

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(T entity)
        :
        base(
            true,
            "Success!"
            )
    {
        Entity = entity;
    }

    public T Entity { get; set; }

}
