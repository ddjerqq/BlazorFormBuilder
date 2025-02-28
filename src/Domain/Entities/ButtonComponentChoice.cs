namespace Domain.Entities;

/// <summary>
/// The type="submit" button for the form.
/// We could also have buttons of a button like a link
/// </summary>
public sealed class ButtonComponentChoice : BaseComponentChoice
{
    public required string ButtonType { get; set; }

    public required string ButtonText { get; set; }
}