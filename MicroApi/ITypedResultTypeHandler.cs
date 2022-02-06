using Microsoft.AspNetCore.Http;

namespace MicroApi;

public interface ITypedResultTypeHandler<TResult>
{
    IResult Handle(TResult successResult);
}
