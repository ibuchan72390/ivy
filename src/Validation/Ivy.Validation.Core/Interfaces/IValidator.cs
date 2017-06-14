namespace Ivy.Validation.Core.Interfaces
{
    public interface IValidator<T>
    {
        IValidationResult Validate(T item);
    }
}
