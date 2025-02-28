namespace Client.Common;

/// <summary>
/// The type="submit" button for the form.
/// We could also have buttons of type="button" but that is outside the scope of this project
/// </summary>
public sealed class ButtonComponentChoice : BaseComponentChoice
{
    public required string ButtonText { get; set; }
}