using Ivy.Validation.Core;

namespace Ivy.Validation
{
    public class ValidationResult : IValidationResult
    {
        public ValidationResult(bool isValid = true, string message = null)
        {
            IsValid = isValid;
            Message = message;
        }

        public bool IsValid { get; }

        public string Message { get; }
    }
}
