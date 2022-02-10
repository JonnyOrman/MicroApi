using Microsoft.AspNetCore.Http;

namespace MicroApi;

public class RequestHandler<T, TParameters> : IRequestHandler<TParameters>
{
    private readonly IParametersProcessor<T, TParameters> _parametersProcessor;
    private readonly IResultHandler<T> _resultHandler;

    public RequestHandler(
        IParametersProcessor<T, TParameters> parametersProcessor,
        IResultHandler<T> resultHandler
    )
    {
        _parametersProcessor = parametersProcessor;
        _resultHandler = resultHandler;
    }

    public async Task<IResult> HandleAsync(TParameters parameters)
    {
        var result = await _parametersProcessor.ProcessAsync(parameters);

        return _resultHandler.Handle(result);
    }
}
