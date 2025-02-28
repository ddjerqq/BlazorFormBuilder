namespace Client.Common;

public sealed class SelectInput : BaseComponentChoice
{
    public string Value { get; set; } = null!;

    public required List<string> Choices { get; set; }
}