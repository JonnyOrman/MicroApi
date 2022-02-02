using MicroApi.Core;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Create;

public class PostRequestHandler<T, TParameters> : IPostRequestHandler<TParameters>
{
    private readonly IParametersProcessor<T, TParameters> _parametersProcessor;
    private readonly IResultHandler<T> _resultHandler;

    public PostRequestHandler(
        IParametersProcessor<T, TParameters> parametersProcessor,
        IResultHandler<T> resultHandler
        ) 
    {
        _parametersProcessor = parametersProcessor;
        _resultHandler = resultHandler;
    }

    public async Task<IResult> HandleAsync(TParameters parameters)
    {
        var createResult = await _parametersProcessor.ProcessAsync(parameters);

        return _resultHandler.Handle(createResult);
    }
}
