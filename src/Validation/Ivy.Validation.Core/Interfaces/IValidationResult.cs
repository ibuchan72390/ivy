namespace Ivy.Validation.Core.Interfaces
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        string Message { get; }
    }
}
