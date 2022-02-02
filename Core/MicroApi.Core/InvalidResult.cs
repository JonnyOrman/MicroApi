namespace MicroApi.Core;

public class InvalidResult : ValidationResult
{
    public InvalidResult(
        IEnumerable<InvalidPropertyValue> invalidPropertyValues
        )
        :
        base(
            false
            )
    {
        InvalidPropertyValues = invalidPropertyValues;
    }

    public IEnumerable<InvalidPropertyValue> InvalidPropertyValues { get; }
}
