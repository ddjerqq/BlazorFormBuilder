namespace Domain.Entities;

public sealed class CheckboxInput : BaseComponentChoice
{
    public bool Checked { get; set; } = false;
}