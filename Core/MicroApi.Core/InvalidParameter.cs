namespace MicroApi.Core;

public class InvalidParameter
{
    public InvalidParameter(
        string parameterName,
        IEnumerable<string> messages
        )
    {
        ParameterName = parameterName;
        Messages = messages;
    }

    public string ParameterName { get; }

    public IEnumerable<string> Messages { get; }
}
