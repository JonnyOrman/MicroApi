namespace MicroApi;

public class ValidationResultBuilder : IValidationResultBuilder
{
    private bool _isSuccessful;
    private IDictionary<string, List<string>> _invalidProperties;

    public ValidationResultBuilder()
    {
        _isSuccessful = true;
        _invalidProperties = new Dictionary<string, List<string>>();
    }

    public void Add(ValidationRuleResult validationRuleResult)
    {
        if (!validationRuleResult.IsSuccessful)
        {
            _isSuccessful = false;

            var invalidRuleResult = validationRuleResult as InvalidPropertyRuleResult;

            if (!_invalidProperties.ContainsKey(invalidRuleResult.PropertyName))
            {
                _invalidProperties[invalidRuleResult.PropertyName] = new List<string>();
            }

            _invalidProperties[invalidRuleResult.PropertyName].Add(invalidRuleResult.Message);
        }
    }

    public ValidationResult Build()
    {
        if (_isSuccessful)
        {
            return new ValidResult();
        }

        var invalidPropertyValues = _invalidProperties.Select(x => new InvalidPropertyValue(x.Key, x.Value));

        return new InvalidResult(
            invalidPropertyValues
            );
    }
}
