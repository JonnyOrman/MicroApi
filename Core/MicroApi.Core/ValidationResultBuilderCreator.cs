namespace MicroApi.Core;

public class ValidationResultBuilderCreator : IValidationResultBuilderCreator
{
    public IValidationResultBuilder Create()
    {
        return new ValidationResultBuilder();
    }
}
