namespace MicroApi;

public class InvalidRuleResult : ValidationRuleResult
{
    public InvalidRuleResult(
        string message
        )
        :
        base(
            false
            )
    {
        Message = message;
    }

    public string Message { get; }
}
