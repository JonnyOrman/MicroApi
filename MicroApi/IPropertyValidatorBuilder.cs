namespace MicroApi;

public interface IPropertyValidatorBuilder<T>
{
    void IsRequired();
    IEnumerable<IValidationRule<T>> ToRules();
    void MustPass<TValidationRule>()
        where TValidationRule : IValidationRule<T>, new();
}
