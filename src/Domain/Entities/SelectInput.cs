namespace Domain.Entities;

public sealed class SelectInput : BaseComponentChoice
{
    public string SelectedOption { get; set; } = null!;

    public required List<string> Choices { get; set; }
}