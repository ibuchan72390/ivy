namespace IBFramework.Validation.Core
{
    public interface IValidator<T>
    {
        IValidationResult Validate(T item);
    }
}
