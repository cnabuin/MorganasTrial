using System.ComponentModel.DataAnnotations;

namespace UmbracoBridge.Validators;

public class StartsWithAttribute : ValidationAttribute
{
    private readonly string _prefix;

    public StartsWithAttribute(string prefix)
    {
        _prefix = prefix;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str && !str.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must start with '{_prefix}'.");
        }
        return ValidationResult.Success!;
    }
}