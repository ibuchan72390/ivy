namespace IBFramework.Validation.Core
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        string Message { get; }
    }
}
