using System.ComponentModel.DataAnnotations;

namespace MorganasTest;

public class StartsWithAttributeTests
{
    private readonly TestableStartsWithAttribute _target;
    private readonly ValidationContext _validationContext;
    
    public StartsWithAttributeTests()
    {
        _target = new TestableStartsWithAttribute("Morgana");
        object instance = new 
        {
            DisplayName = "Name"
        };
        _validationContext = new ValidationContext(instance);
    }

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenValueStartsWithPrefix()
    {
        // Arrange
        string testValue = "Morgana le Fay";
        
        // Act
        ValidationResult result = _target.IsValid(testValue, _validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenValueStartsWithPrefixDiferentCasing()
    {
        // Arrange
        string testValue = "morgana le fay";

        // Act
        ValidationResult result = _target.IsValid(testValue, _validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnError_WhenValueDoesNotStartWithPrefix()
    {
        // Arrange
        string testValue = "Merlin";

        // Act
        ValidationResult result = _target.IsValid(testValue, _validationContext);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnError_WhenValueIsEmptyString()
    {
        // Arrange
        string testValue = "";

        // Act
        ValidationResult result = _target.IsValid(testValue, _validationContext);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenValueIsNull()
    {
        // Arrange
        string? testValue = null;

        // Act
        ValidationResult result = _target.IsValid(testValue, _validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }
}
