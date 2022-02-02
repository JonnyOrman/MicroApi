using MicroApi.Core;

namespace MicroApi.Create;

public class ParametersValidationResultHandler<T, TParameters> : IParametersValidationResultHandler<T, TParameters>
{
    private readonly ICreator<T, TParameters> _creator;

    public ParametersValidationResultHandler(
        ICreator<T, TParameters> creator
    )
    {

        _creator = creator;
    }

    public async Task<Result<T>> HandleAsync(TParameters parameters, ValidationResult validationResult)
    {
        if (!validationResult.IsSuccessful)
        {
            var invalidResult = validationResult as InvalidResult;

            var invalidParameters = invalidResult.InvalidPropertyValues.Select(x => new InvalidParameter(x.PropertyName, x.ErrorMessages));

            return new InvalidParametersResult<T>(
                invalidParameters
            );
        }

        var entity = await _creator.Create(parameters);

        return new SuccessResult<T>(entity);
    }
}