using System.ComponentModel.DataAnnotations;
using UmbracoBridge.Validators;

namespace MorganasTest;

public class TestableStartsWithAttribute : StartsWithAttribute
{
    public TestableStartsWithAttribute(string prefix) : base(prefix) { }

    public new ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        return base.IsValid(value, validationContext);
    }
}