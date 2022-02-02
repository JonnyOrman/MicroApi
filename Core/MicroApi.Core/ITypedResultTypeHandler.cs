using Microsoft.AspNetCore.Http;

namespace MicroApi.Core;

public interface ITypedResultTypeHandler<TResult>
{
    IResult Handle(TResult successResult);
}
