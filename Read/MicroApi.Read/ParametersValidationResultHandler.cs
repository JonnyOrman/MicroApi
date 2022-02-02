using MicroApi.Core;

namespace MicroApi.Read;

public class ParametersValidationResultHandler<T, TKey> : IParametersValidationResultHandler<T, TKey>
{
    private readonly ISingleReader<T, TKey> _singleReader;

    public ParametersValidationResultHandler(
        ISingleReader<T, TKey> singleReader
        )
    {
        _singleReader = singleReader;
    }

    public async Task<Result<T>> HandleAsync(TKey key, ValidationResult validationResult)
    {
        if (!validationResult.IsSuccessful)
        {
            //TODO - handle invalid key
        }

        var result = await _singleReader.ReadAsync(key);

        if (result != null)
        {
            return new SuccessResult<T>(
                result
            );
        }

        return new NotFoundResult<T, TKey>(
            key
        );
    }
}
