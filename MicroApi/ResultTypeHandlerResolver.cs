namespace MicroApi;

public class ResultTypeHandlerResolver<T> : IResultTypeHandlerResolver<T>
{
    private readonly IEnumerable<IResultTypeHandlerRegistration<T>> _handlerRegistrations;

    public ResultTypeHandlerResolver(
        IEnumerable<IResultTypeHandlerRegistration<T>> handlerRegistrations
    )
    {
        _handlerRegistrations = handlerRegistrations;
    }

    public IResultTypeHandler<T> Resolve(Result<T> result)
    {
        var matchingHandlerRegistrations = _handlerRegistrations.Where(x => x.IsMatch(result));

        if (matchingHandlerRegistrations.Count() == 0)
        {
            throw new NotImplementedException("No matching handler registration for  result");
        }

        if (matchingHandlerRegistrations.Count() > 1)
        {
            //TODO - warning that multiple matches found
        }

        return matchingHandlerRegistrations.First().Handler;
    }
}
