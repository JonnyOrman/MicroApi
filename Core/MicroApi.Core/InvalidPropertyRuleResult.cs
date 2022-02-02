namespace MicroApi.Core;

public class InvalidPropertyRuleResult : InvalidRuleResult
{
    public InvalidPropertyRuleResult(
        string propertyName,
        string message
        )
        :
        base(
            message
            )
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }
}
