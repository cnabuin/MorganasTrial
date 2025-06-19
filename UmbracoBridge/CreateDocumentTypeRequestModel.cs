﻿namespace UmbracoBridge;

public class CreateDocumentTypeRequestModel
{
    public string Alias { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public bool AllowedAsRoot { get; set; }
    public bool VariesByCulture { get; set; }
    public bool VariesBySegment { get; set; }
    public object? Collection { get; set; }
    public bool IsElement { get; set; }
}
