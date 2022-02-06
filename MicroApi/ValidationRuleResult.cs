namespace MicroApi;

public class ValidationRuleResult
{
    public ValidationRuleResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public bool IsSuccessful { get; }
}
