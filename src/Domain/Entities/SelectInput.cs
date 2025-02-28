namespace Domain.Entities;

public sealed class SelectInput : BaseComponentChoice
{
    public string SelectedOption { get; set; } = null!;

    // TODO: how do we persist this? using a string?
    public required List<string> Choices { get; set; }
}