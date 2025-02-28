namespace Client.Common;

public sealed class TextInput : BaseComponentChoice
{
    public string Value { get; set; } = null!;
    public string InputType { get; set; } = null!;
    public required string Placeholder { get; set; }
}