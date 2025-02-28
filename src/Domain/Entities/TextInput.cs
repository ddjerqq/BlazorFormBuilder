namespace Domain.Entities;

public sealed class TextInput : BaseComponentChoice
{
    public string TextValue { get; set; } = null!;
    public string InputType { get; set; } = null!;
    public required string Placeholder { get; set; }
}