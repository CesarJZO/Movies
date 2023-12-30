using System.ComponentModel.DataAnnotations;

namespace ControllersAPI.Validations;

public sealed class FirstLetterUppercaseAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        ReadOnlySpan<char> span = value.ToString();

        if (span.Length is 0)
            return ValidationResult.Success;

        if (char.IsUpper(span[0]))
            return ValidationResult.Success;

        return new ValidationResult("First letter must be uppercase.");
    }
}