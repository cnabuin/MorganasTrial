using System.ComponentModel.DataAnnotations;
using UmbracoBridge.Validators;

namespace UmbracoBridge;

public class CreateDocumentTypeRequestModel
{
    [Required]
    public string Alias { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StartsWith("icon-")]
    public string Icon { get; set; } = string.Empty;

    public bool AllowedAsRoot { get; set; }
    public bool VariesByCulture { get; set; }
    public bool VariesBySegment { get; set; }
    public object? Collection { get; set; }
    public bool IsElement { get; set; }
}
