namespace MicroApi;

public class ValidationResultBuilderCreator : IValidationResultBuilderCreator
{
    public IValidationResultBuilder Create()
    {
        return new ValidationResultBuilder();
    }
}
