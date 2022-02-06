namespace MicroApi;

public class InvalidPropertyValue
{
    public InvalidPropertyValue(
        string propertyName,
        IEnumerable<string> errorMessages
        )
    {
        PropertyName = propertyName;
        ErrorMessages = errorMessages;
    }

    public string PropertyName { get; set; }

    public IEnumerable<string> ErrorMessages { get; set; }
}
