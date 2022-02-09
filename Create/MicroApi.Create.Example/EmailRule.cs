namespace MicroApi.Create.Example;

public class EmailRule : IValidationRule<UserParameters>
{
    public ValidationRuleResult Validate(UserParameters value)
    {
        var isValid = value.Email.Contains('@') && value.Email.Contains('.');

        if (isValid)
        {
            return new ValidationRuleResult(true);
        }
        
        return new InvalidPropertyRuleResult(nameof(value.Email), $"{value.Email} is not a valid email");
    }
}